using MVPSA_V2022.clases;

namespace MVPSA_V2022.Services
{
    public interface IReclamoService
    {
        public int guardarReclamo(ReclamoCLS reclamoCLS);

        public IEnumerable<ReclamoCLS> listarReclamos();

        public IEnumerable<TipoReclamoCLS> listarTiposReclamo();

        public TipoReclamoCLS getTipoReclamo(int codTipoReclamo);

        public void eliminarTipoReclamo(int codTipoReclamoEliminar);

        public TipoReclamoCLS guardarTipoReclamo(TipoReclamoCLS tipoReclamoDto);

        public TipoReclamoCLS modificarTipoReclamo(TipoReclamoCLS tipoReclamoDto);

    }        
}
