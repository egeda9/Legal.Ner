using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Legal.Ner.Domain
{
    public class Entity
    {
        [Display(Name = "Eid")]
        public string Eid { get; set; }

        [Display(Name = "Tipo de Entidad")]
        public string EntityType { get; set; }

        public List<string> Terms { get; set; }
    }
}
