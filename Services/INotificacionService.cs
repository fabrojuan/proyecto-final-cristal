namespace MVPSA_V2022.Services {

    public interface INotificacionService {

        void notificarPorEmail(String destinatarios, String destinatariosCC, String asunto, String mensaje);

    }

}