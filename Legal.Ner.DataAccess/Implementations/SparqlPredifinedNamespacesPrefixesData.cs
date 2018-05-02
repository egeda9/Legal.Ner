using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain.Metadata;

namespace Legal.Ner.DataAccess.Implementations
{
    public class SparqlPredifinedNamespacesPrefixesData : ISparqlPredifinedNamespacesPrefixesData
    {
        public IList<SparqlPredefinedNamespacePrefixes> Get()
        {
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString))
            {
                return db.Query<SparqlPredefinedNamespacePrefixes>("SELECT * FROM metadata.PredefinedNamespacePrefixes").ToList();
            }
        }
    }
}
