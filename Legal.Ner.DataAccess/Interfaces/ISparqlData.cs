using Legal.Ner.Domain.Graph;

namespace Legal.Ner.DataAccess.Interfaces
{
    public interface ISparqlData
    {
        void Get(BaseSemanticGraph baseSemanticGraph);
    }
}
