using AutoMapper;
using MVPSA_V2022.clases;
using MVPSA_V2022.Exceptions;
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

        /**
         * 
         * Tipos de Reclamo
         * 
         */
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
                                        descripcion = tipoReclamo.Descripcion,
                                        tiempo_Max_Tratamiento = tipoReclamo.TiempoMaxTratamiento == null ? 0 :(int) tipoReclamo.TiempoMaxTratamiento
                                    })
                                    .OrderBy(tr => tr.cod_Tipo_Reclamo)
                                    .ToList();
                return listaTiposReclamo;
            }
        }

        public TipoReclamoCLS guardarTipoReclamo(TipoReclamoCLS tipoReclamoDto) {

            validarTipoReclamo(tipoReclamoDto);

            TipoReclamo tipoReclamo = new TipoReclamo();
            tipoReclamo.Nombre = tipoReclamoDto.nombre;
            tipoReclamo.Descripcion = tipoReclamoDto.descripcion;
            tipoReclamo.TiempoMaxTratamiento = tipoReclamoDto.tiempo_Max_Tratamiento;
            tipoReclamo.Bhabilitado = 1;

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context()) {
                bd.TipoReclamos.Add(tipoReclamo);
                bd.SaveChanges();
            }

            tipoReclamoDto.cod_Tipo_Reclamo = tipoReclamo.CodTipoReclamo;
            tipoReclamoDto.nombre = tipoReclamo.Nombre;
            tipoReclamoDto.descripcion = tipoReclamo.Descripcion;
            tipoReclamoDto.tiempo_Max_Tratamiento = tipoReclamo.TiempoMaxTratamiento == null ? 0 : (int)tipoReclamo.TiempoMaxTratamiento;

            return tipoReclamoDto;

        }

        private void validarTipoReclamo(TipoReclamoCLS tipoReclamoDto) {
            if (tipoReclamoDto.nombre == null || tipoReclamoDto.nombre.Length == 0
                || tipoReclamoDto.nombre.Length > 90) {
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

        public TipoReclamoCLS modificarTipoReclamo(TipoReclamoCLS tipoReclamoDto)
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

                bd.SaveChanges();
            }

            tipoReclamoDto.cod_Tipo_Reclamo = tipoReclamo.CodTipoReclamo;
            tipoReclamoDto.nombre = tipoReclamo.Nombre;
            tipoReclamoDto.descripcion = tipoReclamo.Descripcion;
            tipoReclamoDto.tiempo_Max_Tratamiento = tipoReclamo.TiempoMaxTratamiento == null ? 0 : (int)tipoReclamo.TiempoMaxTratamiento;

            return tipoReclamoDto;
        }

        public void eliminarTipoReclamo(int codTipoReclamoEliminar)
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                Boolean codTipoReclamoEstaEnUso = 
                    bd.Reclamos.Where(reclamo => reclamo.CodTipoReclamo == codTipoReclamoEliminar).Count() > 0;

                if (codTipoReclamoEstaEnUso) {
                    throw new TipoReclamoEnUsoException("No se puede eliminar el Tipo de Reclamo, hay Reclamos cargados con dicho tipo.");
                }

                TipoReclamo tipoReclamoEliminar = 
                    bd.TipoReclamos.Where(tr => tr.CodTipoReclamo == codTipoReclamoEliminar).Single();
                bd.TipoReclamos.Remove(tipoReclamoEliminar);
                bd.SaveChanges();
            }
        }

        public TipoReclamoCLS getTipoReclamo(int codTipoReclamo) {
            TipoReclamo tipoReclamo;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())            {
                tipoReclamo =
                    bd.TipoReclamos
                    .Where(tr => tr.CodTipoReclamo == codTipoReclamo)
                    .SingleOrDefault();
                bd.SaveChanges();
            }

            if (tipoReclamo == null) {
                throw new TipoReclamoNotFoundException("No se encontro un Tipo de Reclamo con codigo: " + codTipoReclamo);
            }

            // ToDo usar el mapper para esto
            TipoReclamoCLS tipoReclamoResponse = new TipoReclamoCLS();
            tipoReclamoResponse.cod_Tipo_Reclamo = tipoReclamo.CodTipoReclamo;
            tipoReclamoResponse.nombre = tipoReclamo.Nombre;
            tipoReclamoResponse.descripcion = tipoReclamo.Descripcion;
            tipoReclamoResponse.tiempo_Max_Tratamiento = tipoReclamo.TiempoMaxTratamiento == null ? 0 : (int)tipoReclamo.TiempoMaxTratamiento;
            tipoReclamoResponse.fechaAlta = (DateTime)tipoReclamo.FechaAlta;
            tipoReclamoResponse.fechaModificacion = (DateTime)tipoReclamo.FechaModificacion;

            return tipoReclamoResponse;
        }
    }
}
