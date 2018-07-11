using System.Collections.Generic;
using Legal.Ner.Domain;

namespace Legal.Ner.DataAccess.Interfaces
{
    public interface IEntityBulkData
    {
        List<EntityBulk> Get(int fileKeyId, string searchString);
        EntityBulk GetByEid(string eid, int fileKeyId);
        void Update(EntityBulk entityBulk);
        void Delete(EntityBulk entityBulk);
    }
}
