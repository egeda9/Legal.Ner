using System.Collections.Generic;
using Legal.Ner.Domain;

namespace Legal.Ner.DataAccess.Interfaces
{
    public interface IWordData
    {
        void Insert(List<Word> words, int fileKeyId);
    }
}
