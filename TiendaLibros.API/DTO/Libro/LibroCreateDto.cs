using System.ComponentModel.DataAnnotations;

namespace TiendaLibros.API.DTO.Libro
{
    public class LibroCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Titulo { get; set; }

        [Range(1800, int.MaxValue)]
        public int? Año { get; set; }

        [StringLength(20)]
        public string? ISBN { get; set; }

        [StringLength(300, MinimumLength =10)]
        public string? Resumen { get; set; }

        [StringLength(100)]
        public string? Imagen { get; set; }

        [Range(0,int.MaxValue)]
        public  decimal CostoVenta { get; set; }

    }
}
