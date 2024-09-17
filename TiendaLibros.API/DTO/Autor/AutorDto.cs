using System.ComponentModel.DataAnnotations;

namespace TiendaLibros.API.DTO.Autor
{
    public class AutorDto : BaseDto
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
