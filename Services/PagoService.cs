using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVPSA_V2022.clases.Mobbex;
using MVPSA_V2022.Exceptions;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Services
{
    public class PagoService : IPagoService
    {
        public readonly IMapper _mapper;
        public readonly IMailService _mailService;

        public PagoService(IMapper mapper, IMailService mailService)
        {
            _mapper = mapper;
            _mailService = mailService;
        }

        public void registrarPagoMobbex(PagoCLS pagoDto)
        {
            generarPago(pagoDto);                           
        }

        private void generarPago(PagoCLS pagoDto) {

            try
            {
                MobbexPago mobbexPago = _mapper.Map<MobbexPago>(pagoDto);

                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    bd.MobbexPagos.Add(mobbexPago);

                    int boletaId = Int32.Parse(mobbexPago.PaymentReference);
                    string email = mobbexPago.CustomerEmail;
                    decimal fechaPago = Convert.ToDecimal(mobbexPago.PaymentCreated);

                    SqlParameter parameterBoletaId = new SqlParameter("@IdBoleta_par", boletaId);
                    SqlParameter parameterEmail = new SqlParameter("@mail", email);
                    SqlParameter parameterFechaPago = new SqlParameter("@FechaPago", fechaPago);
                    bd.Database.ExecuteSqlRaw("ACTUALIZACION_DETALLES_E_IMPUESTOS @IdBoleta_par, @mail, @FechaPago " +
                                                  parameterBoletaId, parameterEmail, parameterFechaPago);

                    bd.SaveChanges();
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error al registrar un pago de mobex con Checkout UID " 
                    + pagoDto.data.checkout.uid + ". Error: " + ex.Message);
                throw new PagoNoValidoException("Ha ocurrido un error al registrar el pago con Checkout UID " 
                    + pagoDto.data.checkout.uid);
            }            

        }                

    }
}
