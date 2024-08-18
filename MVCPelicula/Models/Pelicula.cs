using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCPelicula.Models
{
    public class Pelicula
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "El campo titulo es requerido")]
        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Display(Name = "Fecha de Lanzamiento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo fecha de lanzamiento es requerido")]
        public DateTime FechaLanzamiento { get; set; }

        //Propiedad para la llave foranea
        [Required]
        [ForeignKey("Genero")]
        [Display(Name = "Genero")]
        public int? GeneroId { get; set; }

        //Propiedad de navegacion
        public Genero Genero { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        [Required(ErrorMessage = "El campo precio es requerido")]
        public decimal Precio { get; set; }

        [StringLength(250)] 
        [Required(ErrorMessage = "El campo director es requerido")]
        public string Director { get; set; }
    }
}
