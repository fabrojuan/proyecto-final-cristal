namespace MVPSA_V2022.Services {

    public interface INotificacionReclamoService {

        void notificarVecinoCambioEnReclamo(int nroReclamo, String nuevoEstado);

    }

}