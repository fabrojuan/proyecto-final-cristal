using Microsoft.AspNetCore.Mvc;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using System.Collections.Generic;
using System.Linq;

namespace MVPSA_V2022.Controllers
{
    public class PruebaGraficaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("api/PruebaGrafica/ListarPruebasIniciales/{idDenuncia}")]
        public IEnumerable<PruebaImagenCLS> ListarPruebasIniciales(int idDenuncia)
        {
            List<PruebaImagenCLS> imagenesDenuncia;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                imagenesDenuncia = (from imagen in bd.PruebaGraficaDenuncia
                                    join denuncia in bd.Denuncia
                                    on imagen.NroDenuncia equals denuncia.NroDenuncia
                                    where denuncia.Bhabilitado == 1
                                                 && denuncia.NroDenuncia == idDenuncia
                                                 && imagen.IdUsuario == null

                                    select new PruebaImagenCLS
                                    {
                                        NroDenuncia = denuncia.NroDenuncia,
                                        Foto = imagen.Foto,
                                        NroImagen = imagen.NroImagen
                                    }).ToList();
                return imagenesDenuncia;
                //VOamos a crear otro controlador para la gestion de las imagenes....probaremos que onda
                // Debo crear un array con tres primeros elementos y luego copiarlos en cada objeto 
                //PgArray=(bd.PruebaGraficaDenuncia.Where(pg => pg.idDenuncia == idDenuncia && pg.IdUsuario==null ).First());
                // PruebaGrafi = bd.PruebaGraficaDenuncia.Where(pg => pg. == idDenuncia).[1];





                //    //REcuperamos la info.
                //    Rol oRol = bd.Rol.Where(p => p.IdRol == oRolCLS.IidRol).First();
                //    oRol.NombreRol = oRolCLS.NombreRol;
                //    oRol.Bhabilitado = 1;
                //    bd.SaveChanges();
                //    String[] idsPaginas = oRolCLS.Valores.Split("-");
                //    //ahora vamos a deshabilitar todas las paginas asociadas con este rol actualmente. 
                //    List<Paginaxrol> lista = bd.Paginaxrol.Where(p => p.IdRol == oRolCLS.IidRol).ToList();
                //    foreach (Paginaxrol pag in lista)
                //    {
                //        pag.Bhabilitado = 0;
                //    }
                //    //
                //    int cantidad;
                //    for (int i = 0; i < idsPaginas.Length; i++)
                //    {
                //        cantidad = lista.Where(p => p.IdPagina == int.Parse(idsPaginas[i])).Count();
                //        //si es = a 0 es porque no existe y debemos registrarlo
                //        if (cantidad == 0)
                //        {
                //            Paginaxrol oPaginaxrol = new Paginaxrol();
                //            oPaginaxrol.IdPagina = int.Parse(idsPaginas[i]);
                //            oPaginaxrol.IdRol = oRolCLS.IidRol;
                //            oPaginaxrol.Bhabilitado = 1;
                //            bd.Paginaxrol.Add(oPaginaxrol);
                //        }
                //        else
                //        {
                //            Paginaxrol PxR = lista.Where(p => p.IdPagina == int.Parse(idsPaginas[i])).First();
                //            PxR.Bhabilitado = 1;
                //        }
                //    }
                //    bd.SaveChanges();
                //    transaccion.Complete();
                //    rpta = 1;

                //}


            }
        }


    }
}
