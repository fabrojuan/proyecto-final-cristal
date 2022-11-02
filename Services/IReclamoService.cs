using MVPSA_V2022.clases;

namespace MVPSA_V2022.Services
{
    public interface IReclamoService
    {
        public ReclamoCLS guardarReclamo(ReclamoCLS reclamoCLS, int idVecinoAlta);

        public IEnumerable<ReclamoCLS> listarReclamos();

        public IEnumerable<TipoReclamoCLS> listarTiposReclamo();

        public TipoReclamoCLS getTipoReclamo(int codTipoReclamo);

        public void eliminarTipoReclamo(int codTipoReclamoEliminar);

        public TipoReclamoCLS guardarTipoReclamo(TipoReclamoCLS tipoReclamoDto, int idUsuarioAlta);

        public TipoReclamoCLS modificarTipoReclamo(TipoReclamoCLS tipoReclamoDto, int idUsuarioModificacion);

    }        
}
