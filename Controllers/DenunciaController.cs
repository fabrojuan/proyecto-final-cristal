﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace MVPSA_V2022.Controllers
{
    public class DenunciaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/Denuncia/listarTiposDenuncia")]
        public IEnumerable<TipoDenunciaCLS> listarTiposDenuncia()
        {
            List<TipoDenunciaCLS> listaTipoDenuncia;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {

                listaTipoDenuncia = (from TipoDenuncia in bd.TipoDenuncia
                                     where TipoDenuncia.Bhabilitado == 1
                                     select new TipoDenunciaCLS
                                     {
                                         Cod_Tipo_Denuncia = TipoDenuncia.CodTipoDenuncia,
                                         Nombre = TipoDenuncia.Nombre,
                                         Tiempo_Max_Tratamiento = (int)TipoDenuncia.TiempoMaxTratamiento,
                                         Descripcion = TipoDenuncia.Descripcion

                                     }).ToList();
                return listaTipoDenuncia;
            }

        }

        [HttpGet]
        [Route("api/Denuncia/listarEstadosDenuncia")]
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
        [Route("api/Denuncia/listarDenuncias")]
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
        [HttpPost]
        [Route("api/Denuncia/guardarDenuncia")] 
        public int guardarDenuncia([FromBody] DenunciaCLS2 oDenunciaCLS2)
        {
            int rpta = 0;
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
                        int NroDenunciaTemp = oDenuncia.NroDenuncia;
                        if (oDenunciaCLS2.foto != null)
                        {
                            PruebaGraficaDenuncium oPrueba = new PruebaGraficaDenuncium();
                            oPrueba.Foto = oDenunciaCLS2.foto;
                            oPrueba.NroDenuncia = NroDenunciaTemp;
                            oPrueba.Bhabilitado = 1;

                            bd.PruebaGraficaDenuncia.Add(oPrueba);
                        }
                        if (oDenunciaCLS2.foto2 != null)
                        {
                            PruebaGraficaDenuncium oPrueba = new PruebaGraficaDenuncium();
                            oPrueba.Foto = oDenunciaCLS2.foto2;
                            oPrueba.NroDenuncia = NroDenunciaTemp;
                            oPrueba.Bhabilitado = 1;
                            bd.PruebaGraficaDenuncia.Add(oPrueba);
                        }
                        if (oDenunciaCLS2.foto3 != null)
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
        [Route("api/Denuncia/DerivaPriorizaDenuncia")]
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
        [Route("api/Denuncia/solucionarDenuncia")]
        public int solucionarDenuncia([FromBody] TrabajoCLS oTrabajoCLS)
        {
            int rpta = 0;
            try
            {
                int idTipoRol = int.Parse(HttpContext.Session.GetString("tipoEmpleado"));
                if (idTipoRol == 2)
                {
                    using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                    {
                        Denuncium oDenuncia = bd.Denuncia.Where(d => d.NroDenuncia == oTrabajoCLS.Nro_Denuncia).First();
                        oDenuncia.CodEstadoDenuncia = 1;
                        oDenuncia.Bhabilitado = 0;
                        bd.SaveChanges();

                    }
                    rpta = 1;
                }
                else
                {
                    using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                    {
                        Denuncium oDenuncia = bd.Denuncia.Where(d => d.NroDenuncia == oTrabajoCLS.Nro_Denuncia).First();
                        oDenuncia.CodEstadoDenuncia = 8;
                        bd.SaveChanges();

                    }
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



        [HttpPost]
        [Route("api/Denuncia/devolverAMEsa")]  ///{nroDenuncia} [FromBody]{id}
        public int devolverAMEsa([FromBody] TrabajoCLS oTrabajoCLS)
        {
            int rpta = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {

                    Denuncium oDenuncia = bd.Denuncia.Where(d => d.NroDenuncia == oTrabajoCLS.Nro_Denuncia).First();
                    oDenuncia.CodEstadoDenuncia = 8;
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
        [Route("api/Denuncia/DenunciasxEmpleado")]
        public ChartDenuncia DenunciasxEmpleado()
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                ChartDenuncia oChartEmpleado = new ChartDenuncia();
                oChartEmpleado.IdUsuario = int.Parse(HttpContext.Session.GetString("empleado"));

                oChartEmpleado.totalDenunAsignaEmpleado = (from denuncia in bd.Denuncia
                                                           join usuario in bd.Usuarios
                                                           on denuncia.IdUsuario equals usuario.IdUsuario
                                                           where denuncia.Bhabilitado == 1
                                                           && denuncia.IdUsuario == oChartEmpleado.IdUsuario
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
                                        && denuncia.IdUsuario == oChartEmpleado.IdUsuario
                                        select new Usuario
                                        {
                                            NombreUser = usuario.NombreUser
                                        }).First();
                    oChartEmpleado.nombreEmpleado = oUsuario.NombreUser;
                }
                else
                {
                    oChartEmpleado.nombreEmpleado = "";
                }
                return oChartEmpleado;
            }
        }







    } //Cierre de la Clase


}
