using System.IO;
using Legal.Ner.Domain;

namespace Legal.Ner.Business.Interfaces
{
    public interface IProcessFile
    {
        FileContent MapData(Stream inputStream, string fileName);
    }
}
