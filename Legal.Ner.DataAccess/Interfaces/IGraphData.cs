using System.Collections.Generic;
using Legal.Ner.Domain.Graph;

namespace Legal.Ner.DataAccess.Interfaces
{
    public interface IGraphData
    {
        IEnumerable<BaseSemanticGraph> GetItems(string filter);
        void CreateNode(BaseSemanticGraph baseSemanticGraph);
        void RelateNodes(string sourceMatch, string targetMatch, string relation, string sourceFilter, string targetFilter);
        void DeleteNode(BaseSemanticGraph baseSemanticGraph);
        void UpdateUriNode(BaseSemanticGraph baseSemanticGraph);
    }
}
