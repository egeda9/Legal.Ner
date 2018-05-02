using System.Collections.Generic;

namespace Legal.Ner.Domain
{
    public class FileContent
    {
        public List<Word> Words { get; set; }
        public List<Term> Terms { get; set; }
        public List<Entity> Entities { get; set; }
        public List<Tree> Trees { get; set; }
        public string FileName { get; set; }
    }
}
