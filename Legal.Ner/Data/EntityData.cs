using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using log4net;
using Legal.Ner.DTO;

namespace Legal.Ner.Data
{
    public class EntityData
    {
        private readonly IDbConnection _db;
        private readonly ILog _log;

        public EntityData()
        {
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString);
            log4net.Config.BasicConfigurator.Configure();
            _log = LogManager.GetLogger(typeof(EntityData));
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
                _log.Error(ex.Message);
            }
        }
    }
}
