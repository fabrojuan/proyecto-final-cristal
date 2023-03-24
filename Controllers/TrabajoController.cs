using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace MVPSA_V2022.Controllers
{
    [Authorize]
    public class TrabajoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("api/Trabajo/listarUsuarios")]
        public IEnumerable<UsuarioCLS> ListarUsuarios()
        {
            List<UsuarioCLS> listaUsuario;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                listaUsuario = (from usuario in bd.Usuarios
                                    //  join persona in bd.Persona
                                    // on usuario.IdPersona equals persona.IdPersona
                                join rol in bd.Rols
                                on usuario.IdTipoUsuario equals rol.IdRol
                                where usuario.Bhabilitado == 1 && (!rol.NombreRol.Contains("Vecino"))

                                select new UsuarioCLS
                                {
                                    IdUsuario = usuario.IdUsuario,
                                    NombreUser = usuario.NombreUser,
                                    NombreTipoUsuario = rol.NombreRol
                                }).ToList();
                return listaUsuario;
            }
            //&& !rol.NombreRol.Contains("Administrador")) Este comentario va luego del primer condicion del where
        }
        [HttpGet]
        [Route("api/Trabajo/listarPrioridades")]
        public IEnumerable<PrioridadCLS> ListarPrioridades()
        {
            List<PrioridadCLS> listaPrioridad;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                listaPrioridad = (from prioridad in bd.Prioridads
                                  where prioridad.Bhabilitado == 1
                                  select new PrioridadCLS
                                  {
                                      NroPrioridad = prioridad.NroPrioridad,
                                      NombrePrioridad = prioridad.NombrePrioridad,
                                      Descripcion = prioridad.Descripcion
                                  }).ToList();
                return listaPrioridad;
            }
        }


        [HttpPost]
        [Route("api/Trabajo/guardarTrabajo")]
        public int GuardarTrabajo([FromBody] TrabajoCLS oTrabajoCLS)
        {
            int rpta = 0;
            // int idPersonatem = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    using (var transaccion = new TransactionScope())
                    {
                        TrabajoCLS oTrCLS = new TrabajoCLS();
                        int NropruebaTemp1 = 0;
                        int NropruebaTemp2 = 0;
                        int NropruebaTemp3 = 0;
                        int nrotrabTemp = 0;
                        Trabajo oTrabajo = new Trabajo();
                        oTrabajo.Descripcion = oTrabajoCLS.Descripcion;
                        oTrabajo.IdUsuario = oTrabajoCLS.Id_Usuario;
                        oTrabajo.NroDenuncia = oTrabajoCLS.Nro_Denuncia;
                        oTrabajo.Bhabilitado = 1;
                        bd.Trabajos.Add(oTrabajo);
                        bd.SaveChanges();
                        oTrCLS = (from Tr in bd.Trabajos
                                    select new TrabajoCLS
                                    {
                                       Nro_Trabajo=Tr.NroTrabajo

                                    }).OrderBy(Tr => Tr.Nro_Trabajo).Last();
                        nrotrabTemp = oTrCLS.Nro_Trabajo;
                        if (oTrabajoCLS.Foto != "")
                        {
                            PruebaGraficaDenuncium oPrueba = new PruebaGraficaDenuncium();
                            oPrueba.Foto = oTrabajoCLS.Foto;
                            oPrueba.Bhabilitado = 1;
                            oPrueba.NroTrabajo = nrotrabTemp;
                            bd.PruebaGraficaDenuncia.Add(oPrueba);
                            NropruebaTemp1 = oPrueba.NroImagen;
                        }
                        if (oTrabajoCLS.Foto2 != "")
                        {
                            PruebaGraficaDenuncium oPrueba2 = new PruebaGraficaDenuncium();
                            oPrueba2.Foto = oTrabajoCLS.Foto2;
                            oPrueba2.Bhabilitado = 1;
                            oPrueba2.NroTrabajo = nrotrabTemp;
                            bd.PruebaGraficaDenuncia.Add(oPrueba2);
                            NropruebaTemp2 = oPrueba2.NroImagen;
                        }
                        if (oTrabajoCLS.Foto3 != "")
                        {
                            PruebaGraficaDenuncium oPrueba3 = new PruebaGraficaDenuncium();
                            oPrueba3.Foto = oTrabajoCLS.Foto3;
                            oPrueba3.Bhabilitado = 1;
                            oPrueba3.NroTrabajo = nrotrabTemp;
                            bd.PruebaGraficaDenuncia.Add(oPrueba3);
                            NropruebaTemp3 = oPrueba3.NroImagen;
                        }
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
            return rpta;
        }
        //----------------------------------------------------------------
        [HttpPost]
        [Route("api/Trabajo/notificar")]
        public int notificar([FromBody] TrabajoCLS oTrabajoCLS)
        {
            int rpta = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {

                    //SqlParameter idDenuncia = new SqlParameter("@IdDenuncia", oTrabajoCLS.Nro_Denuncia);
                    //bd.Database.
                    //Command("PRUEBADEMAIL");
                    //Arriba esta lo que migré no se si esto andaba o no luego lo tocaremos por cada laburo.
                    // bd.SaveChanges();
                }
                rpta = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                rpta = 0;
            }
            // oTrabajo.NroDenuncia = oTrabajoCLS.Nro_Denuncia;
            //     oTrabajo.Bhabilitado = 1;

            return rpta;
        }





        //--------------------------------------------------------------



        [HttpPost]
        [Route("api/Trabajo/GuardarTrabajoReclamo")]
        public int GuardarTrabajoReclamo([FromBody] TrabajoCLS oTrabajoCLS)
        {
            int rpta = 0;
            try
            {
                using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
                {
                    TrabajoReclamo oTrabajo = new TrabajoReclamo();
                    oTrabajo.Descripcion = oTrabajoCLS.Descripcion;
                    oTrabajo.IdUsuario = oTrabajoCLS.Id_Usuario;
                    oTrabajo.NroReclamo = oTrabajoCLS.Nro_Reclamo;
                    oTrabajo.IdVecino = oTrabajoCLS.Id_Vecino;
                    oTrabajo.Bhabilitado = 1;
                    bd.TrabajoReclamos.Add(oTrabajo);
                    bd.SaveChanges();
                }
                rpta = 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                rpta = 0;
            }
            // oTrabajo.NroDenuncia = oTrabajoCLS.Nro_Denuncia;
            //     oTrabajo.Bhabilitado = 1;

            return rpta;
        }


        [HttpGet]
        [Route("api/Trabajo/RecuperarDenuncia/{idDenuncia}")]
        public DenunciaCLS2 RecuperarDenuncia(int idDenuncia)
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                DenunciaCLS2 oDenunciaCLS = (from denuncia in bd.Denuncia
                                             join estadoDenuncia in bd.EstadoDenuncia
                                             on denuncia.CodEstadoDenuncia equals estadoDenuncia.CodEstadoDenuncia
                                             where denuncia.Bhabilitado == 1
                                             && denuncia.NroDenuncia == idDenuncia
                                             select new DenunciaCLS2
                                             {
                                                 Nro_Denuncia = (int)denuncia.NroDenuncia,
                                                 IdUsuario = (int)((denuncia.IdUsuario > 0) ? denuncia.IdUsuario : int.Parse(HttpContext.Session.GetString("empleado"))),  //denuncia.IdUsuario : 1014

                                                 Estado_Denuncia = estadoDenuncia.Nombre
                                             }).First();
                return oDenunciaCLS;
            }
        }

        [HttpGet]
        [Route("api/Trabajo/detalleDenuncia/{idDenuncia}")]
        public DenunciaCLS2 detalleDenuncia(int idDenuncia)
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                //string tipoDenunciaTemp = "";

                DenunciaCLS2 oDenunciaCLS = (from denuncia in bd.Denuncia
                                             join estadoDenuncia in bd.EstadoDenuncia
                                             on denuncia.CodEstadoDenuncia equals estadoDenuncia.CodEstadoDenuncia
                                             join tipoDenuncia in bd.TipoDenuncia
                                             on denuncia.CodTipoDenuncia equals tipoDenuncia.CodTipoDenuncia
                                             where denuncia.Bhabilitado == 1
                                             && denuncia.NroDenuncia == idDenuncia
                                             select new DenunciaCLS2
                                             {
                                                 Nro_Denuncia = (int)denuncia.NroDenuncia,
                                                 IdUsuario = (int)((denuncia.IdUsuario > 0) ? denuncia.IdUsuario : int.Parse(HttpContext.Session.GetString("empleado"))),  //denuncia.IdUsuario : 1014
                                                 Descripcion = denuncia.Descripcion,
                                                 Altura = denuncia.Altura,
                                                 Calle = denuncia.Calle,
                                                 Entre_Calles = denuncia.EntreCalles,
                                                 Estado_Denuncia = estadoDenuncia.Nombre,
                                                 Nro_Prioridad = (int)denuncia.NroPrioridad,
                                                 Tipo_Denuncia = tipoDenuncia.Nombre,
                                                 //Debo trar la descripcion del tipo de denuncia denuncia.CodTipoDenuncia
                                                 // tipoDenunciaTemp = bd.PruebaGraficaDenuncia.Where(pg => pg.idDenuncia == idDenuncia && pg.IdUsuario == null).First(),
                                                 Nombre_Infractor = denuncia.NombreInfractor + " " + denuncia.ApellidoInfractor,
                                                 //Las imagenes se extraen del controller de pruebas
                                                 //Estado_Denuncia = estadoDenuncia.Nombre
                                             }).First();
                return oDenunciaCLS;
            }
        }

        [HttpGet]
        [Route("api/Trabajo/detalleTrabajoDenuncia/{nro_Trabajo}")]
        public TrabajoCLS detalleTrabajoDenuncia(int nro_Trabajo)
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                TrabajoCLS oTrabajoCLS = (from trabajo in bd.Trabajos
                                          join denuncia in bd.Denuncia
                on trabajo.NroDenuncia equals denuncia.NroDenuncia
                                          where denuncia.Bhabilitado == 1
                                          && trabajo.NroTrabajo == nro_Trabajo
                                          select new TrabajoCLS
                                          {
                                              Fecha = (DateTime)trabajo.Fecha,
                                              Nro_Denuncia = trabajo.NroDenuncia,
                                              Descripcion = trabajo.Descripcion,
                                              Id_Usuario = (int)trabajo.IdUsuario,
                                              Nro_Trabajo = (int)trabajo.NroTrabajo,
                                              // por el anterior concepto Foto = ((trabajo.ImagenTrab1.HasValue) ? trabajo.ImagenTrab1.ToString() : "1013"),
                                          }).First();
                return oTrabajoCLS;
            }
        }


        [HttpGet]
        [Route("api/Trabajo/ImagenTrabajoDenuncia/{nro_Trabajo}")]
        public IEnumerable<PruebaImagenCLS> ImagenTrabajoDenuncia(int nro_Trabajo)
        {
            List<PruebaImagenCLS> oPrueba;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                oPrueba = (from prueba in bd.PruebaGraficaDenuncia
                           where prueba.NroTrabajo == nro_Trabajo
                           select new PruebaImagenCLS
                           {
                               Foto = prueba.Foto,
                               NroImagen = prueba.NroImagen
                           }).ToList();
                return oPrueba;
            }
        }


        //******* IMPORTANTE !!! ACA EN RECLAMO TENGO QUE TRAZAR EL VECINO QUE GENERÖ EL RECLAMO  ********* ==>>
        // idVecino en tabla reclamo

        [HttpGet]
        [Route("api/Trabajo/RecuperarReclamo/{idReclamo}")]
        public ReclamoCLS RecuperarReclamo(int idReclamo)
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                ReclamoCLS oReclamoCLS = (from reclamo in bd.Reclamos
                                          join estadoReclamo in bd.EstadoReclamos
                                          on reclamo.CodEstadoReclamo equals estadoReclamo.CodEstadoReclamo
                                          join usuarioVecino in bd.UsuarioVecinos on reclamo.IdVecino equals
                                          usuarioVecino.IdVecino
                                          join persona in bd.Personas on usuarioVecino.IdPersona equals persona.IdPersona
                                          where reclamo.Bhabilitado == 1
                                          && reclamo.NroReclamo == idReclamo && reclamo.IdVecino == usuarioVecino.IdVecino
                                          && usuarioVecino.IdPersona == persona.IdPersona
                                          select new ReclamoCLS
                                          {
                                              nroReclamo = (int)reclamo.NroReclamo,
                                              idUsuario = (int)((reclamo.IdUsuario > 0) ? reclamo.IdUsuario : 7),
                                              estadoReclamo = estadoReclamo.Nombre,
                                              idVecino = (int)reclamo.IdVecino,
                                              nombreYapellido = persona.Nombre + " " + persona.Apellido
                                          }).First();
                return oReclamoCLS;
            }

            // MemberAccessException FALTA AGRWGAR ID VECINO que es el veciono que lo genera al reclamo

        }

        [HttpGet]
        [Route("api/Trabajo/ListarTrabajos/{idDenuncia}")]
        public IEnumerable<TrabajoCLS> ListarTrabajos(int idDenuncia)
        {
            List<TrabajoCLS> listaTrabajo;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                listaTrabajo = (from trabajo in bd.Trabajos
                                join denuncia in bd.Denuncia
                                on trabajo.NroDenuncia equals denuncia.NroDenuncia
                                where denuncia.Bhabilitado == 1
                                             && denuncia.NroDenuncia == idDenuncia

                                select new TrabajoCLS
                                {
                                    Fecha = (DateTime)trabajo.Fecha,
                                    Nro_Denuncia = trabajo.NroDenuncia,
                                    Descripcion = trabajo.Descripcion,
                                    Id_Usuario = (int)trabajo.IdUsuario,
                                    Nro_Trabajo = trabajo.NroTrabajo
                                }).ToList();
                return listaTrabajo;
            }
        }
        //where trabajo.Bhabilitado ==1

        [HttpGet]
        [Route("api/Trabajo/ListarTrabajosReclamo/{idReclamo}")]
        public IEnumerable<TrabajoCLS> ListarTrabajosReclamo(int idReclamo)
        {
            List<TrabajoCLS> listaTrabajo;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                listaTrabajo = (from trabajoReclamo in bd.TrabajoReclamos
                                join reclamo in bd.Reclamos
                                on trabajoReclamo.NroReclamo equals reclamo.NroReclamo
                                where reclamo.Bhabilitado == 1
                                             && reclamo.NroReclamo == idReclamo

                                select new TrabajoCLS
                                {
                                    Fecha = (DateTime)trabajoReclamo.Fecha,
                                    Nro_Reclamo = trabajoReclamo.NroReclamo,
                                    Descripcion = trabajoReclamo.Descripcion,
                                    Id_Usuario = (int)trabajoReclamo.IdUsuario
                                }).ToList();
                return listaTrabajo;
            }
        }


















    }
}
