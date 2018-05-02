using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Xml;
using Legal.Ner.Data;
using Legal.Ner.DTO;

namespace Legal.Ner
{
    public class Program
    {
        private static List<Word> _words;
        private static List<Term> _terms;
        private static List<Entity> _entities;
        private static List<Tree> _trees;

        public static void Main(string[] args)
        {
            string filesPath = ConfigurationManager.AppSettings["FilesPath"];

            MapData(filesPath);

            Console.WriteLine("Process Finished!");
            Console.ReadKey();
        }

        private static void MapData(string filesPath)
        {
            foreach (string file in Directory.EnumerateFiles(filesPath, "*.kaf"))
            {
                _words = new List<Word>();
                _terms = new List<Term>();
                _entities = new List<Entity>();
                _trees = new List<Tree>();

                string fileName = Path.GetFileName(file);

                Console.WriteLine($"Process Started - File: {fileName}");

                using (XmlReader xmlReader = XmlReader.Create(file))
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
                                _words.Add(word);
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
                                _terms.Add(term);
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
                                _entities.Add(entity);
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
                                _trees.Add(tree);
                            }
                        }
                    }
                }

                Console.WriteLine($"File: {fileName} - Words: {_words.Count}");
                Console.WriteLine($"File: {fileName} - Terms: {_terms.Count}");
                Console.WriteLine($"File: {fileName} - Entities: {_entities.Count}");
                Console.WriteLine($"File: {fileName} - Trees: {_trees.Count}");

                InsertData(fileName);

                Console.WriteLine($"Process Completed - File: {fileName}");
            }
        }

        private static void InsertData(string fileName)
        {
            FileKeyData fileKeyData = new FileKeyData();

            if (!fileKeyData.GetIf(fileName))
            {
                WordData wordData = new WordData();
                TermData termData = new TermData();
                EntityData entityData = new EntityData();

                TreeData treeData = new TreeData();
                TerminalData terminalData = new TerminalData();
                NonTerminalData nonTerminalData = new NonTerminalData();
                TreeEdgeData treeEdgeData = new TreeEdgeData();

                int fileKeyId = fileKeyData.Insert(fileName);

                Console.WriteLine($"Insert Words: {_words.Count}");
                wordData.Insert(_words, fileKeyId);

                Console.WriteLine($"Insert Terms: {_terms.Count}");
                termData.Insert(_terms, fileKeyId);

                Console.WriteLine($"Insert Entities: {_entities.Count}");
                entityData.Insert(_entities, fileKeyId);

                Console.WriteLine($"Insert Trees: {_trees.Count}");
                foreach (Tree tree in _trees)
                {
                    int treeId = treeData.Insert(fileKeyId);
                    nonTerminalData.Insert(tree.NonTerminals, fileKeyId, treeId);
                    terminalData.Insert(tree.Terminals, fileKeyId, treeId);
                    treeEdgeData.Insert(tree.TreeEdges, fileKeyId, treeId);
                }
            }
        }
    }
}
