using System.Collections.Generic;
using Legal.Ner.Domain;

namespace Legal.Ner.DataAccess.Interfaces
{
    public interface INonTerminalData
    {
        void Insert(List<NonTerminal> nonTerminals, int fileKeyId, int treeId);
    }
}
