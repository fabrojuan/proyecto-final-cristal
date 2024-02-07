using MVPSA_V2022.clases;

namespace MVPSA_V2022.Services
{
    public interface IDenunciaService
    {

        public IEnumerable<TipoDenunciaCLS> listarTiposDenuncia();

        public TipoDenunciaCLS getTipoDenuncia(int codTipoDenuncia);

        public void eliminarTipoDenuncia(int codTipoDenunciaEliminar);

        public TipoDenunciaCLS guardarTipoDenuncia(TipoDenunciaCLS tipoDenunciaDto, int idUsuarioAlta);

        public TipoDenunciaCLS modificarTipoDenuncia(TipoDenunciaCLS tipoDenunciaDto, int idUsuarioModificacion);

        public IEnumerable<DenunciaCLS2> ListarDenunciasCerradas();
    }
}