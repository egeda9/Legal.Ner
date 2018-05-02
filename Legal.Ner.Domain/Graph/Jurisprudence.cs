using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Legal.Ner.Domain.Graph
{
    public class Jurisprudence : LegalDocument
    {
        [Display(Name = "Tiene Aclaración Voto")]
        [JsonProperty(PropertyName = "TIENE_ACLARACION_VOTO")]
        public string HasVoteClarification { get; set; }

        [Display(Name = "Tiene Aspecto Jurídico Analizado")]
        [JsonProperty(PropertyName = "TIENE_ASPECTO_JURIDICO_ANALIZADO")]
        public string HasAnalyzedLegalAspect { get; set; }

        [Display(Name = "Tiene Explicación Tesis")]
        [JsonProperty(PropertyName = "TIENE_EXPLICACION_TESIS")]
        public string HasExplanationThesis { get; set; }

        [Display(Name = "Tiene Extracto")]
        [JsonProperty(PropertyName = "TIENE_EXTRACTO")]
        public string HasExtract { get; set; }

        [Display(Name = "Tiene Método")]
        [JsonProperty(PropertyName = "TIENE_METODO")]
        public string HasMethod { get; set; }

        [Display(Name = "Tiene Nombre Despacho")]
        [JsonProperty(PropertyName = "TIENE_NOMBRE_DESPACHO")]
        public string HasDispatchName { get; set; }

        [Display(Name = "Tiene Nombre Magistrado Ponente")]
        [JsonProperty(PropertyName = "TIENE_NOMBRE_MAGISTRADO_PONENTE")]
        public string HasMagistrateSpeakerName { get; set; }

        [Display(Name = "Tiene Parte")]
        [JsonProperty(PropertyName = "TIENE_PARTE")]
        public string HasPart { get; set; }

        [Display(Name = "Tiene Problema Jurídico")]
        [JsonProperty(PropertyName = "TIENE_PROBLEMA_JURIDICO")]
        public string HasLegalProblem { get; set; }

        [Display(Name = "Tiene Salvamento Voto")]
        [JsonProperty(PropertyName = "TIENE_SALVAMENTO_VOTO")]
        public string HasVoteSalvage { get; set; }

        [Display(Name = "Tiene Tesis")]
        [JsonProperty(PropertyName = "TIENE_TESIS")]
        public string HasThesis { get; set; }
    }
}
