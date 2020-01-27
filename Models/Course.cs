using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListaCursos.Models
{
    public class Course
    {        
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del curso es requerido.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Descripcion")]
        [MaxLength(300, ErrorMessage = "La descripcion no puede superar los 300 caracteres.")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "El campo autor es requerido.")]
        [Display(Name = "Author")]
        public string Author { get; set; }

        [Url(ErrorMessage = "Ingrese una dirección válida.")]
        [Display(Name = "Dirección del curso")]
        public string Uri { get; set; }

    }
}
