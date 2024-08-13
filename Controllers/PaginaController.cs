using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVPSA_V2022.Controllers
{
    [Authorize]
    public class PaginaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/paginas")]
        public List<PaginaCLS> listarTodasPaginas()
        {
            List<PaginaCLS> listaPagina = new List<PaginaCLS>();

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                listaPagina = (from pagina in bd.Paginas
                               select new PaginaCLS
                               {
                                   idPagina = pagina.IdPagina,
                                   Mensaje = pagina.Mensaje!,
                                   Accion = pagina.Accion!
                               }).ToList();
                return listaPagina;
            }
        }

        [HttpPost]
        [Route("api/paginas")]
        public int guardarPagina([FromBody] PaginaCLS oPaginaCLS)
        {
            int rpta = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    if (oPaginaCLS.idPagina == 0) //SI es igual a cero es un nuevo objeto. 
                    {
                        Pagina oPagina = new Pagina();
                        oPagina.Mensaje = oPaginaCLS.Mensaje;
                        oPagina.Accion = oPaginaCLS.Accion;
                        oPagina.Bhabilitado = oPaginaCLS.Bhabilitado;
                        oPagina.Bvisible = oPaginaCLS.Bvisible;
                        bd.Paginas.Add(oPagina);
                        bd.SaveChanges();
                        rpta = 1;
                    }
                    else
                    {
                        //REcuperamos la info.
                        Pagina oPagina = bd.Paginas.Where(p => p.IdPagina == oPaginaCLS.idPagina).First();
                        oPagina.Accion = oPaginaCLS.Accion;
                        oPagina.Mensaje = oPaginaCLS.Mensaje;
                        oPagina.Bhabilitado = oPaginaCLS.Bhabilitado;
                        oPagina.Bvisible = oPaginaCLS.Bvisible;
                        bd.SaveChanges();
                        rpta = 1;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                rpta = 0;
            }
            return rpta;
        }

        [HttpGet]
        [Route("api/paginas/{idPagina}")]
        public PaginaCLS recuperarPagina(int idPagina)
        {
            PaginaCLS oPaginaCLS = new PaginaCLS();
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    oPaginaCLS = (from pagina in bd.Paginas
                                  where pagina.IdPagina == idPagina
                                  select new PaginaCLS
                                  {
                                      idPagina = pagina.IdPagina,
                                      Mensaje = pagina.Mensaje,
                                      Accion = pagina.Accion,
                                      Bvisible = (int)pagina.Bvisible,
                                      Bhabilitado = (int)pagina.Bhabilitado
                                  }).First();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                oPaginaCLS.Accion = null;
            }
            return oPaginaCLS;
        }

        [HttpDelete]
        [Route("api/paginas/{idPagina}")]
        public int eliminarPagina(int idPagina)
        {
            int rpta = 0;
            PaginaCLS oPaginaCLS = new PaginaCLS();
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    Pagina oPagina = bd.Paginas.Where(p => p.IdPagina == idPagina).First();
                    bd.Paginas.Remove(oPagina);
                    bd.SaveChanges();
                    rpta = 1;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                rpta = 0;
            }
            return rpta;
        }

    }
}
