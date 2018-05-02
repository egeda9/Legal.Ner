using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Legal.Ner.Domain.Graph
{
    public class LegislativeAct : LegalDocument
    {
        [Display(Name = "Tiene Aprobación")]
        [JsonProperty(PropertyName = "TIENE_APROBACION")]
        public string HasApproval { get; set; }

        [Display(Name = "Tiene Debate")]
        [JsonProperty(PropertyName = "TIENE_DEBATE")]
        public string HasDebate { get; set; }

        [Display(Name = "Tiene Discusión")]
        [JsonProperty(PropertyName = "TIENE_DISCUSION")]
        public string HasDiscussion { get; set; }

        [Display(Name = "Tiene Exposición Motivos")]
        [JsonProperty(PropertyName = "TIENE_EXPOSICION_MOTIVOS")]
        public string HasExhibitionReasons { get; set; }

        [Display(Name = "Tiene Hipótesis")]
        [JsonProperty(PropertyName = "TIENE_HIPOTESIS")]
        public string HasHypothesis { get; set; }

        [Display(Name = "Tiene Iniciativa")]
        [JsonProperty(PropertyName = "TIENE_INICIATIVA")]
        public string HasInitiative { get; set; }

        [Display(Name = "Tiene Libro")]
        [JsonProperty(PropertyName = "TIENE_LIBRO")]
        public string HasBook { get; set; }

        [Display(Name = "Tiene Mandato")]
        [JsonProperty(PropertyName = "TIENE_MANDATO")]
        public string HasMandate { get; set; }

        [Display(Name = "Tiene Sanción")]
        [JsonProperty(PropertyName = "TIENE_SANCION")]
        public string HasSaction { get; set; }
    }
}
