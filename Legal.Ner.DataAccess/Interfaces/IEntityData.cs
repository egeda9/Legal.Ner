using System.Collections.Generic;
using Legal.Ner.Domain;

namespace Legal.Ner.DataAccess.Interfaces
{
    public interface IEntityData
    {
        void Insert(List<Entity> entities, int fileKeyId);
    }
}
