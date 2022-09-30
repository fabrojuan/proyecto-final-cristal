using MVPSA_V2022.clases;

namespace MVPSA_V2022.Services
{
    public interface IJwtAuthenticationService
    {
        public LoginResponseCLS getToken(int idUsuario);
    }
}
