using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain.Graph;
using Legal.Ner.Domain.Metadata;
using SPARQLNET;
using SPARQLNET.Enums;

namespace Legal.Ner.DataAccess.Implementations
{
    public class SparqlData : ISparqlData
    {
        private const string DbpediaEndpoint = "http://es.dbpedia.org/sparql";
        private readonly QueryClient _client;
        private readonly ISparqlPredifinedNamespacesPrefixesData _predifinedNamespaces;

        public SparqlData(ISparqlPredifinedNamespacesPrefixesData predifinedNamespaces)
        {
            _predifinedNamespaces = predifinedNamespaces;
            _client = new QueryClient(DbpediaEndpoint);
        }

        public void Get(BaseSemanticGraph baseSemanticGraph)
        {
            IList<SparqlPredefinedNamespacePrefixes> predefinedNamespaces = _predifinedNamespaces.Get();
            StringBuilder prefixes = new StringBuilder();

            foreach (var predefinedNamespace in predefinedNamespaces)
                prefixes.Append($"PREFIX {predefinedNamespace.Prefix}: <{predefinedNamespace.Uri}> ");

            //Create a query that finds people who were born in Berlin before 1900
            var result = _client.Query(prefixes + " SELECT ?person WHERE{ ?person dbpedia-owl:wikiPageWikiLink <" + baseSemanticGraph.Uri + ">}");
        }
    }
}
