using MVPSA_V2022.clases;

namespace MVPSA_V2022.Services
{
    public interface IUsuarioService
    {
        public UsuarioCLS findUsuarioById(int idUsuario);
    }
}
