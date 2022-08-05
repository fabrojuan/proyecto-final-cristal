using AutoMapper;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Services
{
    public class ReclamoService : IReclamoService
    {
        public readonly IMapper _mapper;

        public ReclamoService(IMapper mapper) {
            _mapper = mapper;
        }

        public int guardarReclamo(ReclamoCLS reclamoCLS)
        {
            int rpta = 0;
            try
            {
                //Reclamo reclamo = _mapper.Map<Reclamo>(reclamoCLS);
                //reclamo.CodEstadoReclamo = 1;
                //reclamo.Bhabilitado = 1;
                //reclamo.Fecha = null;

                Reclamo reclamo = new Reclamo();
                reclamo.CodTipoReclamo = reclamoCLS.codTipoReclamo;
                reclamo.Calle = reclamoCLS.calle;
                reclamo.Altura = reclamoCLS.altura;
                reclamo.EntreCalles = reclamoCLS.entreCalles;
                reclamo.CodEstadoReclamo = 1;
                reclamo.Descripcion = reclamoCLS.descripcion;
                reclamo.Bhabilitado = 1;
                reclamo.IdVecino = reclamoCLS.idVecino;

                using (M_VPSA_V3Context bd = new M_VPSA_V3Context()) {
                    bd.Reclamos.Add(reclamo);
                    bd.SaveChanges();
                }
                rpta = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                rpta = 0;
            }
            return rpta;
        }

        public IEnumerable<ReclamoCLS> listarReclamos()
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                List<ReclamoCLS> listaReclamo = (from reclamo in bd.Reclamos
                                                 join estadoReclamo in bd.EstadoReclamos
                                                 on reclamo.CodEstadoReclamo equals estadoReclamo.CodEstadoReclamo
                                                 join tipoReclamo in bd.TipoReclamos
                                                 on reclamo.CodTipoReclamo equals tipoReclamo.CodTipoReclamo
                                                 where reclamo.Bhabilitado == 1
                                                 select new ReclamoCLS
                                                 {
                                                     nroReclamo = reclamo.NroReclamo,
                                                     Fecha = (DateTime)reclamo.Fecha,
                                                     estadoReclamo = estadoReclamo.Nombre,
                                                     tipoReclamo = tipoReclamo.Nombre,
                                                 }).ToList();
                return listaReclamo;
            }
        }

        public IEnumerable<TipoReclamoCLS> listarTiposReclamo()
        {
            List<TipoReclamoCLS> listaTiposReclamo;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                listaTiposReclamo = (from tipoReclamo in bd.TipoReclamos
                                    where tipoReclamo.Bhabilitado == 1
                                    select new TipoReclamoCLS
                                    {
                                        cod_Tipo_Reclamo = tipoReclamo.CodTipoReclamo,
                                        nombre = tipoReclamo.Nombre,
                                        descripcion = tipoReclamo.Descripcion
                                    }).ToList();
                return listaTiposReclamo;
            }
        }
    }
}
