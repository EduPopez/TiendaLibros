using System;
using System.Collections.Generic;

namespace TiendaLibros.API.Models;

public partial class Libro
{
    public Guid Id { get; set; }

    public string Titulo { get; set; } = null!;

    public int? Año { get; set; }

    public string Isbn { get; set; } = null!;

    public string? Resumen { get; set; }

    public string? Imagen { get; set; }

    public decimal? CostoVenta { get; set; }

    public Guid? AutorGuid { get; set; }

    public virtual Autor? Autor { get; set; }
}
