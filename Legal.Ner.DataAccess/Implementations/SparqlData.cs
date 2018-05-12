using System;
using System.Configuration;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain.Extensions;
using VDS.RDF;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace Legal.Ner.DataAccess.Implementations
{
    public class SparqlData : ISparqlData
    {
        private readonly FusekiConnector _connector;

        public SparqlData()
        {
            _connector = new FusekiConnector(ConfigurationManager.AppSettings["FusekiBaseUri"]);
        }

        public string Get(string query)
        {
            string output = null;
            try
            {
                var result = _connector.Query(query);
                if (result is SparqlResultSet)
                {
                    var sparqlResultSet = (SparqlResultSet) result;
                    output = sparqlResultSet.ToDataTable().ToHtml();
                }

                if (result is IGraph)
                {
                    IGraph graph = (Graph)result;
                    output = graph.Triples.ToHtml();
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message, e);
            }
            return output;
        }

        public string GetRemote(string query, string endpoint)
        {
            string output;
            SparqlRemoteEndpoint sparqlRemoteEndpoint = new SparqlRemoteEndpoint(new Uri($"{endpoint}sparql"), endpoint);
            
            try
            {
                output = sparqlRemoteEndpoint.QueryWithResultSet(query)
                    .ToDataTable().ToHtml();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message, e);
            }
            return output;
        }
    }
}
