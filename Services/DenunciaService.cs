using MVPSA_V2022.clases;
using MVPSA_V2022.Exceptions;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Services
{
    public class DenunciaService //: IDenunciaService
    {

        //void IDenunciaService.eliminarTipoDenuncia(int codTipoDenunciaEliminar)
        //{
        //    using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
        //    {
        //        Boolean codTipoDenunciaEstaEnUso =
        //            bd.Denuncia.Where(denuncia => denuncia.CodTipoDenuncia == codTipoDenunciaEliminar).Count() > 0;

        //        if (codTipoDenunciaEstaEnUso)
        //        {
        //            throw new Exception("No se puede eliminar el Tipo de Denuncia, hay Denuncias cargadas con dicho tipo.");
        //        }

        //        TipoDenuncium tipoDenunciaEliminar =
        //            bd.TipoDenuncia.Where(tr => tr.CodTipoDenuncia == codTipoDenunciaEliminar).Single();
        //        bd.TipoDenuncia.Remove(tipoDenunciaEliminar);
        //        bd.SaveChanges();
        //    }

        //}



        //TipoDenunciaCLS IDenunciaService.getTipoDenuncia(int codTipoDenuncia)
        //{
        //    TipoDenunciaCLS tipoDenunciaResponse;

        //    using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
        //    {
        //        tipoDenunciaResponse = (from tipoDenunciaQuery in bd.TipoDenuncia
        //                                join usuarioAlta in bd.Usuarios
        //                                 on tipoDenunciaQuery.IdUsuarioAlta equals usuarioAlta.IdUsuario
        //                               join usuarioModificacion in bd.Usuarios
        //                                 on tipoDenunciaQuery.IdUsuarioModificacion equals usuarioModificacion.IdUsuario
        //                               where tipoDenunciaQuery.CodTipoDenuncia == codTipoDenuncia
        //                                select new TipoDenunciaCLS
        //                               {
        //                                   Cod_Tipo_Denuncia = tipoDenunciaQuery.CodTipoDenuncia,
        //                                   Nombre = tipoDenunciaQuery.Nombre,
        //                                   Descripcion = tipoDenunciaQuery.Descripcion,
        //                                   Tiempo_Max_Tratamiento = tipoDenunciaQuery.TiempoMaxTratamiento == null ? 0 : (int)tipoDenunciaQuery.TiempoMaxTratamiento,
        //                                   fechaAlta = (DateTime)tipoDenunciaQuery.FechaAlta,
        //                                   fechaModificacion = (DateTime)tipoDenunciaQuery.FechaModificacion,
        //                                   usuarioAlta = usuarioAlta.NombreUser,
        //                                   usuarioModificacion = usuarioModificacion.NombreUser
        //                               }).Single();
        //    }

        //    if (tipoDenunciaResponse == null)
        //    {
        //        throw new Exception("No se encontro un Tipo de Denuncia con codigo: " + codTipoDenuncia);
        //    }

        //    return tipoDenunciaResponse;
        //}

        //TipoDenunciaCLS IDenunciaService.guardarTipoDenuncia(TipoDenunciaCLS tipoDenunciaDto, int idUsuarioAlta)
        //{
        //    validarTipoDenuncia(tipoDenunciaDto);

        //    TipoDenuncium tipoDenuncia = new TipoDenuncium();
        //    tipoDenuncia.Nombre = tipoDenunciaDto.Nombre;
        //    tipoDenuncia.Descripcion = tipoDenunciaDto.Descripcion;
        //    tipoDenuncia.TiempoMaxTratamiento = tipoDenunciaDto.Tiempo_Max_Tratamiento;
        //    //para hacer scaffold tipoDenuncia.IdUsuarioAlta = idUsuarioAlta;
        //    //para hacer scaffold tipoDenuncia.IdUsuarioModificacion = idUsuarioAlta;
        //    tipoDenuncia.Bhabilitado = 1;

        //    using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
        //    {
        //        bd.TipoDenuncia.Add(tipoDenuncia);
        //        bd.SaveChanges();

        //        tipoDenunciaDto = (from tipoDenunciaQuery in bd.TipoDenuncia
        //                          join usuarioAlta in bd.Usuarios
        //                            on tipoDenunciaQuery.IdUsuarioAlta equals usuarioAlta.IdUsuario
        //                          join usuarioModificacion in bd.Usuarios
        //                            on tipoDenunciaQuery.IdUsuarioModificacion equals usuarioModificacion.IdUsuario
        //                          where tipoDenunciaQuery.CodTipoDenuncia == tipoDenuncia.CodTipoDenuncia
        //                          select new TipoDenunciaCLS
        //                          {
        //                              Cod_Tipo_Denuncia = tipoDenunciaQuery.CodTipoDenuncia,
        //                              Nombre = tipoDenunciaQuery.Nombre,
        //                              Descripcion = tipoDenunciaQuery.Descripcion,
        //                              Tiempo_Max_Tratamiento = tipoDenunciaQuery.TiempoMaxTratamiento == null ? 0 : (int)tipoDenuncia.TiempoMaxTratamiento,
        //                              usuarioAlta = usuarioAlta.NombreUser,
        //                              usuarioModificacion = usuarioModificacion.NombreUser
        //                          })
        //                            .Single();
        //    }

        //    return tipoDenunciaDto;
        //}

        //IEnumerable<TipoDenunciaCLS> IDenunciaService.listarTiposDenuncia()
        //{
        //    List<TipoDenunciaCLS> listaTiposDenuncia;
        //    using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
        //    {
        //        listaTiposDenuncia = (from tipoDenuncia in bd.TipoDenuncia
        //                             join usuarioAlta in bd.Usuarios
        //                               on tipoDenuncia.IdUsuarioAlta equals usuarioAlta.IdUsuario
        //                             join usuarioModificacion in bd.Usuarios
        //                               on tipoDenuncia.IdUsuarioModificacion equals usuarioModificacion.IdUsuario
        //                             where tipoDenuncia.Bhabilitado == 1
        //                             select new TipoDenunciaCLS
        //                             {
        //                                 Cod_Tipo_Denuncia = tipoDenuncia.CodTipoDenuncia,
        //                                 Nombre = tipoDenuncia.Nombre,
        //                                 Descripcion = tipoDenuncia.Descripcion,
        //                                 Tiempo_Max_Tratamiento = tipoDenuncia.TiempoMaxTratamiento == null ? 0 : (int)tipoDenuncia.TiempoMaxTratamiento,
        //                                 usuarioAlta = usuarioAlta.NombreUser,
        //                                 usuarioModificacion = usuarioModificacion.NombreUser
        //                             })
        //                            .OrderBy(tr => tr.Cod_Tipo_Denuncia)
        //                            .ToList();
        //        return listaTiposDenuncia;
        //    }


        //}


        //void validarTipoDenuncia(TipoDenunciaCLS tipoDenuncia)
        //{
        //    if (tipoDenuncia.Nombre == null || tipoDenuncia.Nombre.Length == 0
        //        || tipoDenuncia.Nombre.Length > 90)
        //    {
        //        throw new Exception("El nombre es requerido y no puede superar los 90 caracteres");
        //    }

        //    if (tipoDenuncia.Descripcion == null || tipoDenuncia.Descripcion.Length == 0
        //        || tipoDenuncia.Descripcion.Length > 250)
        //    {
        //        throw new Exception("La descripción es requerida y no puede superar los 90 caracteres");
        //    }

        //    if (tipoDenuncia.Tiempo_Max_Tratamiento < 0)
        //    {
        //        throw new Exception("El tiempo máximo de tratamiento no puede ser inferior a 0");
        //    }
        //}

        //TipoDenunciaCLS IDenunciaService.modificarTipoDenuncia(TipoDenunciaCLS tipoDenunciaDto, int idUsuarioModificacion)
        //{
        //    validarTipoDenuncia(tipoDenunciaDto);

        //    TipoDenuncium tipoDenuncia = new TipoDenuncium();
        //    using (M_VPSA_V3Context bd = new M_VPSA_V3Context())
        //    {
        //        tipoDenuncia =
        //            bd.TipoDenuncia.Where(tr => tr.CodTipoDenuncia == tipoDenunciaDto.Cod_Tipo_Denuncia).Single();
        //        tipoDenuncia.Nombre = tipoDenunciaDto.Nombre;
        //        tipoDenuncia.Descripcion = tipoDenunciaDto.Descripcion;
        //        tipoDenuncia.TiempoMaxTratamiento = tipoDenunciaDto.Tiempo_Max_Tratamiento;
        //     //   tipoDenuncia.FechaModificacion = DateTime.Now;
        //       // tipoDenuncia.IdUsuarioModificacion = idUsuarioModificacion;
        //        bd.SaveChanges();

        //        tipoDenunciaDto = (from tipoDenunciaQuery in bd.TipoDenuncia
        //                           join usuarioAlta in bd.Usuarios
        //                            on tipoDenunciaQuery.IdUsuarioAlta equals usuarioAlta.IdUsuario
        //                           join usuarioModificacion in bd.Usuarios
        //                             on tipoDenunciaQuery.IdUsuarioModificacion equals usuarioModificacion.IdUsuario
        //                           where tipoDenunciaQuery.CodTipoDenuncia == tipoDenuncia.CodTipoDenuncia
        //                           select new TipoDenunciaCLS
        //                           {
        //                               Cod_Tipo_Denuncia = tipoDenunciaQuery.CodTipoDenuncia,
        //                               Nombre = tipoDenunciaQuery.Nombre,
        //                               Descripcion = tipoDenunciaQuery.Descripcion,
        //                               Tiempo_Max_Tratamiento = tipoDenunciaQuery.TiempoMaxTratamiento == null ? 0 : (int)tipoDenunciaQuery.TiempoMaxTratamiento,
        //                               fechaAlta = (DateTime)tipoDenunciaQuery.FechaAlta,
        //                               fechaModificacion = (DateTime)tipoDenunciaQuery.FechaModificacion,
        //                               usuarioAlta = usuarioAlta.NombreUser,
        //                               usuarioModificacion = usuarioModificacion.NombreUser
        //                           })
        //                            .Single();
        //    }

        //    return tipoDenunciaDto;
        //}


    }

}
