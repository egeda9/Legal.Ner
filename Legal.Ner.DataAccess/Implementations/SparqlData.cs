using System;
using System.Configuration;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain.Extensions;
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
            string output;
            try
            {
                var result = (SparqlResultSet) _connector.Query(query);
                output     = result.ToDataTable().ToHtml();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message, e);
            }
            return output;
        }
    }
}
