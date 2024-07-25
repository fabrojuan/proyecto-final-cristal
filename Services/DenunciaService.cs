using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVPSA_V2022.clases;
using MVPSA_V2022.clases.Mobbex;
using MVPSA_V2022.Enums;
using MVPSA_V2022.Exceptions;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Services
{
    public class DenunciaService : IDenunciaService
    {
        public readonly IMapper mapper;
        public readonly IUsuarioService usuarioService;
        private readonly M_VPSA_V3Context dbContext;
       // private readonly CristalContext dbContext;

        public DenunciaService(IMapper mapper, IUsuarioService usuarioService,
       M_VPSA_V3Context dbContext)
   ///  reeemplazar por este sino funca              M_VPSA_V3Context dbContext)

        {
            this.mapper = mapper;
            this.usuarioService = usuarioService;
            this.dbContext = dbContext;
        }

        void IDenunciaService.eliminarTipoDenuncia(int codTipoDenunciaEliminar)
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                Boolean codTipoDenunciaEstaEnUso =
                    bd.Denuncia.Where(denuncia => denuncia.CodTipoDenuncia == codTipoDenunciaEliminar).Count() > 0;

                if (codTipoDenunciaEstaEnUso)
                {
                    throw new Exception("No se puede eliminar el Tipo de Denuncia, hay Denuncias cargadas con dicho tipo.");
                }

                TipoDenuncium tipoDenunciaEliminar =
                    bd.TipoDenuncia.Where(tr => tr.CodTipoDenuncia == codTipoDenunciaEliminar).Single();
                bd.TipoDenuncia.Remove(tipoDenunciaEliminar);
                bd.SaveChanges();
            }

        }



        TipoDenunciaCLS IDenunciaService.getTipoDenuncia(int codTipoDenuncia)
        {
            TipoDenunciaCLS tipoDenunciaResponse = null;
            Console.WriteLine("Con writeline llego a buscar la denuncia");
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                tipoDenunciaResponse = (from tipoDenunciaQuery in bd.TipoDenuncia
                                        join usuarioAlta in bd.Usuarios
                                         on tipoDenunciaQuery.IdUsuarioAlta equals usuarioAlta.IdUsuario
                                        join usuarioModificacion in bd.Usuarios
                                          on tipoDenunciaQuery.IdUsuarioModificacion equals usuarioModificacion.IdUsuario
                                        where tipoDenunciaQuery.CodTipoDenuncia == codTipoDenuncia
                                        select new TipoDenunciaCLS
                                        {
                                            cod_Tipo_Denuncia = tipoDenunciaQuery.CodTipoDenuncia,
                                            Nombre = !String.IsNullOrEmpty(tipoDenunciaQuery.Nombre)? tipoDenunciaQuery.Nombre: "No posee",
                                            
                                            Descripcion = tipoDenunciaQuery.Descripcion,
                                            Tiempo_Max_Tratamiento = tipoDenunciaQuery.TiempoMaxTratamiento == null ? 0 : (int)tipoDenunciaQuery.TiempoMaxTratamiento,
                                            fechaAlta = (DateTime)tipoDenunciaQuery.FechaAlta,
                                            fechaModificacion = (DateTime)tipoDenunciaQuery.FechaModificacion,
                                            usuarioAlta = usuarioAlta.NombreUser,
                                            usuarioModificacion = usuarioModificacion.NombreUser
                                        }).Single();
            }

            if (tipoDenunciaResponse == null)
            {
                throw new Exception("No se encontro un Tipo de Denuncia con codigo: " + codTipoDenuncia);
            }

            return tipoDenunciaResponse;
        }

        TipoDenunciaCLS IDenunciaService.guardarTipoDenuncia(TipoDenunciaCLS tipoDenunciaDto, int idUsuarioAlta)
        {
            validarTipoDenuncia(tipoDenunciaDto);

            TipoDenuncium tipoDenuncia = new TipoDenuncium();
            tipoDenuncia.Nombre = tipoDenunciaDto.Nombre;
            tipoDenuncia.Descripcion = tipoDenunciaDto.Descripcion;
            tipoDenuncia.TiempoMaxTratamiento = tipoDenunciaDto.Tiempo_Max_Tratamiento;
            //para hacer scaffold tipoDenuncia.IdUsuarioAlta = idUsuarioAlta;
            //para hacer scaffold tipoDenuncia.IdUsuarioModificacion = idUsuarioAlta;
            tipoDenuncia.Bhabilitado = 1;

            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                bd.TipoDenuncia.Add(tipoDenuncia);
                bd.SaveChanges();

            }

            return tipoDenunciaDto;
        }

        IEnumerable<TipoDenunciaCLS> IDenunciaService.listarTiposDenuncia()
        {
            List<TipoDenunciaCLS> listaTiposDenuncia = null;
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                listaTiposDenuncia = (from tipoDenuncia in bd.TipoDenuncia
                                      join usuarioAlta in bd.Usuarios
                                        on tipoDenuncia.IdUsuarioAlta equals usuarioAlta.IdUsuario
                                      join usuarioModificacion in bd.Usuarios
                                        on tipoDenuncia.IdUsuarioModificacion equals usuarioModificacion.IdUsuario
                                      where tipoDenuncia.Bhabilitado == 1
                                      select new TipoDenunciaCLS
                                      {
                                          cod_Tipo_Denuncia = tipoDenuncia.CodTipoDenuncia,
                                          Nombre = !String.IsNullOrEmpty(tipoDenuncia.Nombre) ? tipoDenuncia.Nombre : "No posee",
                                          Descripcion = tipoDenuncia.Descripcion,
                                          Tiempo_Max_Tratamiento = tipoDenuncia.TiempoMaxTratamiento == null ? 0 : (int)tipoDenuncia.TiempoMaxTratamiento,
                                          usuarioAlta = usuarioAlta.NombreUser,
                                          usuarioModificacion = usuarioModificacion.NombreUser
                                      })
                                    .OrderBy(tr => tr.cod_Tipo_Denuncia)
                                    .ToList();
                return listaTiposDenuncia;
            }
        }

         IEnumerable<DenunciaCLS2> IDenunciaService.ListarDenunciasCerradas()
        {
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                Prioridad oPriorodad = new Prioridad();
                UsuarioCLS usuarioCLS = new UsuarioCLS();

                List<DenunciaCLS2> listaDenunciaCerradas = (from Denuncia in bd.Denuncia
                                                    join EstadoDenuncia in bd.EstadoDenuncia
                                                   on Denuncia.CodEstadoDenuncia equals EstadoDenuncia.CodEstadoDenuncia
                                                    join TipoDenuncia in bd.TipoDenuncia
                                                    on Denuncia.CodTipoDenuncia equals TipoDenuncia.CodTipoDenuncia
                                                    join Prioridad in bd.Prioridads
                                                    on Denuncia.NroPrioridad equals Prioridad.NroPrioridad
                                                    join Usuario in bd.Usuarios
                                                    on Denuncia.IdUsuario equals Usuario.IdUsuario
                                                    where Denuncia.Bhabilitado == 0
                                                    select new DenunciaCLS2
                                                    {
                                                        Nro_Denuncia = Denuncia.NroDenuncia,
                                                        Fecha = (DateTime)Denuncia.Fecha,
                                                        Estado_Denuncia = EstadoDenuncia.Nombre,
                                                        Tipo_Denuncia = !String.IsNullOrEmpty(TipoDenuncia.Nombre) ? TipoDenuncia.Nombre : "No Posee",
                                                        Prioridad = Prioridad.NombrePrioridad,
                                                        IdUsuario = (int)((Denuncia.IdUsuario.HasValue) ? Denuncia.IdUsuario : 0),
                                                        NombreUser = (string)Usuario.NombreUser

                                                    }).ToList();
                return listaDenunciaCerradas;
            }

        }

                //Con filtros
        public IEnumerable<DenunciaCLS2> listarDenunciasconFiltros(int idUsuarioConectado, int tipo, int estado)
        {

             var usuarioConectado = dbContext.Usuarios.Where(usu => usu.IdUsuario == idUsuarioConectado).FirstOrDefault();
            String codRolUsuarioConectado = usuarioConectado.IdTipoUsuarioNavigation.NombreRol;


              //var query1 = dbContext.VwDenuncia;
                //.Where(den => den.Bhabilitado >= 0);

            var query = dbContext.Denuncia.Where(den => den.Bhabilitado == 1);
            //if (codRolUsuarioConectado != "MDE" && codRolUsuarioConectado != "INT"
            //    && codRolUsuarioConectado != "ADS")
            //{
            //    query = query.Where(den => den. == usuarioConectado.IdTipoUsuario);
            //}

            if (tipo != 0)
            {
                query = query.Where(den => den.CodTipoDenuncia == tipo);
            }

            if (estado != 0)
            {
                query = query.Where(den => den.CodEstadoDenuncia == estado);
            }

            List<DenunciaCLS2> listaDenuncia = new List<DenunciaCLS2>();
            query.ToList().ForEach(denuncia =>
            {
                DenunciaCLS2 denunciaCLS2 = new DenunciaCLS2
                {
                    Nro_Denuncia = denuncia.NroDenuncia,
                    Fecha = (DateTime)denuncia.Fecha,
                    Estado_Denuncia = denuncia?.CodEstadoDenunciaNavigation?.Nombre,
                    Tipo_Denuncia = denuncia?.CodTipoDenunciaNavigation?.Nombre,
                    Prioridad = denuncia?.NroPrioridadNavigation?.NombrePrioridad,
                    IdUsuario = (int)((denuncia.IdUsuario.HasValue) ? denuncia.IdUsuario : 0),
                    NombreUser = denuncia?.IdUsuarioNavigation?.NombreUser

            };
                listaDenuncia.Add(denunciaCLS2);
            });
            return listaDenuncia;
        }

        void validarTipoDenuncia(TipoDenunciaCLS tipoDenuncia)
        {
            if (tipoDenuncia.Nombre == null || tipoDenuncia.Nombre.Length == 0
                || tipoDenuncia.Nombre.Length > 90)
            {
                throw new Exception("El nombre es requerido y no puede superar los 90 caracteres");
            }

            if (tipoDenuncia.Descripcion == null || tipoDenuncia.Descripcion.Length == 0
                || tipoDenuncia.Descripcion.Length > 250)
            {
                throw new Exception("La descripción es requerida y no puede superar los 90 caracteres");
            }

            if (tipoDenuncia.Tiempo_Max_Tratamiento < 0)
            {
                throw new Exception("El tiempo máximo de tratamiento no puede ser inferior a 0");
            }
        }

        TipoDenunciaCLS IDenunciaService.modificarTipoDenuncia(TipoDenunciaCLS tipoDenunciaDto, int idUsuarioModificacion)
        {
            validarTipoDenuncia(tipoDenunciaDto);

            TipoDenuncium tipoDenuncia = new TipoDenuncium();
            using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
            {
                tipoDenuncia =
                    bd.TipoDenuncia.Where(tr => tr.CodTipoDenuncia == tipoDenunciaDto.cod_Tipo_Denuncia).Single();
                tipoDenuncia.Nombre = tipoDenunciaDto.Nombre;
                tipoDenuncia.Descripcion = tipoDenunciaDto.Descripcion;
                tipoDenuncia.TiempoMaxTratamiento = tipoDenunciaDto.Tiempo_Max_Tratamiento;
                //   tipoDenuncia.FechaModificacion = DateTime.Now;
                // tipoDenuncia.IdUsuarioModificacion = idUsuarioModificacion;
                bd.SaveChanges();

                /*tipoDenunciaDto = (from tipoDenunciaQuery in bd.TipoDenuncia
                                   join usuarioAlta in bd.Usuarios
                                    on tipoDenunciaQuery.IdUsuarioAlta equals usuarioAlta.IdUsuario
                                   join usuarioModificacion in bd.Usuarios
                                     on tipoDenunciaQuery.IdUsuarioModificacion equals usuarioModificacion.IdUsuario
                                   where tipoDenunciaQuery.CodTipoDenuncia == tipoDenuncia.CodTipoDenuncia
                                   select new TipoDenunciaCLS
                                   {
                                       Cod_Tipo_Denuncia = tipoDenunciaQuery.CodTipoDenuncia,
                                       Nombre = tipoDenunciaQuery.Nombre,
                                       Descripcion = tipoDenunciaQuery.Descripcion,
                                       Tiempo_Max_Tratamiento = tipoDenunciaQuery.TiempoMaxTratamiento == null ? 0 : (int)tipoDenunciaQuery.TiempoMaxTratamiento,
                                       fechaAlta = (DateTime)tipoDenunciaQuery.FechaAlta,
                                       fechaModificacion = (DateTime)tipoDenunciaQuery.FechaModificacion,
                                       usuarioAlta = usuarioAlta.NombreUser,
                                       usuarioModificacion = usuarioModificacion.NombreUser
                                   })
                                    .Single();*/
            }

            return tipoDenunciaDto;
        }


    }

}
