using AutoMapper;

namespace MVPSA_V2022.Mappers
{
    public class MobbexPagoProfile : Profile
    {
        public MobbexPagoProfile()
        {
            CreateMap<clases.Mobbex.PagoCLS, Modelos.MobbexPago>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.type))
                .ForMember(dest => dest.ViewType, opt => opt.MapFrom(src => src.data.view.type))
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.data.payment.id))
                .ForMember(dest => dest.PaymentStatusCode, opt => opt.MapFrom(src => src.data.payment.status.code))
                .ForMember(dest => dest.PaymentStatusText, opt => opt.MapFrom(src => src.data.payment.status.text))
                .ForMember(dest => dest.PaymentStatusMessage, opt => opt.MapFrom(src => src.data.payment.status.message))
                .ForMember(dest => dest.PaymentTotal, opt => opt.MapFrom(src => src.data.payment.total))
                .ForMember(dest => dest.PaymentCurrencyCode, opt => opt.MapFrom(src => src.data.payment.currency.code))
                .ForMember(dest => dest.PaymentCurrencySymbol, opt => opt.MapFrom(src => src.data.payment.currency.symbol))
                .ForMember(dest => dest.PaymentCreated, opt => opt.MapFrom(src => src.data.payment.created))
                .ForMember(dest => dest.PaymentUpdated, opt => opt.MapFrom(src => src.data.payment.updated))
                .ForMember(dest => dest.PaymentReference, opt => opt.MapFrom(src => src.data.payment.reference))
                .ForMember(dest => dest.CustomerUid, opt => opt.MapFrom(src => src.data.customer.uid))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.data.customer.email))
                .ForMember(dest => dest.CheckoutUid, opt => opt.MapFrom(src => src.data.checkout.uid));

        }
    }
}
