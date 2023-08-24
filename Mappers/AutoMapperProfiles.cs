using AutoMapper;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            //CreateMap<PrioridadReclamoDto, PrioridadReclamo>().ReverseMap();
            //CreateMap<ReclamoDto, Reclamo>().ReverseMap();
            //CreateMap<CrearReclamoRequestDto, Reclamo>();

            //CreateMap<TipoReclamoDto, TipoReclamo>()
            //    .ReverseMap()
             //   .ForMember(dest => dest.usuarioAlta, act => act.MapFrom(src => src.IdUsuarioAltaNavigation.NombreUser))
             //   .ForMember(dest => dest.usuarioModificacion, act => act.MapFrom(src => src.IdUsuarioModificacionNavigation.NombreUser));

            //CreateMap<UsuarioCLS, Usuario>().ReverseMap();
        }
    }
}
