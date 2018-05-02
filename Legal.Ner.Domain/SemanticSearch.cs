using System.ComponentModel.DataAnnotations;

namespace Legal.Ner.Domain
{
    public class SemanticSearch
    {
        [Display(Name = "Consulta")]
        public string Query { get; set; }
    }
}
