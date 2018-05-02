using System.ComponentModel.DataAnnotations;

namespace Legal.Ner.Domain
{
    public class EntityBulk
    {
        [Display(Name = "Eid")]
        public string Eid { get; set; }

        [Display(Name = "Tipo de Entidad")]
        public string EntityType { get; set; }

        [Display(Name = "Nombre de Entidad")]
        public string EntityName { get; set; }

        [Display(Name = "Agregado")]
        public bool Added { get; set; }

        public FileKey FileKey { get; set; }
    }
}
