using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace MVPSA_V2022.Controllers
{
    [Authorize]
    public class RolController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //EL rol ya esta listado desde el usuario controller voy a listarlo tambien de aqui para modificar el rol. 
        [HttpGet]
        [Route("api/roles")]
        public List<RolCLS> listarRoles()
        {
            List<RolCLS> listaRol;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                listaRol = (from Rol in bd.Rols
                            //where Rol.Bhabilitado == 1
                            select new RolCLS
                            {
                                IidRol = Rol.IdRol,
                                NombreRol = Rol.NombreRol,
                                BHabilitado = (int)Rol.Bhabilitado,
                                TipoRol = Rol.TipoRol,
                                CodRol = Rol.CodRol
                            }).ToList();
                return listaRol;
            }
        }

        [HttpGet]
        [Route("api/roles/paginas")]
        public List<PaginaCLS> listarPaginasTipoRol()
        {
            List<PaginaCLS> listaPagina = new List<PaginaCLS>();

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                listaPagina = (from pagina in bd.Paginas
                               where pagina.Bhabilitado == 1
                               select new PaginaCLS
                               {
                                   idPagina = pagina.IdPagina,
                                   Mensaje = pagina.Mensaje,
                               }).ToList();
                return listaPagina;
            }
        }
        [HttpGet]
        [Route("api/roles/{idRol}/paginas")]
        public RolCLS listarPaginasRecuperar(int idRol)
        {
            RolCLS oUsuarioRolCLS = new RolCLS();

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                List<PaginaCLS> listaPaginas = (from rolUsuario in bd.Rols
                                                join paginaRol in bd.Paginaxrols
                                                on rolUsuario.IdRol equals paginaRol.IdRol
                                                join pagina in bd.Paginas
                                                on paginaRol.IdPagina equals pagina.IdPagina
                                                where paginaRol.IdRol == idRol && pagina.Bhabilitado == 1
                                                select new PaginaCLS
                                                {
                                                    idPagina = pagina.IdPagina,
                                                }).ToList();
                Rol oRol = bd.Rols.Where(prop => prop.IdRol == idRol).First();
                oUsuarioRolCLS.IidRol = oRol.IdRol;
                oUsuarioRolCLS.NombreRol = oRol.NombreRol;
                oUsuarioRolCLS.BHabilitado = (int)oRol.Bhabilitado;
                oUsuarioRolCLS.CodRol = oRol.CodRol;
                oUsuarioRolCLS.TipoRol = oRol.TipoRol;
                oUsuarioRolCLS.ListaPagina = listaPaginas;

                return oUsuarioRolCLS;
            }
        }

        [HttpPost]
        [Route("api/roles")]
        public ActionResult guardarROL([FromBody] RolCLS oRolCLS)
        {
            int rpta = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    //Usaremos transacciones porque tocaremos dos tablas imultaneamente.
                    using (var transaccion = new TransactionScope())
                    {
                        if (oRolCLS.IidRol == 0)
                        {
                            Rol oRol = new Rol();
                            oRol.NombreRol = oRolCLS.NombreRol;
                            oRol.Bhabilitado = oRolCLS.BHabilitado;
                            oRol.CodRol = oRolCLS.CodRol.ToUpper();
                            oRol.TipoRol = oRolCLS.TipoRol;
                            bd.Rols.Add(oRol);
                            bd.SaveChanges();

                            if (!oRolCLS.Valores.IsNullOrEmpty()) {
                                String[] idsPaginas = oRolCLS.Valores.Split("-"); //Separo los valores obtenidos enla variable que tienen el guion como separador y los transformo en un array de objetos separados por coma.
                                for (int i = 0; i < idsPaginas.Length; i++)
                                {
                                    Paginaxrol oPaginaxrol = new Paginaxrol();
                                    oPaginaxrol.IdPagina = int.Parse(idsPaginas[i]);
                                    oPaginaxrol.IdRol = oRol.IdRol;
                                    oPaginaxrol.Bhabilitado = 1;
                                    bd.Paginaxrols.Add(oPaginaxrol);
                                }
                            }
                            
                            bd.SaveChanges();
                            transaccion.Complete();
                            rpta = 1;
                        }
                        else
                        {
                            //REcuperamos la info.
                            Rol oRol = bd.Rols.Where(p => p.IdRol == oRolCLS.IidRol).First();
                            oRol.NombreRol = oRolCLS.NombreRol;
                            oRol.CodRol = oRolCLS.CodRol.ToUpper();
                            oRol.TipoRol = oRolCLS.TipoRol;
                            oRol.Bhabilitado = oRolCLS.BHabilitado;
                            //bd.SaveChanges();

                            if (!oRolCLS.Valores.IsNullOrEmpty()) {
                                String[] idsPaginas = oRolCLS.Valores.Split("-");
                                //ahora vamos a deshabilitar todas las paginas asociadas con este rol actualmente. 
                                List<Paginaxrol> lista = bd.Paginaxrols.Where(p => p.IdRol == oRolCLS.IidRol).ToList();
                                foreach (Paginaxrol pag in lista)
                                {
                                    pag.Bhabilitado = 0;
                                }

                                //
                                int cantidad;
                                for (int i = 0; i < idsPaginas.Length; i++)
                                {
                                    cantidad = lista.Where(p => p.IdPagina == int.Parse(idsPaginas[i])).Count();
                                    //si es = a 0 es porque no existe y debemos registrarlo
                                    if (cantidad == 0)
                                    {
                                        Paginaxrol oPaginaxrol = new Paginaxrol();
                                        oPaginaxrol.IdPagina = int.Parse(idsPaginas[i]);
                                        oPaginaxrol.IdRol = oRolCLS.IidRol;
                                        oPaginaxrol.Bhabilitado = 1;
                                        bd.Paginaxrols.Add(oPaginaxrol);
                                    }
                                    else
                                    {
                                        Paginaxrol PxR = lista.Where(p => p.IdPagina == int.Parse(idsPaginas[i])).First();
                                        PxR.Bhabilitado = 1;
                                    }
                                }
                            }
                          
                            bd.SaveChanges();
                            transaccion.Complete();
                            rpta = 1;

                        }
                    }

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete]
        [Route("api/roles/{idRol}")]
        public int eliminarRol(int idRol)
        {
            int rpta = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    Rol oRol = bd.Rols.Where(p => p.IdRol == idRol).First();
                    oRol.Bhabilitado = 0;
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
