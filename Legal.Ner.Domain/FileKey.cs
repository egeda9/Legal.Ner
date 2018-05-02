using System;
using System.ComponentModel.DataAnnotations;

namespace Legal.Ner.Domain
{
    public class FileKey
    {
        public int Id { get; set; }

        [Display(Name = "Nombre de Archivo")]
        public string FileName { get; set; }

        [Display(Name = "Fecha de Carga")]
        public DateTime UploadDate { get; set; }

        [Display(Name = "Título del Documento")]
        public string DocumentTitle { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "Fecha de Publicación")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",
            ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Número de Norma")]
        public long Number { get; set; }
    }
}
