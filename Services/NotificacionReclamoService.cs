using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Services {

    public class NotificacionReclamoService : INotificacionReclamoService
    {
        public readonly IReclamoService reclamoService;
        private readonly M_VPSA_V3Context dbContext;

        public NotificacionReclamoService(IReclamoService reclamoService, M_VPSA_V3Context dbContext)
        {
            this.reclamoService = reclamoService;
            this.dbContext = dbContext;
        }

        public void notificarVecinoCambioEnReclamo(int nroReclamo, string nuevoEstado)
        {
            var reclamo = this.dbContext.Reclamos.Where(reclamo => reclamo.NroReclamo == nroReclamo).FirstOrDefault();

            throw new NotImplementedException();
        }
    }

}