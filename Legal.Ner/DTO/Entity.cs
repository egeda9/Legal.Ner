using System.Collections.Generic;

namespace Legal.Ner.DTO
{
    public class Entity
    {
        public string Eid { get; set; }
        public string EntityType { get; set; }
        public List<string> Terms { get; set; }
    }
}
