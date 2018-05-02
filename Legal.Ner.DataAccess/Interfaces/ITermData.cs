using System.Collections.Generic;
using Legal.Ner.Domain;

namespace Legal.Ner.DataAccess.Interfaces
{
    public interface ITermData
    {
        void Insert(List<Term> terms, int fileKeyId);
    }
}
