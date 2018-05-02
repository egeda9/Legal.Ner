using System.Collections.Generic;
using System.IO;
using System.Xml;
using Legal.Ner.Business.Interfaces;
using Legal.Ner.Domain;
using Legal.Ner.DataAccess.Implementations;

namespace Legal.Ner.Business.Implementations
{
    public class ProcessFile : IProcessFile
    {
        private readonly FileKeyData _fileKeyData;
        private readonly WordData _wordData;
        private readonly TermData _termData;
        private readonly EntityData _entityData;
        private readonly TreeData _treeData;
        private readonly TerminalData _terminalData;
        private readonly NonTerminalData _nonTerminalData;
        private readonly TreeEdgeData _treeEdgeData;

        public ProcessFile(FileKeyData fileKeyData, WordData wordData, TermData termData, EntityData entityData, TreeData treeData, TerminalData terminalData, NonTerminalData nonTerminalData, TreeEdgeData treeEdgeData)
        {
            _fileKeyData = fileKeyData;
            _wordData = wordData;
            _termData = termData;
            _entityData = entityData;
            _treeData = treeData;
            _terminalData = terminalData;
            _nonTerminalData = nonTerminalData;
            _treeEdgeData = treeEdgeData;
        }

        public FileContent MapData(Stream inputStream, string fileName)
        {
            FileContent fileContent = new FileContent();

            List<Word> words = new List<Word>();
            List<Term> terms = new List<Term>();
            List<Entity> entities = new List<Entity>();
            List<Tree> trees = new List<Tree>();

            using (XmlReader xmlReader = XmlReader.Create(inputStream))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        // Words
                        if (xmlReader.LocalName == "wf")
                        {
                            Word word = new Word
                            {
                                Wid = xmlReader.GetAttribute("wid"),
                                Length = xmlReader.GetAttribute("length"),
                                Offset = xmlReader.GetAttribute("offset"),
                                Para = xmlReader.GetAttribute("para"),
                                Sent = xmlReader.GetAttribute("sent"),
                                Value = xmlReader.ReadElementContentAsString()
                            };
                            words.Add(word);
                        }

                        // Terms
                        if (xmlReader.LocalName == "term")
                        {
                            Term term = new Term
                            {
                                Pos = xmlReader.GetAttribute("pos"),
                                Lemma = xmlReader.GetAttribute("lemma"),
                                Type = xmlReader.GetAttribute("type"),
                                Tid = xmlReader.GetAttribute("tid"),
                                Morphofeat = xmlReader.GetAttribute("morphofeat")
                            };

                            using (XmlReader subtree = xmlReader.ReadSubtree())
                            {
                                while (subtree.Read())
                                {
                                    if (subtree.LocalName == "target")
                                        term.Wid = subtree.GetAttribute("id");
                                }
                            }
                            terms.Add(term);
                        }

                        // Entities
                        if (xmlReader.LocalName == "entity")
                        {
                            Entity entity = new Entity
                            {
                                Eid = xmlReader.GetAttribute("eid"),
                                EntityType = xmlReader.GetAttribute("type")
                            };

                            List<string> targetTerms = new List<string>();

                            using (XmlReader subtree = xmlReader.ReadSubtree())
                            {
                                while (subtree.Read())
                                {
                                    if (subtree.LocalName == "target")
                                        targetTerms.Add(subtree.GetAttribute("id"));
                                }
                                entity.Terms = targetTerms;
                            }
                            entities.Add(entity);
                        }

                        // Trees
                        if (xmlReader.LocalName == "tree")
                        {
                            Tree tree = new Tree();

                            using (XmlReader subtree = xmlReader.ReadSubtree())
                            {
                                List<NonTerminal> nonTerminals = new List<NonTerminal>();
                                List<Terminal> terminals = new List<Terminal>();
                                List<TreeEdge> treeEdges = new List<TreeEdge>();

                                while (subtree.Read())
                                {
                                    if (subtree.LocalName == "nt")
                                    {
                                        NonTerminal nonTerminal = new NonTerminal
                                        {
                                            Id = subtree.GetAttribute("id"),
                                            Label = subtree.GetAttribute("label")
                                        };

                                        nonTerminals.Add(nonTerminal);
                                    }

                                    if (subtree.LocalName == "t")
                                    {
                                        Terminal terminal = new Terminal
                                        {
                                            Id = subtree.GetAttribute("id")
                                        };

                                        using (XmlReader subtreeTargetTerminal = subtree.ReadSubtree())
                                        {
                                            while (subtreeTargetTerminal.Read())
                                            {
                                                if (subtreeTargetTerminal.LocalName == "target")
                                                    terminal.Tid = subtreeTargetTerminal.GetAttribute("id");
                                            }
                                        }
                                        terminals.Add(terminal);
                                    }

                                    if (subtree.LocalName == "edge")
                                    {
                                        TreeEdge treeEdge = new TreeEdge
                                        {
                                            Id = subtree.GetAttribute("id"),
                                            FromNode = subtree.GetAttribute("from"),
                                            ToNode = subtree.GetAttribute("to"),
                                            Head = subtree.GetAttribute("head") == "yes"
                                        };
                                        treeEdges.Add(treeEdge);
                                    }
                                }

                                tree.TreeEdges = treeEdges;
                                tree.NonTerminals = nonTerminals;
                                tree.Terminals = terminals;
                            }
                            trees.Add(tree);
                        }
                    }
                }
            }

            fileContent.Words = words;
            fileContent.Terms = terms;
            fileContent.Entities = entities;
            fileContent.Trees = trees;
            fileContent.FileName = fileName;

            InsertData(fileContent);

            return fileContent;
        }

        private void InsertData(FileContent fileContent)
        {
            if (!_fileKeyData.GetIf(fileContent.FileName))
            {
                int fileKeyId = _fileKeyData.Insert(fileContent.FileName);

                _wordData.Insert(fileContent.Words, fileKeyId);
                _termData.Insert(fileContent.Terms, fileKeyId);
                _entityData.Insert(fileContent.Entities, fileKeyId);
                
                foreach (Tree tree in fileContent.Trees)
                {
                    int treeId = _treeData.Insert(fileKeyId);
                    _nonTerminalData.Insert(tree.NonTerminals, fileKeyId, treeId);
                    _terminalData.Insert(tree.Terminals, fileKeyId, treeId);
                    _treeEdgeData.Insert(tree.TreeEdges, fileKeyId, treeId);
                }
            }
        }
    }
}
