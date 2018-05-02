using System.Collections.Generic;
using Legal.Ner.Domain.Metadata;

namespace Legal.Ner.DataAccess.Interfaces
{
    public interface ISparqlPredifinedNamespacesPrefixesData
    {
        IList<SparqlPredefinedNamespacePrefixes> Get();
    }
}
