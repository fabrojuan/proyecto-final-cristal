using Org.BouncyCastle.Asn1.Bsi;

namespace MVPSA_V2022.Services {

    public interface INotificacionVecinoService {

        void notificarVecinoCambioEstadoReclamo(int nroReclamo);

        void notificarRecuperacionCuenta(int idRecuperacionCuenta);

    }

}