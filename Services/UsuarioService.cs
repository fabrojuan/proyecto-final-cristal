using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Services
{
    public class UsuarioService : IUsuarioService
    {
        public UsuarioCLS findUsuarioById(int idUsuario)
        {
            UsuarioCLS usuarioResponse = new UsuarioCLS();
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context()) {
                Usuario usuario = bd.Usuarios.Where(usr => usr.IdUsuario == idUsuario).First();

                if (usuario != null) {
                    usuarioResponse.IdUsuario = usuario.IdUsuario;
                    usuarioResponse.NombreUser = usuario.NombreUser;
                }
            }

            return usuarioResponse;
        }
    }
}
