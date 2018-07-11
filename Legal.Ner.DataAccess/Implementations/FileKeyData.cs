using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain;
using Legal.Ner.Log;

namespace Legal.Ner.DataAccess.Implementations
{
    public class FileKeyData : IFileKeyData
    {
        private readonly IDbConnection _db;
        private readonly ILogger _logger;

        public FileKeyData(ILogger logger)
        {
            _logger = logger;
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        }

        public int Insert(string fileName)
        {
            int result = 0;
            const string sql = "INSERT INTO FileKey(FileName,UploadDate) values (@FileName,@UploadDate); SELECT CAST(SCOPE_IDENTITY() as int)";
            try
            {
                int filekeyId = _db.Query<int>(sql, new { FileName = fileName.ToLower(), UploadDate = DateTime.Now }).SingleOrDefault();
                result = filekeyId;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
            return result;
        }

        public bool GetIf(string fileName)
        {
            string sql = "SELECT COUNT(1) FROM FileKey WHERE FileName = @FileName";
            int result = _db.Query<int>(sql, new { FileName = fileName.ToLower()} ).SingleOrDefault();
            return result > 0;
        }

        public List<FileKey> Get(string searchString)
        {
            List<FileKey> files;
            using (IDbConnection db = _db)
            {
                db.Open();
                string filterString = !string.IsNullOrEmpty(searchString) ? "WHERE f.DocumentTitle LIKE CONCAT('%',@SearchString,'%')" : string.Empty;
                string sql = "SELECT f.Id" +
                             " ,f.Description" +
                             " ,f.DocumentTitle" +
                             " ,f.FileName" +
                             " ,f.Number" +
                             " ,f.ReleaseDate" +
                             " ,f.UploadDate" +
                             " FROM FileKey f " +
                             filterString +
                             " ORDER BY f.Id";

                files = db.Query<FileKey>(sql, new { SearchString = searchString }).ToList();
            }
            return files;
        }

        public FileKey Get(int id)
        {
            FileKey fileKey;
            using (IDbConnection db = _db)
            {
                db.Open();
                string sql = "SELECT f.Id" +
                             " ,f.Description" +
                             " ,f.DocumentTitle" +
                             " ,f.FileName" +
                             " ,f.Number" +
                             " ,f.ReleaseDate" +
                             " ,f.UploadDate" +
                             " FROM FileKey f" +
                             " WHERE f.Id = @FileKeyId";

                fileKey = db.Query<FileKey>(sql, new { FileKeyId = id }).SingleOrDefault();
            }
            return fileKey;
        }

        public void Update(FileKey fileKey)
        {
            using (IDbConnection db = _db)
            {
                db.Open();
                string sql = "UPDATE FileKey" +
                             " SET Description = @Description" +
                             " ,DocumentTitle = @DocumentTitle" +
                             " ,Number = @Number" +
                             " ,ReleaseDate = @ReleaseDate" +
                             " WHERE Id = @Id";

                db.Execute(sql, new { Id = fileKey.Id, Description = fileKey.Description, DocumentTitle = fileKey.DocumentTitle, Number = fileKey.Number, ReleaseDate = fileKey.ReleaseDate });
            }
        }

        public void Delete(FileKey fileKey)
        {
            using (IDbConnection db = _db)
            {
                db.Open();
                string sql = "DELETE FileKey" +
                             " WHERE Id = @Id";

                db.Execute(sql, new { Id = fileKey.Id });
            }
        }
    }
}
