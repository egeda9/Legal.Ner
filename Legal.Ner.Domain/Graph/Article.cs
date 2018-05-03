using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Legal.Ner.Domain.Graph
{
    public class Article : DocumentPart
    {
        [JsonProperty(PropertyName = "TIENE_INCISO")]
        public string[] HasIncese { get; set; }

        [Display(Name = "Tiene Inciso")]
        public string HasInceseValue { get; set; }

        [JsonProperty(PropertyName = "TIENE_LITERAL")]
        public string[] HasLiteral { get; set; }

        [Display(Name = "Tiene Literal")]
        public string HasLiteralValue { get; set; }

        [JsonProperty(PropertyName = "TIENE_NUMERAL")]
        public string[] HasNumeral { get; set; }

        [Display(Name = "Tiene Numeral")]
        public string HasNumeralValue { get; set; }

        [Display(Name = "Tiene Parágrafo")]
        [JsonProperty(PropertyName = "TIENE_PARAGRAFO")]
        public string HasParagraph { get; set; }

        [Display(Name = "Tiene Párrafo")]
        [JsonProperty(PropertyName = "TIENE_PARRAFO")]
        public string HasSection { get; set; }
    }
}
