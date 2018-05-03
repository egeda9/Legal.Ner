using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Log;

namespace Legal.Ner.DataAccess.Implementations
{
    public class TreeData : ITreeData
    {
        private readonly IDbConnection _db;
        private readonly ILogger _logger;

        public TreeData(ILogger logger)
        {
            _logger = logger;
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        }

        public int Insert(int fileKeyId)
        {
            int result = 0;
            const string sql = "INSERT INTO Tree(FileKey_Id) values (@FileKey_Id); SELECT CAST(SCOPE_IDENTITY() as int)";
            try
            {
                int treeId = _db.Query<int>(sql, new { FileKey_Id = fileKeyId }).SingleOrDefault();
                result = treeId;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
            return result;
        }
    }
}
