using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Legal.Ner.Domain.Graph;

namespace Legal.Ner.Web.Models
{
    public class GraphRelationViewModel
    {
        [Required]
        [Display(Name = "Origen")]
        public string SelectedSource { get; set; }
        public IEnumerable<BaseSemanticGraph> Sources { get; set; }

        [Required]
        [Display(Name = "Object property")]
        public string SelectedObjectProperty { get; set; }
        public IEnumerable<BaseSemanticGraph> ObjectProperties { get; set; }

        [Required]
        [Display(Name = "Destino")]
        public string SelectedTarget { get; set; }
        public IEnumerable<BaseSemanticGraph> Targets { get; set; }
    }
}