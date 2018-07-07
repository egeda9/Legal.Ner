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
        private readonly IDbConnection _db;

        public SparqlPredifinedNamespacesPrefixesData()
        {
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        }

        public IList<SparqlPredefinedNamespacePrefix> Get()
        {
            using (IDbConnection db = _db)
            {
                db.Open();
                return db.Query<SparqlPredefinedNamespacePrefix>("SELECT * FROM metadata.PredefinedNamespacePrefixes").ToList();
            }
        }
    }
}
