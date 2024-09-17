namespace TiendaLibros.API.DTO.Libro
{
    public class LibroDto : BaseDto
    {
        public string Titulo { get; set; } = null!;
        

        public string? Imagen { get; set; }

        public decimal? CostoVenta { get; set; }

        public Guid AutorID { get; set; }

        public string? AutorNombre { get; set; }
    }
}
