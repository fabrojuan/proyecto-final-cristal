using AutoMapper;
using MVPSA_V2022.clases;
using MVPSA_V2022.Exceptions;
using MVPSA_V2022.Modelos;
using System.Linq;

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
            ReclamoDto respuesta = new ReclamoDto();
            Reclamo reclamo = new Reclamo();

            try
            {
                reclamo = mapper.Map<Reclamo>(reclamoCLS);
                reclamo.CodEstadoReclamo = (int?) EstadoReclamoEnum.NUEVO;
                reclamo.Bhabilitado = 1;
                reclamo.IdUsuario = idUsuarioAlta;
                reclamo.NroPrioridad = getPrioridadReclamoSegunTipoReclamo(reclamoCLS.codTipoReclamo);
                reclamo.Fecha = DateTime.Now;

                Usuario usuarioAlta = dbContext.Usuarios.Where(usr => usr.IdUsuario == idUsuarioAlta).FirstOrDefault();

                if (usuarioAlta.IdTipoUsuario == 2000) {
                    // Si es un usuario vecino
                    reclamo.IdVecino = idUsuarioAlta;
                }

                using (M_VPSA_V3Context bd = new M_VPSA_V3Context()) {
                    
                    // Guarda la foto 1 si el vecino la cargo
                    if (reclamoCLS.foto1 != null && reclamoCLS.foto1.Length > 0) {
                        PruebaGraficaReclamo pruebaGraficaReclamo1 = new PruebaGraficaReclamo();
                        pruebaGraficaReclamo1.IdVecino = idUsuarioAlta;
                        pruebaGraficaReclamo1.Bhabilitado = 1;

                        reclamo.PruebaGraficaReclamos.Add(pruebaGraficaReclamo1);
                    }

                    // Guarda la foto 2 si el vecino la cargo
                    if (reclamoCLS.foto2 != null && reclamoCLS.foto2.Length > 0)
                    {
                        PruebaGraficaReclamo pruebaGraficaReclamo2 = new PruebaGraficaReclamo();
                        pruebaGraficaReclamo2.IdVecino = idUsuarioAlta;
                        pruebaGraficaReclamo2.Bhabilitado = 1;

                        reclamo.PruebaGraficaReclamos.Add(pruebaGraficaReclamo2);
                    }

                    // Guarda el reclamo con las imagenes
                    bd.Reclamos.Add(reclamo);
                    bd.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("Se produjo un error y no se pudo guardar el Reclamo");
            }

            return mapper.Map<ReclamoDto>(reclamo);
        }

        public IEnumerable<ReclamoDto> listarReclamos()
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                List<ReclamoDto> listaReclamo = (from reclamo in bd.ReclamosVw
                                                 where reclamo.Bhabilitado == 1
                                                 select new ReclamoDto
                                                 {
                                                     nroReclamo = reclamo.NroReclamo,
                                                     descripcion = reclamo.Descripcion,
                                                     codTipoReclamo = reclamo.CodTipoReclamo,
                                                     codEstadoReclamo = reclamo.CodEstadoReclamo,
                                                     Bhabilitado = reclamo.Bhabilitado,
                                                     calle = reclamo.Calle,
                                                     altura = reclamo.Altura,
                                                     entreCalles = reclamo.EntreCalles,
                                                     idVecino = reclamo.IdVecino,
                                                     idUsuario = reclamo.IdUsuario,
                                                     Fecha = (DateTime)reclamo.Fecha,
                                                     estadoReclamo = reclamo.EstadoReclamo,
                                                     tipoReclamo = reclamo.TipoReclamo,
                                                     nombreYapellido = reclamo.NomApeVecino,
                                                     nroPrioridad = reclamo.NroPrioridad,
                                                     usuarioAsignado = reclamo.Usuario,
                                                     empleadoAsignado = reclamo.Empleado,
                                                     prioridad = reclamo.PrioridadReclamo
                                                 }).ToList();

                return listaReclamo;
            }
        }

        public ReclamoDto getReclamo(int nroReclamo)
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())            {

                ReclamoDto reclamoResponse = (from reclamo in bd.ReclamosVw
                                                 where reclamo.Bhabilitado == 1
                                                 && reclamo.NroReclamo == nroReclamo
                                              select new ReclamoDto
                                                 {
                                                  nroReclamo = reclamo.NroReclamo,
                                                  descripcion = reclamo.Descripcion,
                                                  codTipoReclamo = reclamo.CodTipoReclamo,
                                                  codEstadoReclamo = reclamo.CodEstadoReclamo,
                                                  Bhabilitado = reclamo.Bhabilitado,
                                                  calle = reclamo.Calle,
                                                  altura = reclamo.Altura,
                                                  entreCalles = reclamo.EntreCalles,
                                                  idVecino = reclamo.IdVecino,
                                                  idUsuario = reclamo.IdUsuario,
                                                  Fecha = (DateTime)reclamo.Fecha,
                                                  estadoReclamo = reclamo.EstadoReclamo,
                                                  tipoReclamo = reclamo.TipoReclamo,
                                                  nombreYapellido = reclamo.NomApeVecino,
                                                  nroPrioridad = reclamo.NroPrioridad,
                                                  usuarioAsignado = reclamo.Usuario,
                                                  empleadoAsignado = reclamo.Empleado,
                                                  prioridad = reclamo.PrioridadReclamo
                                              }).Single();


                List<PruebaGraficaReclamo> listaPruebaGraficaReclamo = 
                    bd.PruebaGraficaReclamos.Where(pgr => pgr.NroReclamo == nroReclamo)
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
                                        tiempo_Max_Tratamiento = tipoReclamo.TiempoMaxTratamiento,
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
                                         tiempo_Max_Tratamiento = tipoReclamoQuery.TiempoMaxTratamiento,
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
                                      tiempo_Max_Tratamiento = tipoReclamoQuery.TiempoMaxTratamiento,
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
            dbContext.Entry(tipoReclamoDomain).Reference(s => s.UsuarioAlta).Load();
            dbContext.Entry(tipoReclamoDomain).Reference(s => s.UsuarioModificacion).Load();

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
            Reclamo reclamo =
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
            }

            return new ReclamoDto();

        }
    }
}
