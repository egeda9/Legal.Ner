using System;
using System.Data;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain.Extensions;
using Legal.Ner.Log;
using VDS.RDF.Query;
using VDS.RDF.Storage;

namespace Legal.Ner.DataAccess.Implementations
{
    public class SparqlData : ISparqlData
    {
        private readonly ILogger _logger;
        private readonly FusekiConnector _connector;

        public SparqlData(ILogger logger)
        {
            _logger = logger;
            _connector = new FusekiConnector("http://localhost:3030/ontologia-legal-colombia/data");
        }

        public string Get(string query)
        {
            string output = null;
            try
            {
                var result = (SparqlResultSet) _connector.Query(query);
                output = result.ToDataTable().ToHtml();
            }
            catch (Exception e)
            {
                _logger.Error(e.Message, e);
            }
            return output;
        }
    }
}
