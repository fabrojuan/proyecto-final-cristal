using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using MVPSA_V2022.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVPSA_V2022.Controllers
{
    [ApiController]
    [Route("api/Sugerencia/")]
    [Authorize]
    public class SugerenciaController : Controller
    {

        private readonly M_VPSA_V3Context dbContext;

        public SugerenciaController(M_VPSA_V3Context dbContext) {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("api/Sugerencia/ListarTiposReclamo")]
        public IEnumerable<TipoReclamoDto> ListarTiposReclamo()
        {
            List<TipoReclamoDto> listaTipoReclamo;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                listaTipoReclamo = (from tipoReclamo in bd.TipoReclamos
                                    where tipoReclamo.Bhabilitado == 1
                                    select new TipoReclamoDto
                                    {
                                        cod_Tipo_Reclamo = tipoReclamo.CodTipoReclamo,
                                        nombre = tipoReclamo.Nombre,
                                        descripcion = tipoReclamo.Descripcion
                                    }).ToList();
                return listaTipoReclamo;
            }

        }

        [HttpPost]
        [Route("guardarSugerencia")]
        [AllowAnonymous]
        public int guardarSugerencia([FromBody] SugerenciaCLS sugerenciaCLS)
        {
            int rpta = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    Sugerencium oSugerencia = new Sugerencium();
                    oSugerencia.Descripcion = sugerenciaCLS.descripcion;
                    oSugerencia.Bhabilitado = 1;
                    oSugerencia.Estado = 1;
                    bd.Sugerencia.Add(oSugerencia);
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
        [HttpGet]
        [Route("listarSugerencias")]
        public IEnumerable<SugerenciaCLS> listarSugerencias()
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                List<SugerenciaCLS> listaSugerencia = (from sugerencia in bd.Sugerencia join esugere in bd.EstadoSugerencia
                                                             on sugerencia.Estado equals esugere.CodEstadoSugerencia
                                                       where sugerencia.Bhabilitado == 1 && sugerencia.Estado == esugere.CodEstadoSugerencia
                                                       select new SugerenciaCLS
                                                       {
                                                           idSugerencia = sugerencia.IdSugerencia,
                                                           fechaGenerada = (DateTime)sugerencia.FechaGenerada,
                                                           descripcion = !String.IsNullOrEmpty(sugerencia.Descripcion) ? sugerencia.Descripcion : "No Posee",
                                                           estado= !String.IsNullOrEmpty(esugere.Nombre) ? esugere.Nombre : "No Posee"
            }).ToList();
                return listaSugerencia;
            }

        }

        [HttpGet]
        [Route("{idSugerencia}")]
        public SugerenciaCLS getSugerencia(int idSugerencia)
        {

            var sugerencia = this.dbContext.Sugerencia.Where(sug => sug.IdSugerencia == idSugerencia).FirstOrDefault();

            if (sugerencia == null) {
                return null;
            }
            
            return Conversor.convertToSugerenciaCLS(sugerencia);

        }

    }
}
