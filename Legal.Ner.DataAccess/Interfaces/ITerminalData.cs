using System.Collections.Generic;
using Legal.Ner.Domain;

namespace Legal.Ner.DataAccess.Interfaces
{
    public interface ITerminalData
    {
        void Insert(List<Terminal> terminals, int fileKeyId, int treeId);
    }
}
