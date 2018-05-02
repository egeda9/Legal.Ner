﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Legal.Ner.Domain.Graph
{
    public class BaseSemanticGraph
    {
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }
        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "comment")]
        public string Comment { get; set; }
        [JsonProperty(PropertyName = "nodeLabels")]
        public string NodeLabels { get; set; }

        [JsonIgnore]
        [Display(Name = "Clases de Origen")]
        public string SourceClasses { get; set; }
        [JsonIgnore]
        public string Filter { get; set; }

        [Display(Name = "Tiene Número")]
        [JsonProperty(PropertyName = "TIENE_NUMERO")]
        public int HasNumber { get; set; }
    }
}