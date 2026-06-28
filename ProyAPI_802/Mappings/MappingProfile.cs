using AutoMapper;
using ProyAPI_802.DTOs;
using ProyAPI_802.Models;

namespace ProyAPI_802.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Producto, ProductoDto>()
                .ForMember(dest => dest.CategoriaNombre,
                           opt => opt.MapFrom(src => src.objCategoria.Nombre));

            CreateMap<ProductoCreateDto, Producto>();

            CreateMap<Categoria, CategoriaDto>();
        }
    }
}