using AutoMapper;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Mappers
{
    public class ReclamoProfile : Profile
    {
        public ReclamoProfile()
        {
            //CreateMap<ReclamoCLS, Reclamo>() .ForMember(dest => dest.ConfirmEmail, opt => opt.MapFrom(src => src.Email));
            CreateMap<ReclamoCLS, Reclamo>();
            CreateMap<Reclamo, ReclamoCLS>();
        }
    }
}
