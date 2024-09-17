using System;
using System.Collections.Generic;

namespace TiendaLibros.API.Models;

public partial class Autor
{

    public Autor()
    {
        //Libros = new HashSet<Libro>();
    }

    public Guid Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string? Apellidos { get; set; }

    public string? Biografia { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
