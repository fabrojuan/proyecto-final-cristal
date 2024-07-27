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
    public class LoteService : ILoteService
    {
        public readonly IMapper mapper;
        public readonly IUsuarioService usuarioService;
        private readonly M_VPSA_V3Context dbContext;
        public LoteService(IMapper mapper, IUsuarioService usuarioService,
       M_VPSA_V3Context dbContext)
          {
            this.mapper = mapper;
            this.usuarioService = usuarioService;
            this.dbContext = dbContext;
        }

        public LoteCLS mostrarLoteById(int idLote)
        {
            var lote = dbContext.Lotes.Where(l => l.IdLote == idLote && l.Bhabilitado == 1)
                                      .FirstOrDefault();

            if (lote == null)
            {
                //return NotFound(); // Devuelve null si no se encuentra el lote
            }

            return new LoteCLS
            {
                Calle = lote.Calle ?? "No Posee",
                Altura = lote.Altura,
                SupEdificada = lote.SupEdificada,
                NomenclaturaCatastral = lote.NomenclaturaCatastral,
                EstadoDeuda = lote.EstadoDeuda,
                DniTitular = lote.IdPersonaNavigation?.Dni,
                Esquina = lote.Esquina,
                Asfaltado = lote.Asfaltado,
                NombreLote = lote.IdTipoLoteNavigation?.Nombre,
                propietarioLote = lote.IdPersonaNavigation?.Apellido
            };
        }
        //LoteCLS ILoteService.mostrarLoteById(int idLote)
        // {
        //     var lote = dbContext.Lotes.Where(lote => lote.IdLote == idLote && lote.Bhabilitado == 1)
        //     .FirstOrDefault();

        //         LoteCLS loteCLS = new LoteCLS
        //         {
        //         Calle = !String.IsNullOrEmpty(lote.Calle) ? lote.Calle : "No Posee",
        //             Altura = lote.Altura,
        //             SupEdificada = lote.SupEdificada,
        //             NomenclaturaCatastral = lote.NomenclaturaCatastral,
        //             EstadoDeuda = lote.EstadoDeuda,
        //             DniTitular = lote.IdPersonaNavigation.Dni,
        //             Esquina = lote.Esquina,
        //             Asfaltado = lote.Asfaltado,
        //             NombreLote = lote?.IdTipoLoteNavigation?.Nombre,
        //             propietarioLote = lote.IdPersonaNavigation?.Apellido
        //              };


        //     return loteCLS;
        // }



    }
}
