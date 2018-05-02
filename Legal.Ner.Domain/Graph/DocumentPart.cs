﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Legal.Ner.Domain.Graph
{
    public class DocumentPart : BaseSemanticGraph
    {
        [Display(Name = "Tiene Descripción")]
        [JsonProperty(PropertyName = "TIENE_DESCRIPCION")]
        public string HasDescription { get; set; }
    }
}