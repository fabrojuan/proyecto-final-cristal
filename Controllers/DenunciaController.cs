using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVPSA_V2022.clases;
using MVPSA_V2022.Exceptions;
using MVPSA_V2022.Modelos;
using MVPSA_V2022.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace MVPSA_V2022.Controllers
{
    [ApiController]
    [Route("api/Denuncia/")]
   [Authorize]
    public class DenunciaController : Controller
    {

        private readonly IDenunciaService denunciaService;

        public DenunciaController(IDenunciaService denunciaService, IUsuarioService usuarioService)
        {
            this.denunciaService = denunciaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("listarDenuncias")]
        public IEnumerable<DenunciaCLS2> listarDenuncias()
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                Prioridad oPriorodad = new Prioridad();
                UsuarioCLS usuarioCLS = new UsuarioCLS();

                List<DenunciaCLS2> listaDenuncia = (from Denuncia in bd.Denuncia
                                                    join EstadoDenuncia in bd.EstadoDenuncia
                                                   on Denuncia.CodEstadoDenuncia equals EstadoDenuncia.CodEstadoDenuncia
                                                    join TipoDenuncia in bd.TipoDenuncia
                                                    on Denuncia.CodTipoDenuncia equals TipoDenuncia.CodTipoDenuncia
                                                    join Prioridad in bd.Prioridads
                                                    on Denuncia.NroPrioridad equals Prioridad.NroPrioridad
                                                    join Usuario in bd.Usuarios
                                                    on Denuncia.IdUsuario equals Usuario.IdUsuario
                                                    where Denuncia.Bhabilitado == 1
                                                    select new DenunciaCLS2
                                                    {
                                                        Nro_Denuncia = Denuncia.NroDenuncia,
                                                        Fecha = (DateTime)Denuncia.Fecha,
                                                        Estado_Denuncia = EstadoDenuncia.Nombre,
                                                        Tipo_Denuncia = TipoDenuncia.Nombre,
                                                        Prioridad = Prioridad.NombrePrioridad,
                                                        IdUsuario = (int)((Denuncia.IdUsuario.HasValue) ? Denuncia.IdUsuario : 0),
                                                        NombreUser = (string)Usuario.NombreUser

                                                    }).ToList();
                return listaDenuncia;
            }

        }
        //con Filtros hay que modificar la consulta con los filtros a la denuncia service
        [HttpGet]
        [Route("listarDenunciasconFiltros")]
        public IActionResult listarDenunciasconFiltros([FromHeader(Name = "id_usuario")] string idUsuarioAlta,
                                           [FromQuery] int tipo,
                                           [FromQuery] int estado)
        {
            try
            {
                return Ok(denunciaService.listarDenunciasconFiltros(Int32.Parse(idUsuarioAlta), tipo, estado));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("listarEstadosDenuncia")]
        public IEnumerable<DenunciaCLS> listarEstadosDenuncia()
        {
            List<DenunciaCLS> listaEstadoDenuncia;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                listaEstadoDenuncia = (from EstadoDenuncia in bd.EstadoDenuncia
                                       where EstadoDenuncia.Bhabilitado == 1
                                       select new DenunciaCLS
                                       {
                                           cod_Estado_Denuncia = EstadoDenuncia.CodEstadoDenuncia,
                                           nombre = EstadoDenuncia.Nombre,
                                           bHabilitado = (int)EstadoDenuncia.Bhabilitado,
                                           descripcion = EstadoDenuncia.Descripcion

                                       }).ToList();
                return listaEstadoDenuncia;
            }

        }
        [HttpGet]
        [Route("ListarDenunciasCerradas")]
        public IActionResult ListarDenunciasCerradas()
        {
            try
            {
                return Ok(denunciaService.ListarDenunciasCerradas());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("DenunciasCerradas")]
        public ChartDenuncia DenunciasCerradas([FromHeader(Name = "id_usuario")] string idUsuario)
        {
            ChartDenuncia oChartEmpleado = new ChartDenuncia();

            try
            {

                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    oChartEmpleado.totalDenunAsignaEmpleado = (from denuncia in bd.Denuncia
                                                               join usuario in bd.Usuarios
                                                               on denuncia.IdUsuario equals usuario.IdUsuario
                                                               where denuncia.Bhabilitado == 1
                                                               && denuncia.IdUsuario == Int32.Parse(idUsuario)
                                                               select new DenunciaCLS2
                                                               {
                                                                   Nro_Denuncia = (int)denuncia.NroDenuncia,
                                                                   IdUsuario = (int)(denuncia.IdUsuario),
                                                               }).Count();

                    oChartEmpleado.totalDenuncias = (from denuncia in bd.Denuncia
                                                     where denuncia.Bhabilitado == 0
                                                     select new DenunciaCLS2
                                                     {
                                                         Nro_Denuncia = (int)denuncia.NroDenuncia,
                                                     }).Count();

                    if (oChartEmpleado.totalDenunAsignaEmpleado > 0)
                    {
                        Usuario oUsuario = (from denuncia in bd.Denuncia
                                            join usuario in bd.Usuarios
                                            on denuncia.IdUsuario equals usuario.IdUsuario
                                            where denuncia.Bhabilitado == 1
                                            && denuncia.IdUsuario == Int32.Parse(idUsuario)
                                            select new Usuario
                                            {
                                                NombreUser = usuario.NombreUser
                                            }).First();



                        oChartEmpleado.nombreEmpleado = !String.IsNullOrEmpty(oUsuario.NombreUser) ? oUsuario.NombreUser : "No Posee";
                    }
                    else
                    {
                        oChartEmpleado.nombreEmpleado = "";
                    }
                    return oChartEmpleado;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return oChartEmpleado;
                // return (ex.Message);
            }


        }
        
        [HttpPost]
        [Route("guardarDenuncia")]
        [AllowAnonymous]
        public int guardarDenuncia([FromBody] DenunciaCLS2 oDenunciaCLS2)
        {
            int rpta = 0;
            string descripcion = !String.IsNullOrEmpty(oDenunciaCLS2.Descripcion) ? oDenunciaCLS2.Descripcion : "No Posee";
            ;

            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    //Usaremos transacciones porque tocaremos dos tablas simultaneamente.
                    using (var transaccion = new TransactionScope())
                    {
                        Denuncium oDenuncia = new Denuncium();
                        oDenuncia.CodTipoDenuncia = oDenunciaCLS2.CodTipoDenuncia;
                        oDenuncia.Calle = oDenunciaCLS2.Calle;
                        oDenuncia.Altura = oDenunciaCLS2.Altura;
                        oDenuncia.EntreCalles = oDenunciaCLS2.Entre_Calles;
                        oDenuncia.NombreInfractor = oDenunciaCLS2.Nombre_Infractor;
                        oDenuncia.ApellidoInfractor = oDenunciaCLS2.Apellido_Infractor;
                        oDenuncia.CodEstadoDenuncia = 3;
                        oDenuncia.Descripcion = oDenunciaCLS2.Descripcion;
                        oDenuncia.MailNotificacion = oDenunciaCLS2.Mail_Notificacion;
                        oDenuncia.MovilNotificacion = oDenunciaCLS2.Movil_Notificacion;
                        oDenuncia.NroPrioridad = 4;
                        // oUsuario = bd.Usuario.Where(u => u.NombreUser.Contains("Sin Asignar")).First();

                        oDenuncia.IdUsuario = 2;  //oUsuario.IdUsuario;
                                                  //oDenuncia.IdUsuario = AÑADIR FUNCIONALIDAD PARA CUANDO EL VECINO ESTA LOGUEADO
                        oDenuncia.Bhabilitado = 1;
                        bd.Denuncia.Add(oDenuncia);
                        bd.SaveChanges();
                        oDenuncia = bd.Denuncia.Where(d => d.Descripcion == descripcion).First();

                        int NroDenunciaTemp = oDenuncia.NroDenuncia;
                        if (oDenunciaCLS2.foto != "data:text/plain;base64,dGVzdCB0ZXh0")
                        {
                            PruebaGraficaDenuncium oPrueba = new PruebaGraficaDenuncium();
                            oPrueba.Foto = oDenunciaCLS2.foto;
                            oPrueba.NroDenuncia = NroDenunciaTemp;
                            oPrueba.Bhabilitado = 1;

                            bd.PruebaGraficaDenuncia.Add(oPrueba);
                        }
                        if (oDenunciaCLS2.foto2 != "data:text/plain;base64,dGVzdCB0ZXh0")
                        {
                            PruebaGraficaDenuncium oPrueba = new PruebaGraficaDenuncium();
                            oPrueba.Foto = oDenunciaCLS2.foto2;
                            oPrueba.NroDenuncia = NroDenunciaTemp;
                            oPrueba.Bhabilitado = 1;
                            bd.PruebaGraficaDenuncia.Add(oPrueba);
                        }
                        if (oDenunciaCLS2.foto3 != "data:text/plain;base64,dGVzdCB0ZXh0")
                        {
                            PruebaGraficaDenuncium oPrueba = new PruebaGraficaDenuncium();
                            oPrueba.Foto = oDenunciaCLS2.foto3;
                            oPrueba.NroDenuncia = NroDenunciaTemp;
                            oPrueba.Bhabilitado = 1;
                            bd.PruebaGraficaDenuncia.Add(oPrueba);

                        }
                        bd.SaveChanges();
                        transaccion.Complete();

                        // oDenuncia = bd.Denuncia.Where(d => d.NroDenuncia == DenunciaCLS.NroDenuncia).First();


                    } //Fin de La Transaccion
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

        [HttpPost]
        [Route("DerivaPriorizaDenuncia")]
        public int DerivaPriorizaDenuncia([FromBody] DenunciaCLS2 DenunciaCLS2)
        {
            int rpta = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    Denuncium oDenuncia = bd.Denuncia.Where(d => d.NroDenuncia == DenunciaCLS2.Nro_Denuncia).First();
                    // oDenuncia.CodTipoDenuncia = int.Parse(DenunciaCLS2.Tipo_Denuncia);
                    oDenuncia.CodEstadoDenuncia = 2;
                    oDenuncia.NroPrioridad = DenunciaCLS2.Nro_Prioridad;
                    oDenuncia.IdUsuario = DenunciaCLS2.IdUsuario;
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


        [HttpPost]
        [Route("solucionarDenuncia")]
        //VULNEARBiLIDAD EN PUERTA SI SE MODIFIQCA EL PARAKMETRO SE PUEDE CERRAR DENUNCUAS
        public int solucionarDenuncia([FromHeader(Name = "id_usuario")] string idUsuario, [FromBody] TrabajoCLS oTrabajoCLS)
        {
            int rpta = 0;
            int iddelUsuario = int.Parse(idUsuario);
            try
            {

                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    rpta = (from usuario in bd.Usuarios
                            join rol in bd.Rols
                            on usuario.IdTipoUsuario equals rol.IdRol

                            where usuario.Bhabilitado == 1
                            && usuario.IdUsuario == iddelUsuario
                              && rol.CodRol == "MDE"
                            select new DenunciaCLS2
                            {
                                Nro_Denuncia = oTrabajoCLS.Nro_Denuncia,
                            }).Count();
                    if (rpta == 1)
                    {
                        Denuncium oDenuncia = bd.Denuncia.Where(d => d.NroDenuncia == oTrabajoCLS.Nro_Denuncia).First();
                        oDenuncia.Bhabilitado = 0;
                        oDenuncia.CodEstadoDenuncia = 8;
                       bd.SaveChanges();
                        //Esta faltando registrar el trabajo final de cierre de la denuncia. 
                        //revisar el codigo a continuacion que no me cierra el cierre de denuncia sin cargar ningun trabajo che.
                        //try
                        //{
                        //    using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                        //    {
                        //        using (var transaccion = new TransactionScope())
                        //        {
                        //            TrabajoCLS oTrCLS = new TrabajoCLS();
                        //            Trabajo oTrabajo = new Trabajo();
                        //            oTrabajo.Descripcion = oTrabajoCLS.Descripcion;
                        //            oTrabajo.IdUsuario = oTrabajoCLS.Id_Usuario;
                        //            oTrabajo.NroDenuncia = oTrabajoCLS.Nro_Denuncia;
                        //            oTrabajo.Bhabilitado = 1;
                        //            bd.Trabajos.Add(oTrabajo);
                        //            bd.SaveChanges();
                        //            oTrCLS = (from Tr in bd.Trabajos
                        //                      select new TrabajoCLS
                        //                      {
                        //                          Nro_Trabajo = Tr.NroTrabajo

                        //                      }).OrderBy(Tr => Tr.Nro_Trabajo).Last();
                        //            nrotrabTemp = oTrCLS.Nro_Trabajo;
                                    
                        //            bd.SaveChanges();
                        //            transaccion.Complete();

                        //        } //Fin de La Transaccion
                        //    }
                        //    rpta = 1;
                        //}
                        //catch (Exception ex)
                        //{
                        //    Console.WriteLine(ex);
                        //    rpta = 0;
                        //}

                    }
                    else
                    {
                        {
                            Denuncium oDenuncia = bd.Denuncia.Where(d => d.NroDenuncia == oTrabajoCLS.Nro_Denuncia).First();
                            oDenuncia.CodEstadoDenuncia = 5;
                            bd.SaveChanges();
                            try
                            {
                                //using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                                {
                                    using (var transaccion = new TransactionScope())
                                    {
                                        TrabajoCLS oTrCLS = new TrabajoCLS();
                                        Trabajo oTrabajo = new Trabajo();
                                        oTrabajo.Descripcion = "Se deriva a mesa de Ayuda para la finalizacion de la denuncia y proceda a notificar si corresponde";
                                        oTrabajo.IdUsuario = oTrabajoCLS.Id_Usuario;
                                        oTrabajo.NroDenuncia = oTrabajoCLS.Nro_Denuncia;
                                        oTrabajo.Bhabilitado = 1;
                                        bd.Trabajos.Add(oTrabajo);
                                        bd.SaveChanges();
                                        transaccion.Complete();

                                    } //Fin de La Transaccion
                                }
                                rpta = 1;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                                rpta = 0;
                            }
                        }
                        rpta = 1;
                    }

                    return rpta;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                rpta = 0;
            }
            return rpta;
        }



        [HttpPost]
        [Route("devolverAMEsa")]  ///{nroDenuncia} [FromBody]{id}
        public int devolverAMEsa([FromBody] TrabajoCLS oTrabajoCLS)
        {
            int rpta = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {

                    Denuncium oDenuncia = bd.Denuncia.Where(d => d.NroDenuncia == oTrabajoCLS.Nro_Denuncia).First();
                    oDenuncia.CodEstadoDenuncia = 7;
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
        [Route("DenunciasxEmpleado")]
        public ChartDenuncia DenunciasxEmpleado([FromHeader(Name = "id_usuario")] string idUsuario)
        {
            ChartDenuncia oChartEmpleado = new ChartDenuncia();

            try
            {

                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    oChartEmpleado.totalDenunAsignaEmpleado = (from denuncia in bd.Denuncia
                                                               join usuario in bd.Usuarios
                                                               on denuncia.IdUsuario equals usuario.IdUsuario
                                                               where denuncia.Bhabilitado == 1
                                                               && denuncia.IdUsuario == Int32.Parse(idUsuario)
                                                               select new DenunciaCLS2
                                                               {
                                                                   Nro_Denuncia = (int)denuncia.NroDenuncia,
                                                                   IdUsuario = (int)(denuncia.IdUsuario),
                                                               }).Count();

                    oChartEmpleado.totalDenuncias = (from denuncia in bd.Denuncia
                                                     where denuncia.Bhabilitado == 1
                                                     select new DenunciaCLS2
                                                     {
                                                         Nro_Denuncia = (int)denuncia.NroDenuncia,
                                                     }).Count();

                    if (oChartEmpleado.totalDenunAsignaEmpleado > 0)
                    {
                        Usuario oUsuario = (from denuncia in bd.Denuncia
                                            join usuario in bd.Usuarios
                                            on denuncia.IdUsuario equals usuario.IdUsuario
                                            where denuncia.Bhabilitado == 1
                                            && denuncia.IdUsuario == Int32.Parse(idUsuario)
                                            select new Usuario
                                            {
                                                NombreUser = usuario.NombreUser
                                            }).First();



                        oChartEmpleado.nombreEmpleado = !String.IsNullOrEmpty(oUsuario.NombreUser) ? oUsuario.NombreUser : "No Posee";
                    }
                    else
                    {
                        oChartEmpleado.nombreEmpleado = "";
                    }
                    return oChartEmpleado;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return oChartEmpleado;
                // return (ex.Message);
            }


        }


        [HttpGet]
        [Route("tipos-denuncia/{codTipoDenuncia}")]
        public IActionResult consultarTipoDenuncia(int codTipoDenuncia)
        {
            try
            {
                TipoDenunciaCLS tipoDenuncia = this.denunciaService.getTipoDenuncia(codTipoDenuncia);
                return Ok(tipoDenuncia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("listarTiposDenuncia")]
        [AllowAnonymous]

        public IEnumerable<TipoDenunciaCLS> listarTiposDenuncia()
        {
            List<TipoDenunciaCLS> listaTipoDenuncia;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                listaTipoDenuncia = (from TipoDenuncia in bd.TipoDenuncia
                                     where TipoDenuncia.Bhabilitado == 1
                                     select new TipoDenunciaCLS
                                     {
                                         cod_Tipo_Denuncia = (int)TipoDenuncia.CodTipoDenuncia,
                                         Nombre= !String.IsNullOrEmpty(TipoDenuncia.Nombre) ? TipoDenuncia.Nombre : "Sin Tipo",
                                         Tiempo_Max_Tratamiento = (int)TipoDenuncia.TiempoMaxTratamiento,
                                         Descripcion = !String.IsNullOrEmpty(TipoDenuncia.Descripcion) ? TipoDenuncia.Descripcion : "Sin Descripcion"
                                     }).ToList();
                return listaTipoDenuncia;
            }

        }


        [HttpDelete]
        [Route("tipos-denuncia/{codTipoDenuncia}")]
        public IActionResult emininarTipoDenuncia(int codTipoDenuncia)
        {
            try
            {
                this.denunciaService.eliminarTipoDenuncia(codTipoDenuncia);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]

        [Route("tipos-denuncia")]
        public IActionResult guardarTipoDenuncia(
            [FromHeader(Name = "id_usuario")] string idUsuario,
            [FromBody] TipoDenunciaCLS tipoDenunciaCLS)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    StringBuilder errors = new StringBuilder("Errores: ");
                    ModelState.Keys.ToList().ForEach(key => errors.Append(key.ToString() + ". "));
                    Console.WriteLine(errors);
                    throw new PagoNoValidoException(errors.ToString());
                }

                tipoDenunciaCLS = this.denunciaService.guardarTipoDenuncia(tipoDenunciaCLS, Int32.Parse(idUsuario));
                return Ok(tipoDenunciaCLS);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("tipos-denuncia")]
        public IActionResult actualizarTipoReclamo(
            [FromHeader(Name = "id_usuario")] string idUsuario,
            [FromBody] TipoDenunciaCLS tipoDenunciaCLS)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    StringBuilder errors = new StringBuilder("Errores: ");
                    ModelState.Keys.ToList().ForEach(key => errors.Append(key.ToString() + ". "));
                    Console.WriteLine(errors);
                    throw new PagoNoValidoException(errors.ToString());
                }

                tipoDenunciaCLS = this.denunciaService.modificarTipoDenuncia(tipoDenunciaCLS, Int32.Parse(idUsuario));
                return Ok(tipoDenunciaCLS);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




    } //Cierre de la Clase


}
