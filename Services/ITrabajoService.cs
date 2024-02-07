using MVPSA_V2022.clases;

namespace MVPSA_V2022.Services
{
    public interface ITrabajoService
    {   
        public IEnumerable<TrabajoCLS> ListarTrabajosDenunciasCerradas(int idDenuncia);
    }
}
