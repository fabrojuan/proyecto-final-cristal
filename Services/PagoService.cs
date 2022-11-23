using AutoMapper;
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
            validarPago(pagoDto);
            generarPago(pagoDto);                           
        }

        private void validarPago(PagoCLS pagoDto)
        {
            int IdBoleta = (int)System.Convert.ToInt64(pagoDto.data.payment.reference);

            if (!existeBoleta(IdBoleta))
            {
                throw new PagoNoValidoException("La boleta informada no existe.");
            }
        }

        private void generarPago(PagoCLS pagoDto) {

            MobbexPago mobbexPago = _mapper.Map<MobbexPago>(pagoDto);

            Pago pago = new Pago();
            pago.IdTipoPago = getIdTipoPago(pagoDto.data.view.type);
            pago.IdBoleta = (int?)System.Convert.ToInt64(pagoDto.data.payment.reference);
            pago.Importe = pagoDto.data.payment.total;
            pago.Bhabilitado = 1;

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
             //   bd.MobbexPagos.Add(mobbexPago);
                bd.Pagos.Add(pago);
                bd.SaveChanges();
            }

            String email = pagoDto.data.customer.email;
            generarYEnviarRecibo(pago, email);
        }

        

        private void generarYEnviarRecibo(Pago pago, String email) {

            Recibo recibo = new Recibo();
            recibo.IdBoleta = pago.IdBoleta;
            recibo.Bhabilitado = 1;
            recibo.FechaPago = pago.FechaDePago;
            recibo.Importe = pago.Importe;
            recibo.Mail = email; 

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                bd.Recibos.Add(recibo);
                bd.SaveChanges();
            }

            enviarRecibo(recibo);
        }

        private void enviarRecibo(Recibo recibo) {
            MailRequest mailRequest = new MailRequest();
            mailRequest.ToEmail = recibo.Mail;
            mailRequest.Subject = "Recibo de Pago Realizado";
            mailRequest.Body = generarBodyEmailRecibo(recibo);
            _mailService.SendEmailAsync(mailRequest);
        }

        private string generarBodyEmailRecibo(Recibo recibo) {
            string bodyMail = "<body><p>Estimado vecino,</p><p>Se ha emitido el recibo nro. #reciboNro, por el importe de " +
                "$#importe, correspondiente a la boleta nro. #boletaNro.</p><p>Muchas gracias.</p><br><p style=\"color:green\">" +
                "<b>Comuna Villa Parque Santa Ana</b></p></body>";

            bodyMail = bodyMail.Replace("#reciboNro", recibo.IdRecibo.ToString());
            bodyMail = bodyMail.Replace("#importe", recibo.Importe.ToString());
            bodyMail = bodyMail.Replace("#boletaNro", recibo.IdBoleta.ToString());

            return bodyMail;
        }
        

        // ToDo Mover este metodo a una clase que tenga metodos relacionados con las boletas
        private Boolean existeBoleta(int idBoleta)
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                if (bd.Boleta.Where(bol => bol.IdBoleta == idBoleta).Count() > 0) { return true; }
                return false;
            }
        }

        private int getIdTipoPago(string tipoPagoMobbex) {
            if ("cash".Equals(tipoPagoMobbex))
            {
                return 1;
            }
            else if ("card".Equals(tipoPagoMobbex))
            {
                return 2;
            }
            else {
                return 3;
            }
        }

    }
}
