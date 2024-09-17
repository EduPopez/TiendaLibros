using AutoMapper;
using Microsoft.Build.Framework;
using TiendaLibros.API.DTO.Autor;
using TiendaLibros.API.DTO.Libro;
using TiendaLibros.API.Models;

namespace TiendaLibros.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Primero es la Fuente y Luego el destino
            CreateMap<AutorCreateDto, Autor>().ReverseMap();
            CreateMap<AutorDto, Autor>().ReverseMap();
            CreateMap<AutorUpdateDto, Autor>().ReverseMap();

            CreateMap<Libro, LibroDto>()
                .ForMember(dest => dest.AutorNombre, src => src.MapFrom( x=> string.Format("{0}{1}", x.Autor.Nombres, x.Autor.Apellidos).Trim()))
                .ReverseMap();

            CreateMap<LibroCreateDto, Libro>().ReverseMap();
        }
    }
}
