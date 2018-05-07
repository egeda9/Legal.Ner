using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain;

namespace Legal.Ner.DataAccess.Implementations
{
    public class EntityBulkData : IEntityBulkData
    {
        private readonly IDbConnection _db;

        public EntityBulkData()
        {
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        }

        public List<EntityBulk> Get(int fileKeyId, string searchString)
        {
            List<EntityBulk> entities;
            using (IDbConnection db = _db)
            {
                db.Open();
                string filterString = !string.IsNullOrEmpty(searchString) ? "AND e.EntityName LIKE CONCAT('%',@SearchString,'%')" : string.Empty;
                string sql = "SELECT e.Eid" +
                             " ,e.EntityName" +
                             " ,e.EntityType" +
                             " ,e.FileKey_Id" +
                             " ,e.Added" +
                             " ,f.Id" +
                             " ,f.Description" +
                             " ,f.DocumentTitle" +
                             " ,f.FileName" +
                             " ,f.Number" +
                             " ,f.ReleaseDate" +
                             " ,f.UploadDate" +
                             " FROM EntityBulk e" +
                             " INNER JOIN FileKey f ON f.Id = e.FileKey_Id" +
                             " WHERE f.Id = @FileKeyId " +
                             filterString +
                             " ORDER BY CAST(SUBSTRING(e.Eid, 2, 10) AS INT)";

                entities = db.Query<EntityBulk, FileKey, EntityBulk>(sql, (e, f) => { e.FileKey = f; return e; }, new { FileKeyId = fileKeyId, SearchString = searchString }).ToList();
                db.Close();
            }
            return entities;
        }

        public EntityBulk GetByEid(string eid, int fileKeyId)
        {
            EntityBulk entity;
            using (IDbConnection db = _db)
            {
                db.Open();
                string sql = "SELECT e.Eid" +
                             " ,e.EntityName" +
                             " ,e.EntityType" +
                             " ,e.FileKey_Id" +
                             " ,e.Added" +
                             " ,f.Id" +
                             " ,f.Description" +
                             " ,f.DocumentTitle" +
                             " ,f.FileName" +
                             " ,f.Number" +
                             " ,f.ReleaseDate" +
                             " ,f.UploadDate" +
                             " FROM EntityBulk e" +
                             " INNER JOIN FileKey f ON f.Id = e.FileKey_Id" +
                             " WHERE f.Id = @FileKeyId" +
                             " AND  e.Eid = @Eid" +
                             " ORDER BY CAST(SUBSTRING(e.Eid, 2, 10) AS INT)";

                entity = db.Query<EntityBulk, FileKey, EntityBulk>(sql, (e, f) => { e.FileKey = f; return e; }, new { FileKeyId = fileKeyId, Eid = eid }).SingleOrDefault();
                db.Close();
            }
            return entity;
        }

        public void Update(EntityBulk entityBulk)
        {
            using (IDbConnection db = _db)
            {
                db.Open();
                string sql = "UPDATE EntityBulk" +
                             " SET EntityType = @EntityType" +
                             " ,EntityName = @EntityName" +
                             " ,Added = @Added" +
                             " WHERE FileKey_Id = @FileKeyId" +
                             " AND Eid = @Eid";

                db.Execute(sql, new { FileKeyId = entityBulk.FileKey.Id, Eid = entityBulk.Eid, EntityType = entityBulk.EntityType, EntityName = entityBulk.EntityName, Added = entityBulk.Added });
                db.Close();
            }
        }
    }
}
