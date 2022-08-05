using MVPSA_V2022.clases;

namespace MVPSA_V2022.Services
{
    public interface IReclamoService
    {
        public IEnumerable<TipoReclamoCLS> listarTiposReclamo();

        public int guardarReclamo(ReclamoCLS reclamoCLS);

        public IEnumerable<ReclamoCLS> listarReclamos();

    }        
}
