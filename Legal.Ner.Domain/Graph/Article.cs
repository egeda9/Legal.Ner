using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Legal.Ner.Domain.Graph
{
    public class Article : DocumentPart
    {
        [Display(Name = "Tiene Inciso")]
        [JsonProperty(PropertyName = "TIENE_INCISO")]
        public string HasIncese { get; set; }

        [Display(Name = "Tiene Literal")]
        [JsonProperty(PropertyName = "TIENE_LITERAL")]
        public string HasLiteral { get; set; }

        [Display(Name = "Tiene Numeral")]
        [JsonProperty(PropertyName = "TIENE_LITERAL")]
        public int HasNumeral { get; set; }

        [Display(Name = "Tiene Parágrafo")]
        [JsonProperty(PropertyName = "TIENE_PARAGRAFO")]
        public string HasParagraph { get; set; }

        [Display(Name = "Tiene Párrafo")]
        [JsonProperty(PropertyName = "TIENE_PARRAFO")]
        public string HasSection { get; set; }
    }
}
