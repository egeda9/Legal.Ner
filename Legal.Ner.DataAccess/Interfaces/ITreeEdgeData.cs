using System.Collections.Generic;
using Legal.Ner.Domain;

namespace Legal.Ner.DataAccess.Interfaces
{
    public interface ITreeEdgeData
    {
        void Insert(List<TreeEdge> treeEdges, int fileKeyId, int treeId);
    }
}
