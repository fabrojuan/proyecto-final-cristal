using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVPSA_V2022.clases;
using MVPSA_V2022.clases.Mobbex;
using MVPSA_V2022.Enums;
using MVPSA_V2022.Exceptions;
using MVPSA_V2022.Modelos;
using MVPSA_V2022.Utils;

namespace MVPSA_V2022.Services
{
    public class ReclamoService : IReclamoService
    {
        public readonly IMapper mapper;
        public readonly IUsuarioService usuarioService;
        private readonly M_VPSA_V3Context dbContext;

        public ReclamoService(IMapper mapper, IUsuarioService usuarioService,
            M_VPSA_V3Context dbContext) {
            this.mapper = mapper;
            this.usuarioService = usuarioService;
            this.dbContext = dbContext;
        }

        /**
         * RECLAMOS
         */

        public ReclamoDto guardarReclamo(CrearReclamoRequestDto reclamoCLS, int idUsuarioAlta)
        {
            Reclamo reclamo = new Reclamo();

            try
            {
                if (reclamoCLS.idSugerenciaOrigen != 0) {
                    var rec = this.dbContext.Sugerencia.Where(s => s.IdSugerencia == reclamoCLS.idSugerenciaOrigen)
                    .FirstOrDefault();

                    if (rec == null) {
                        throw new BusinessException("La sugerencia ingresada no existe");
                    }

                    if (rec.EstadoNavigation.CodEstadoSugerencia != (int)EstadoSugerenciaEnum.CONSIDERADA) {
                        throw new BusinessException("Para poder crear un requerimiento a partir de una sugerencia esta debe estar en estado Considerada");
                    }
                }


                reclamo = Conversor.convertToReclamo(reclamoCLS);                
                reclamo.Bhabilitado = 1;
                reclamo.IdUsuario = idUsuarioAlta;
                reclamo.NroPrioridad = getPrioridadReclamoSegunTipoReclamo(reclamoCLS.codTipoReclamo);
                reclamo.Fecha = DateTime.Now;

                if (reclamoCLS.nroArea == null || reclamoCLS.nroArea == 0) {
                    var areaMesaDeEntrada = dbContext.Areas.Where(area => area.CodArea == "ME").Single();
                    reclamo.NroArea = areaMesaDeEntrada.NroArea;
                } else {
                    reclamo.NroArea = reclamoCLS.nroArea;
                }

                var area = dbContext.Areas.Where(a => a.NroArea == reclamo.NroArea).FirstOrDefault();
                if (area.CodArea == "ME") {
                    reclamo.CodEstadoReclamo = (int?) EstadoReclamoEnum.CREADO;
                } else {
                    reclamo.CodEstadoReclamo = (int?) EstadoReclamoEnum.EN_CURSO;
                }                

                if (reclamoCLS.idSugerenciaOrigen != 0) {
                    reclamo.IdSugerenciaOrigen = reclamoCLS.idSugerenciaOrigen;
                }
                
                Usuario usuarioAlta = dbContext.Usuarios
                    .Where(usr => usr.IdUsuario == idUsuarioAlta).FirstOrDefault();

                if ("VEC".Equals(usuarioAlta.IdTipoUsuarioNavigation.CodRol))
                {
                    reclamo.IdVecino = idUsuarioAlta;
                    Persona personaVecino = usuarioAlta.IdPersonaNavigation;
                    reclamo.NomApeVecino = personaVecino.Nombre + " " + personaVecino.Apellido;
                    reclamo.MailVecino = personaVecino.Mail;
                    reclamo.TelefonoVecino = personaVecino.Telefono;
                }

                    
                // Guarda la foto 1 si el vecino la cargo
                if (reclamoCLS.foto1 != null && reclamoCLS.foto1.Length > 0) {
                    PruebaGraficaReclamo pruebaGraficaReclamo1 = new PruebaGraficaReclamo();
                    pruebaGraficaReclamo1.IdUsuario = idUsuarioAlta;
                    pruebaGraficaReclamo1.Bhabilitado = 1;
                    reclamo.PruebaGraficaReclamos.Add(pruebaGraficaReclamo1);
                }

                // Guarda la foto 2 si el vecino la cargo
                if (reclamoCLS.foto2 != null && reclamoCLS.foto2.Length > 0)
                {
                    PruebaGraficaReclamo pruebaGraficaReclamo2 = new PruebaGraficaReclamo();
                    pruebaGraficaReclamo2.IdUsuario = idUsuarioAlta;
                    pruebaGraficaReclamo2.Bhabilitado = 1;
                    reclamo.PruebaGraficaReclamos.Add(pruebaGraficaReclamo2);
                }

                // Guarda el reclamo con las imagenes
                dbContext.Reclamos.Add(reclamo);

                if (reclamoCLS.idSugerenciaOrigen != 0) {
                    var sugerencia = this.dbContext.Sugerencia
                        .Where(s => s.IdSugerencia == reclamoCLS.idSugerenciaOrigen)
                        .FirstOrDefault();
                    sugerencia.Estado = (int) EstadoSugerenciaEnum.GESTIONADA;
                }

                dbContext.SaveChanges();
                
            }
            catch(BusinessException be) {
                throw be;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("Se produjo un error y no se pudo guardar el Reclamo");
            }

            return  Conversor.convertToReclamoDto(reclamo);
        }

        public IEnumerable<ReclamoDto> listarReclamos(int idUsuarioConectado, int area, int estado, int numero,
                                                      String nomApeVecino)
        {           

            var usuarioConectado = dbContext.Usuarios.Where(usu => usu.IdUsuario == idUsuarioConectado).FirstOrDefault();
            String codRolUsuarioConectado = usuarioConectado.IdTipoUsuarioNavigation.CodRol;

            var query = dbContext.Reclamos.Where(rec => rec.Bhabilitado == 1);
            /*if (codRolUsuarioConectado != "MDE" && codRolUsuarioConectado != "INT"
                && codRolUsuarioConectado != "ADS") {
                query = query.Where(rec => rec.NroArea == usuarioConectado.NroArea);
            }*/

            if (area != 0) {
                query = query.Where(rec => rec.NroArea == area);
            }

            if (estado != 0) {
                query = query.Where(rec => rec.CodEstadoReclamo == estado);
            }

            if (numero != 0) {
                query = query.Where(rec => rec.NroReclamo == numero);
            }

            if (nomApeVecino != null && nomApeVecino.Length > 0) {
                query = query.Where(rec => rec.NomApeVecino.ToUpper().Contains(nomApeVecino.ToUpper()));
            }

            List<ReclamoDto> listaReclamo = new List<ReclamoDto>();
            query.OrderByDescending(rec => rec.Fecha).ToList().ForEach(reclamo => {
                ReclamoDto reclamoDto = new ReclamoDto
                {
                    nroReclamo = reclamo.NroReclamo,
                    descripcion = reclamo.Descripcion,
                    codTipoReclamo = (int)reclamo.CodTipoReclamo,
                    codEstadoReclamo = (int)reclamo.CodEstadoReclamo,
                    Bhabilitado = reclamo.Bhabilitado,
                    calle = reclamo.Calle,
                    altura = reclamo.Altura,
                    entreCalles = reclamo.EntreCalles,
                    idVecino = reclamo.IdVecino,
                    idUsuario = reclamo.IdUsuario,
                    Fecha = (DateTime)reclamo.Fecha,
                    estadoReclamo = reclamo.CodEstadoReclamoNavigation.Nombre,
                    tipoReclamo = reclamo.CodTipoReclamoNavigation.Nombre,
                    nombreYapellido = reclamo.NomApeVecino,
                    nroPrioridad = (int)reclamo.NroPrioridad,
                    usuarioAsignado = "",
                    empleadoAsignado = reclamo.NroAreaNavigation?.Nombre,
                    prioridad = reclamo.NroPrioridadNavigation.NombrePrioridad,
                    interno = reclamo.Interno
                };
                listaReclamo.Add(reclamoDto);
            });

            return listaReclamo;
                        
        }

        public ReclamoDto getReclamo(int nroReclamo)
        {

            var reclamo = this.dbContext.Reclamos.Where(reclamo => reclamo.NroReclamo == nroReclamo
                && reclamo.Bhabilitado == 1)
                .FirstOrDefault();

            ReclamoDto reclamoResponse =  new ReclamoDto
                                {
                                nroReclamo = reclamo.NroReclamo,
                                descripcion = reclamo.Descripcion,
                                codTipoReclamo = (int)reclamo.CodTipoReclamo,
                                codEstadoReclamo = (int)reclamo.CodEstadoReclamo,
                                Bhabilitado = reclamo.Bhabilitado,
                                calle = reclamo.Calle,
                                altura = reclamo.Altura,
                                entreCalles = reclamo.EntreCalles,
                                idVecino = reclamo.IdVecino,
                                idUsuario = reclamo.IdUsuario,
                                Fecha = (DateTime)reclamo.Fecha,
                                estadoReclamo = reclamo.CodEstadoReclamoNavigation.Nombre,
                                tipoReclamo = reclamo.CodTipoReclamoNavigation.Nombre,
                                nombreYapellido = reclamo.NomApeVecino,
                                nroPrioridad = (int)reclamo.NroPrioridad,
                                //usuarioAsignado = reclamo.Usuario,
                                //empleadoAsignado = reclamo.Empleado,
                                prioridad = reclamo.NroPrioridadNavigation.NombrePrioridad,
                                nroArea = reclamo.NroArea,
                                interno = reclamo.Interno,
                                fechaCierre = reclamo.FechaCierre,
                                idSugerenciaOrigen = reclamo.IdSugerenciaOrigen
                            };

            List<PruebaGraficaReclamo> listaPruebaGraficaReclamo = 
                    this.dbContext.PruebaGraficaReclamos.Where(pgr => pgr.NroReclamo == nroReclamo)
                    .OrderBy(pgr => pgr.NroImagen)
                    .ToList();

                if (listaPruebaGraficaReclamo != null && listaPruebaGraficaReclamo.Count != 0) {
                    reclamoResponse.foto1 = listaPruebaGraficaReclamo.ElementAt(0).Foto;

                    if (listaPruebaGraficaReclamo.Count > 1) {
                        reclamoResponse.foto2 = listaPruebaGraficaReclamo.ElementAt(1).Foto;
                    }
                }

                return reclamoResponse;
        }

        /**
         * 
         * Tipos de Reclamo
         * 
         */
        public IEnumerable<TipoReclamoDto> listarTiposReclamo()
        {
            List<TipoReclamoDto> listaTiposReclamo;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                listaTiposReclamo = (from tipoReclamo in bd.TipoReclamos
                                     join usuarioAlta in bd.Usuarios
                                       on tipoReclamo.IdUsuarioAlta equals usuarioAlta.IdUsuario
                                     join usuarioModificacion in bd.Usuarios
                                       on tipoReclamo.IdUsuarioModificacion equals usuarioModificacion.IdUsuario
                                     where tipoReclamo.Bhabilitado == 1
                                    select new TipoReclamoDto
                                    {
                                        cod_Tipo_Reclamo = tipoReclamo.CodTipoReclamo,
                                        nombre = tipoReclamo.Nombre,
                                        descripcion = tipoReclamo.Descripcion,
                                        tiempo_Max_Tratamiento = (int)tipoReclamo.TiempoMaxTratamiento,
                                        usuarioAlta = usuarioAlta.NombreUser,
                                        usuarioModificacion = usuarioModificacion.NombreUser
                                    })
                                    .OrderBy(tr => tr.cod_Tipo_Reclamo)
                                    .ToList();
                return listaTiposReclamo;
            }
        }

        public TipoReclamoDto guardarTipoReclamo(TipoReclamoDto tipoReclamoDto, int idUsuarioAlta) {

            validarTipoReclamo(tipoReclamoDto);

            TipoReclamo tipoReclamo = new TipoReclamo();
            tipoReclamo.Nombre = tipoReclamoDto.nombre;
            tipoReclamo.Descripcion = tipoReclamoDto.descripcion;
            tipoReclamo.TiempoMaxTratamiento = tipoReclamoDto.tiempo_Max_Tratamiento;
            tipoReclamo.IdUsuarioAlta = idUsuarioAlta;
            tipoReclamo.IdUsuarioModificacion = idUsuarioAlta;
            tipoReclamo.Bhabilitado = 1;

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context()) {
                bd.TipoReclamos.Add(tipoReclamo);
                bd.SaveChanges();

                tipoReclamoDto = (from tipoReclamoQuery in bd.TipoReclamos
                                     join usuarioAlta in bd.Usuarios
                                       on tipoReclamoQuery.IdUsuarioAlta equals usuarioAlta.IdUsuario
                                     join usuarioModificacion in bd.Usuarios
                                       on tipoReclamoQuery.IdUsuarioModificacion equals usuarioModificacion.IdUsuario
                                     where tipoReclamoQuery.CodTipoReclamo == tipoReclamo.CodTipoReclamo
                                  select new TipoReclamoDto
                                     {
                                         cod_Tipo_Reclamo = tipoReclamoQuery.CodTipoReclamo,
                                         nombre = tipoReclamoQuery.Nombre,
                                         descripcion = tipoReclamoQuery.Descripcion,
                                         tiempo_Max_Tratamiento = (int)tipoReclamoQuery.TiempoMaxTratamiento,
                                         usuarioAlta = usuarioAlta.NombreUser,
                                         usuarioModificacion = usuarioModificacion.NombreUser
                                     })
                                    .Single();
            }

            return tipoReclamoDto;

        }

        
        public TipoReclamoDto modificarTipoReclamo(TipoReclamoDto tipoReclamoDto, int idUsuarioModificacion)
        {
            validarTipoReclamo(tipoReclamoDto);

            TipoReclamo tipoReclamo = new TipoReclamo();
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                tipoReclamo =
                    bd.TipoReclamos.Where(tr => tr.CodTipoReclamo == tipoReclamoDto.cod_Tipo_Reclamo).Single();
                tipoReclamo.Nombre = tipoReclamoDto.nombre;
                tipoReclamo.Descripcion = tipoReclamoDto.descripcion;
                tipoReclamo.TiempoMaxTratamiento = tipoReclamoDto.tiempo_Max_Tratamiento;
                tipoReclamo.FechaModificacion = DateTime.Now;
                tipoReclamo.IdUsuarioModificacion = idUsuarioModificacion;
                bd.SaveChanges();

                tipoReclamoDto = (from tipoReclamoQuery in bd.TipoReclamos
                                  join usuarioAlta in bd.Usuarios
                                    on tipoReclamoQuery.IdUsuarioAlta equals usuarioAlta.IdUsuario
                                  join usuarioModificacion in bd.Usuarios
                                    on tipoReclamoQuery.IdUsuarioModificacion equals usuarioModificacion.IdUsuario
                                  where tipoReclamoQuery.CodTipoReclamo == tipoReclamo.CodTipoReclamo
                                  select new TipoReclamoDto
                                  {
                                      cod_Tipo_Reclamo = tipoReclamoQuery.CodTipoReclamo,
                                      nombre = tipoReclamoQuery.Nombre,
                                      descripcion = tipoReclamoQuery.Descripcion,
                                      tiempo_Max_Tratamiento = (int)tipoReclamoQuery.TiempoMaxTratamiento,
                                      fechaAlta = (DateTime)tipoReclamoQuery.FechaAlta,
                                      fechaModificacion = (DateTime)tipoReclamoQuery.FechaModificacion,
                                      usuarioAlta = usuarioAlta.NombreUser,
                                      usuarioModificacion = usuarioModificacion.NombreUser
                                  })
                                    .Single();
            }

            return tipoReclamoDto;
        }

        private void validarTipoReclamo(TipoReclamoDto tipoReclamoDto)
        {
            if (tipoReclamoDto.nombre == null || tipoReclamoDto.nombre.Length == 0
                || tipoReclamoDto.nombre.Length > 90)
            {
                throw new TipoReclamoNoValidoException("El nombre es requerido y no puede superar los 90 caracteres");
            }

            if (tipoReclamoDto.descripcion == null || tipoReclamoDto.descripcion.Length == 0
                || tipoReclamoDto.descripcion.Length > 250)
            {
                throw new TipoReclamoNoValidoException("La descripción es requerida y no puede superar los 90 caracteres");
            }

            if (tipoReclamoDto.tiempo_Max_Tratamiento < 0)
            {
                throw new TipoReclamoNoValidoException("El tiempo máximo de tratamiento no puede ser inferior a 0");
            }
        }

        public void eliminarTipoReclamo(int codTipoReclamoEliminar)
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                TipoReclamo tipoReclamoEliminar =
                    bd.TipoReclamos.Where(tr => tr.CodTipoReclamo == codTipoReclamoEliminar).FirstOrDefault();

                if (tipoReclamoEliminar == null) {
                    throw new TipoReclamoEnUsoException("No se puede eliminar el Tipo de Reclamo, hay Reclamos cargados con dicho tipo.");
                }

                bd.TipoReclamos.Remove(tipoReclamoEliminar);
                bd.SaveChanges();
            }
        }

        public TipoReclamoDto getTipoReclamo(int codTipoReclamo) {

            var tipoReclamoDomain = 
                dbContext.TipoReclamos
                .Where(tr => tr.CodTipoReclamo == codTipoReclamo)
                .FirstOrDefault();

            if (tipoReclamoDomain == null) {
                throw new TipoReclamoNotFoundException("No se encontro un Tipo de Reclamo con codigo: " + codTipoReclamo);
            }

            // Esto es para que me traiga datos de las entidades relacionadas.
            // por ahora no encontre otra forma de hacerlo. En teoria existe
            // un metodo include() que permite hacerlo pero no aparece como disponible.
            //dbContext.Entry(tipoReclamoDomain).Reference(s => s.IdUsuarioAltaNavigation).Load();
            //dbContext.Entry(tipoReclamoDomain).Reference(s => s.IdUsuarioModificacionNavigation).Load();

            return mapper.Map<TipoReclamoDto>(tipoReclamoDomain);

            //using (M_VPSA_V3Context bd = new M_VPSA_V3Context()) {
            //    tipoReclamoResponse = (from tipoReclamoQuery in bd.TipoReclamos
            //                           join usuarioAlta in bd.Usuarios
            //                             on tipoReclamoQuery.IdUsuarioAlta equals usuarioAlta.IdUsuario
            //                           join usuarioModificacion in bd.Usuarios
            //                             on tipoReclamoQuery.IdUsuarioModificacion equals usuarioModificacion.IdUsuario
            //                           where tipoReclamoQuery.CodTipoReclamo == codTipoReclamo
            //                           select new TipoReclamoDto
            //                           {
            //                               cod_Tipo_Reclamo = tipoReclamoQuery.CodTipoReclamo,
            //                               nombre = tipoReclamoQuery.Nombre,
            //                               descripcion = tipoReclamoQuery.Descripcion,
            //                               tiempo_Max_Tratamiento = tipoReclamoQuery.TiempoMaxTratamiento == null ? 0 : (int)tipoReclamoQuery.TiempoMaxTratamiento,
            //                               fechaAlta = (DateTime)tipoReclamoQuery.FechaAlta,
            //                               fechaModificacion = (DateTime)tipoReclamoQuery.FechaModificacion,
            //                               usuarioAlta = usuarioAlta.NombreUser,
            //                               usuarioModificacion = usuarioModificacion.NombreUser
            //                           }).Single();
            //}

            //if (tipoReclamoResponse == null) {
            //    throw new TipoReclamoNotFoundException("No se encontro un Tipo de Reclamo con codigo: " + codTipoReclamo);
            //}

            //return tipoReclamoResponse;
        }

        public IEnumerable<PrioridadReclamoDto> getPrioridades()
        {
            var prioridadesModelo = dbContext.PrioridadReclamos.Where(pri => pri.Bhabilitado == 1).ToList();
            return mapper.Map<List<PrioridadReclamoDto>>(prioridadesModelo);
        }

        private int getPrioridadReclamoSegunTipoReclamo(int codTipoReclamo) {

            // busco los datos del tipo de reclamo para obtener el tiempo max de tratamiento
            TipoReclamo tipoReclamo =
                dbContext.TipoReclamos.Where(tr => tr.CodTipoReclamo == codTipoReclamo)
                .FirstOrDefault();

            // Busco para el tiempo max de tratamiento del tipo de reclamo cual es la prioridad
            // que le corresponde.
            PrioridadReclamo prioridadReclamo = dbContext.PrioridadReclamos
                .Where(pr => tipoReclamo.TiempoMaxTratamiento <= pr.TiempoMaxTratamiento)
                .OrderBy(pr => pr.TiempoMaxTratamiento)
                .FirstOrDefault();

            // si no encontro uno se queda con la prioridad que tenga el tiempo max de tratamiento
            // mas alta
            if (prioridadReclamo == null)
            {
                prioridadReclamo = dbContext.PrioridadReclamos
                .OrderByDescending(pr => pr.TiempoMaxTratamiento)
                .FirstOrDefault();
            }

            return prioridadReclamo.NroPrioridad;
        }

        public ReclamoDto modificarReclamo(int nroReclamo, ModificarReclamoRequestDto reclamoDto)
        {
            /*Reclamo reclamo =
                dbContext.Reclamos
                .Where(rec => rec.NroReclamo == nroReclamo)
                .FirstOrDefault();

            if (reclamo != null) {

                if (reclamoDto.idUsuarioAsignado != null) {
                    reclamo.IdUsuario = reclamoDto.idUsuarioAsignado;
                }
                
                dbContext.Reclamos.Update(reclamo);
                dbContext.SaveChanges();
                return getReclamo(nroReclamo);
            }*/

            return new ReclamoDto();

        }

        public void aplicarAccion(AplicarAccionDto aplicarAccionDto)
        {
            if (aplicarAccionDto.codAccion.Equals("RECHAZAR")) {
                var reclamo = dbContext.Reclamos.Find(aplicarAccionDto.nroReclamo);
                reclamo.CodEstadoReclamo = (int)EstadoReclamoEnum.RECHAZADO;
                reclamo.FechaCierre = DateTime.Today;
                dbContext.Reclamos.Update(reclamo);

                ObservacionReclamo observacionReclamo = new ObservacionReclamo();
                observacionReclamo.CodAccion = aplicarAccionDto.codAccion;
                observacionReclamo.CodEstadoReclamo = (int)EstadoReclamoEnum.RECHAZADO;
                observacionReclamo.IdUsuarioAlta = int.Parse(aplicarAccionDto.idUsuario);
                observacionReclamo.NroReclamo = aplicarAccionDto.nroReclamo;
                observacionReclamo.Observacion = aplicarAccionDto.observacion;
                dbContext.ObservacionReclamos.Add(observacionReclamo);

                dbContext.SaveChanges();
            }

            if (aplicarAccionDto.codAccion.Equals("SUSPENDER")) {
                var reclamo = dbContext.Reclamos.Find(aplicarAccionDto.nroReclamo);
                reclamo.CodEstadoReclamo = (int)EstadoReclamoEnum.SUSPENDIDO;
                dbContext.Reclamos.Update(reclamo);

                ObservacionReclamo observacionReclamo = new ObservacionReclamo();
                observacionReclamo.CodAccion = aplicarAccionDto.codAccion;
                observacionReclamo.CodEstadoReclamo = (int)EstadoReclamoEnum.SUSPENDIDO;
                observacionReclamo.IdUsuarioAlta = int.Parse(aplicarAccionDto.idUsuario);
                observacionReclamo.NroReclamo = aplicarAccionDto.nroReclamo;
                observacionReclamo.Observacion = "El requerimiento pasa a estado SUSPENDIDO. " + aplicarAccionDto.observacion;
                dbContext.ObservacionReclamos.Add(observacionReclamo);

                dbContext.SaveChanges();
            }

            if (aplicarAccionDto.codAccion.Equals("CANCELAR")) {
                var reclamo = dbContext.Reclamos.Find(aplicarAccionDto.nroReclamo);
                reclamo.CodEstadoReclamo = (int)EstadoReclamoEnum.CANCELADO;
                reclamo.FechaCierre = DateTime.Today;
                dbContext.Reclamos.Update(reclamo);

                ObservacionReclamo observacionReclamo = new ObservacionReclamo();
                observacionReclamo.CodAccion = aplicarAccionDto.codAccion;
                observacionReclamo.CodEstadoReclamo = (int)EstadoReclamoEnum.CANCELADO;
                observacionReclamo.IdUsuarioAlta = int.Parse(aplicarAccionDto.idUsuario);
                observacionReclamo.NroReclamo = aplicarAccionDto.nroReclamo;
                observacionReclamo.Observacion = "El requerimiento pasa a estado CANCELADO. " + aplicarAccionDto.observacion;
                dbContext.ObservacionReclamos.Add(observacionReclamo);

                dbContext.SaveChanges();
            }

            if (aplicarAccionDto.codAccion.Equals("SOLUCIONAR")) {
                var reclamo = dbContext.Reclamos.Find(aplicarAccionDto.nroReclamo);
                reclamo.CodEstadoReclamo = (int)EstadoReclamoEnum.SOLUCIONADO;
                reclamo.FechaCierre = DateTime.Today;
                dbContext.Reclamos.Update(reclamo);

                ObservacionReclamo observacionReclamo = new ObservacionReclamo();
                observacionReclamo.CodAccion = aplicarAccionDto.codAccion;
                observacionReclamo.CodEstadoReclamo = (int)EstadoReclamoEnum.SOLUCIONADO;
                observacionReclamo.IdUsuarioAlta = int.Parse(aplicarAccionDto.idUsuario);
                observacionReclamo.NroReclamo = aplicarAccionDto.nroReclamo;
                observacionReclamo.Observacion = "El requerimiento pasa a estado SOLUCIONADO. " + aplicarAccionDto.observacion;
                dbContext.ObservacionReclamos.Add(observacionReclamo);

                dbContext.SaveChanges();
            }

            if (aplicarAccionDto.codAccion.Equals("ASIGNAR")) {
                var reclamo = dbContext.Reclamos.Find(aplicarAccionDto.nroReclamo);
                reclamo.NroArea = aplicarAccionDto.codArea;    


                var area = dbContext.Areas.Find(aplicarAccionDto.codArea);
                if ((reclamo.CodEstadoReclamo == ((int)EstadoReclamoEnum.CREADO) && area.CodArea != "ME")
                        || reclamo.CodEstadoReclamo == ((int)EstadoReclamoEnum.SUSPENDIDO)) {
                    reclamo.CodEstadoReclamo = (int?)EstadoReclamoEnum.EN_CURSO;
                }    

                dbContext.Reclamos.Update(reclamo);                

                
                ObservacionReclamo observacionReclamo = new ObservacionReclamo();
                observacionReclamo.CodAccion = aplicarAccionDto.codAccion;
                observacionReclamo.CodEstadoReclamo = (int)reclamo.CodEstadoReclamo;
                observacionReclamo.IdUsuarioAlta = int.Parse(aplicarAccionDto.idUsuario);
                observacionReclamo.NroReclamo = aplicarAccionDto.nroReclamo;
                observacionReclamo.Observacion = "Se asigna requerimiento al área " +
                    area.Nombre + ". " + aplicarAccionDto.observacion;
                dbContext.ObservacionReclamos.Add(observacionReclamo);

                dbContext.SaveChanges();
            }
            
        }

        public List<ObservacionReclamoDto> obtenerObservacionesDeReclamo(int nroReclamo) {

            List<ObservacionReclamoDto> result = new List<ObservacionReclamoDto>();
            var observaciones = dbContext.ObservacionReclamos.Where(obs => obs.NroReclamo == nroReclamo)
                .OrderBy(obs => obs.FechaAlta)
                .ToList();
            observaciones.ForEach(obs => {
                ObservacionReclamoDto observacion = new ObservacionReclamoDto
                {
                    id = obs.Id,
                    nroReclamo = obs.NroReclamo,
                    observacion = obs.Observacion,
                    codEstadoReclamo = obs.CodEstadoReclamo,
                    estadoReclamo = obs.CodEstadoReclamoNavigation.Nombre,
                    idUsuarioAlta = obs.IdUsuarioAlta,
                    usuarioAlta = obs.IdUsuarioAltaNavigation.NombreUser,
                    fechaAlta = obs.FechaAlta,
                    codAccion = obs.CodAccion
                };

                result.Add(observacion);

            });

            return result;

        }

        public List<EstadoReclamoDto> getEstadosReclamo()
        {
            List<EstadoReclamoDto> estados = new List<EstadoReclamoDto>();

            this.dbContext.EstadoReclamos
                .Where(est => est.Bhabilitado == 1)
                .OrderBy(est => est.Nombre)
                .ToList()
                .ForEach(est => {
                        EstadoReclamoDto estado = new EstadoReclamoDto{
                        codEstadoReclamo = est.CodEstadoReclamo,
                        nombre = est.Nombre           
                        };

                        estados.Add(estado);
                    }
                );

            return estados;
        }

        public OpcionesReclamoDto getOpcionesReclamo(int numeroReclamo, int idUsuario)
        {
            OpcionesReclamoDto opciones = new OpcionesReclamoDto();

            var reclamo = this.dbContext.Reclamos.Where(rec => rec.NroReclamo == numeroReclamo).FirstOrDefault();
            if (reclamo == null) {
                return opciones;
            }

            var usuario = this.dbContext.Usuarios.Where(usr => usr.IdUsuario == idUsuario).FirstOrDefault();

            opciones.nroReclamo = reclamo.NroReclamo;

            int codEstadoReclamo = (int)reclamo.CodEstadoReclamo;

            // Puede Asignar
            opciones.puedeAsignar = 
                (codEstadoReclamo == ((int)EstadoReclamoEnum.CREADO) || codEstadoReclamo == ((int)EstadoReclamoEnum.SUSPENDIDO)
                || codEstadoReclamo == ((int)EstadoReclamoEnum.EN_CURSO)) && usuario.IdTipoUsuarioNavigation?.TipoRol == "EMPLEADO";
            
            // Puede Cargar Trabajo            
            opciones.puedeCargarTrabajo = codEstadoReclamo == ((int)EstadoReclamoEnum.EN_CURSO)
                && usuario.IdTipoUsuarioNavigation?.TipoRol == "EMPLEADO";

            // Puede Rechazar
            opciones.puedeRechazar = codEstadoReclamo == ((int)EstadoReclamoEnum.CREADO) 
                && usuario.IdTipoUsuarioNavigation?.TipoRol == "EMPLEADO";

            // Puede Finalizar
            opciones.puedeFinalizar = codEstadoReclamo == ((int)EstadoReclamoEnum.EN_CURSO)
                && usuario.IdTipoUsuarioNavigation.CodRol == "MDE";

            // Puede Ver Observaciones
            opciones.puedeVerObservaciones = usuario.IdTipoUsuarioNavigation?.TipoRol == "EMPLEADO"
                && codEstadoReclamo != ((int)EstadoReclamoEnum.CREADO);

            // Puede Ver Trabajos
            opciones.puedeVerTrabajos = codEstadoReclamo != ((int)EstadoReclamoEnum.CREADO)
                && codEstadoReclamo != ((int)EstadoReclamoEnum.RECHAZADO);

            // Puede Suspender
            opciones.puedeSuspender = usuario.IdTipoUsuarioNavigation?.TipoRol == "EMPLEADO"
                && codEstadoReclamo == ((int)EstadoReclamoEnum.EN_CURSO);

            return opciones;
        }
    }
}
