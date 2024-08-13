using MVPSA_V2022.clases;

namespace MVPSA_V2022.Services
{
    public interface ITrabajoReclamoService
    {
        public void guardarTrabajo(TrabajoReclamoCreacionRequestDto trabajoDto, int idUsuarioAlta);

        public List<TrabajoReclamoDto> obtenerTrabajosReclamo(int nroReclamo);
    }        
}
