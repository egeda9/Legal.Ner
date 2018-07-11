using System.Collections.Generic;
using Legal.Ner.Domain;

namespace Legal.Ner.DataAccess.Interfaces
{
    public interface IFileKeyData
    {
        int Insert(string fileName);
        bool GetIf(string fileName);
        List<FileKey> Get(string searchString);
        FileKey Get(int id);
        void Update(FileKey fileKey);
        void Delete(FileKey fileKey);
    }
}
