using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using log4net;

namespace Legal.Ner.Data
{
    public class FileKeyData
    {
        private readonly ILog _log;

        public FileKeyData()
        {
            log4net.Config.BasicConfigurator.Configure();
            _log = LogManager.GetLogger(typeof(FileKeyData));
        }

        public int Insert(string fileName)
        {
            var result = 0;
            const string sql = "INSERT INTO FileKey(FileName,UploadDate) values (@FileName,@UploadDate); SELECT CAST(SCOPE_IDENTITY() as int)";
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager .ConnectionStrings["DefaultConnectionString"].ConnectionString))
                {
                    var filekeyId = db.Query<int>(sql, new {FileName = fileName.ToLower(), UploadDate = DateTime.Now}).SingleOrDefault();
                    result = filekeyId;
                }
            }

            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            return result;
        }

        public bool GetIf(string fileName)
        {
            int result;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString))
            {
                string sql = "SELECT COUNT(1) FROM FileKey WHERE FileName = @FileName";
                result = db.Query<int>(sql, new { FileName = fileName.ToLower() }).SingleOrDefault();
            }
            return result > 0;
        }
    }
}
