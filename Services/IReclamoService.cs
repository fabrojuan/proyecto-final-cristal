using MVPSA_V2022.clases;

namespace MVPSA_V2022.Services
{
    public interface IReclamoService
    {
        public ReclamoDto guardarReclamo(CrearReclamoRequestDto reclamoCLS, int idVecinoAlta);

        public IEnumerable<ReclamoDto> listarReclamos(int usuarioConsulta, int tipo, int estado, int numero, String nomApeVecino);

        public ReclamoDto getReclamo(int nroReclamo);

        public ReclamoDto modificarReclamo(int nroReclamo, ModificarReclamoRequestDto reclamoDto);

        public IEnumerable<TipoReclamoDto> listarTiposReclamo();

        public TipoReclamoDto getTipoReclamo(int codTipoReclamo);

        public void eliminarTipoReclamo(int codTipoReclamoEliminar);

        public TipoReclamoDto guardarTipoReclamo(TipoReclamoDto tipoReclamoDto, int idUsuarioAlta);

        public TipoReclamoDto modificarTipoReclamo(TipoReclamoDto tipoReclamoDto, int idUsuarioModificacion);

        public IEnumerable<PrioridadReclamoDto> getPrioridades();

        public void aplicarAccion(AplicarAccionDto aplicarAccionDto);

        public List<ObservacionReclamoDto> obtenerObservacionesDeReclamo(int nroReclamo);

        public List<EstadoReclamoDto> getEstadosReclamo();

        public OpcionesReclamoDto getOpcionesReclamo(int numeroReclamo, int idUsuario);

    }        
}
