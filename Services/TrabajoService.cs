using AutoMapper;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Services
{
    public class TrabajoService :ITrabajoService
    {
        public readonly IMapper mapper;
        //public readonly ITrabajoService trabajoService ;
        private readonly M_VPSA_V3Context dbContext;

        public readonly IUsuarioService usuarioService;
     
        public TrabajoService(IMapper mapper, IUsuarioService usuarioService,
            M_VPSA_V3Context dbContext){
            this.mapper = mapper;
            this.usuarioService = usuarioService;
            this.dbContext = dbContext;
        }




        public IEnumerable<TrabajoCLS> ListarTrabajosDenunciasCerradas(int idDenuncia)
        {
            List<TrabajoCLS> listaTrabajo;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    Usuario oUsuario = new Usuario();
                    listaTrabajo = (from trabajo in bd.Trabajos
                                    join denuncia in bd.Denuncia
                                     on trabajo.NroDenuncia equals denuncia.NroDenuncia
                                    join usuario in bd.Usuarios
                                   on trabajo.IdUsuario equals usuario.IdUsuario
                                    where denuncia.Bhabilitado == 0
                                                 && denuncia.NroDenuncia == idDenuncia

                                    select new TrabajoCLS
                                    {
                                        Fecha = (DateTime)trabajo.Fecha,
                                        Nro_Denuncia = trabajo.NroDenuncia,
                                        Descripcion = !String.IsNullOrEmpty(trabajo.Descripcion) ? trabajo.Descripcion : "No Posee",
                                        Id_Usuario = (int)trabajo.IdUsuario,
                                        //oUsuario = bd.Usuarios.Where(d => d.IdUsuario == trabajo.IdUsuario).First(),
                                        ApellidoEmpleado = !String.IsNullOrEmpty(usuario.NombreUser) ? usuario.NombreUser : "Sin nombre",
                                        //oUsuario.NombreUser,
                                        //  int NroDenunciaTemp = oDenuncia.NroDenuncia,
                                        Nro_Trabajo = trabajo.NroTrabajo
                                    }).ToList();
                    return listaTrabajo;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                listaTrabajo = new List<TrabajoCLS>();
                return listaTrabajo;
            }

        }


    }
}
