using System.ComponentModel.DataAnnotations;

namespace TiendaLibros.API.DTO
{
    public class AutorCreateDto
    {
        [Required]
        [StringLength(50)]
        public string Nombres { get; set; } = null!;

        [StringLength(50)]
        public string? Apellidos { get; set; }

        [StringLength(300)]
        public string? Biografia { get; set; }
    }
}
