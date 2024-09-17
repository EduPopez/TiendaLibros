using AutoMapper;
using TiendaLibros.API.DTO;
using TiendaLibros.API.Models;

namespace TiendaLibros.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AutorCreateDto, Autor>().ReverseMap();
            CreateMap<AutorDto, Autor>().ReverseMap();
            CreateMap<AutorUpdateDto, Autor>().ReverseMap();
        }
    }
}
