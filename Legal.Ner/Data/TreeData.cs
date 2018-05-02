using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using log4net;

namespace Legal.Ner.Data
{
    public class TreeData
    {
        private readonly IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        private readonly ILog _log;

        public TreeData()
        {
            log4net.Config.BasicConfigurator.Configure();
            _log = LogManager.GetLogger(typeof(TreeData));
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
                _log.Error(ex.Message);
            }
            return result;
        }
    }
}
