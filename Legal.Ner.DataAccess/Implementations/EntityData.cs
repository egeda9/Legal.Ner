using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain;
using Neo4j.Driver.V1;

namespace Legal.Ner.DataAccess.Implementations
{
    public class EntityData : IEntityData
    {
        private readonly IDbConnection _db;
        private readonly ILogger _logger;

        public EntityData(ILogger logger)
        {
            _logger = logger;
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
        }

        public void Insert(List<Entity> entities, int fileKeyId)
        {
            const string insertEntity = "INSERT INTO Entity(Eid,EntityType,FileKey_Id) values (@Eid,@EntityType,@FileKey_Id);";
            const string insertEntityTerm = "INSERT INTO EntityTerm(Eid,FileKey_Id,Tid) values (@Eid,@FileKey_Id,@Tid);";
            try
            {
                foreach (Entity entity in entities)
                {
                    _db.Execute(insertEntity, new { Eid = entity.Eid, EntityType = entity.EntityType, FileKey_Id = fileKeyId });

                    foreach (var entityTerm in entity.Terms)
                        _db.Execute(insertEntityTerm, new { Eid = entity.Eid, EntityType = entity.EntityType, Tid = entityTerm, FileKey_Id = fileKeyId });
                }
                _db.Execute("BulkEntitiesByFileKey", new { FileKey = fileKeyId }, commandType: CommandType.StoredProcedure);
            }

            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }
    }
}
