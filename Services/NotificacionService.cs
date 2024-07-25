using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Services {

    public class NotificacionService : INotificacionService
    {
        private readonly M_VPSA_V3Context dbContext;

        public NotificacionService(M_VPSA_V3Context dbContext)
        {
            this.dbContext = dbContext;
        }


        public void notificarPorEmail(string destinatarios, string destinatariosCC, string asunto, string mensaje)
        {
            var email = new EmailQueue();
            email.EmailSubject = asunto;            
            email.Recipients = destinatarios;
            email.CcRecipients = destinatariosCC;
            email.EmailBody = mensaje;
            email.QueueTime = DateTime.Now;

            this.dbContext.EmailQueues.Add(email);
            this.dbContext.SaveChanges();
        }
    }

}