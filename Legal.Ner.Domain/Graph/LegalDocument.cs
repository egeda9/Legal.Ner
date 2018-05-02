using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Legal.Ner.Domain.Graph
{
    public class LegalDocument : BaseSemanticGraph
    {
        [Display(Name = "Tiene Titulo")]
        [JsonProperty(PropertyName = "TIENE_TITULO")]
        public string HasTitle { get; set; }

        [Display(Name = "Tiene Datos Bibliográficos")]
        [JsonProperty(PropertyName = "TIENE_DATOS_BIBLIOGRAFICOS")]
        public string HasBibliographicData { get; set; }

        [Display(Name = "Tiene Fecha Publicación")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",
            ApplyFormatInEditMode = true)]
        [JsonProperty(PropertyName = "TIENE_FECHA_PUBLICACION")]
        public DateTime HasPublicationDate { get; set; }

        [Display(Name = "Tiene Tema")]
        [JsonProperty(PropertyName = "TIENE_TEMA")]
        public string HasSubject { get; set; }
    }
}
