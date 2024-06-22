using AutoMapper;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MimeKit.Cryptography;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Services
{
    public class TrabajoReclamoService : ITrabajoReclamoService
    {
        public readonly IMapper mapper;
        public readonly IUsuarioService usuarioService;
        private readonly M_VPSA_V3Context dbContext;

        public TrabajoReclamoService(IMapper mapper, IUsuarioService usuarioService,
            M_VPSA_V3Context dbContext) {
            this.mapper = mapper;
            this.usuarioService = usuarioService;
            this.dbContext = dbContext;
        }

        public void guardarTrabajo(TrabajoReclamoCreacionRequestDto trabajoDto, int idUsuarioAlta)
        {
            TrabajoReclamo trabajo = new TrabajoReclamo();
            trabajo.Descripcion = trabajoDto.descripcion;
            trabajo.FechaTrabajo = trabajoDto.fechaTrabajo;
            trabajo.NroReclamo = trabajoDto.nroReclamo;
            trabajo.FechaAlta = DateTime.Now;
            trabajo.IdUsuarioAlta = idUsuarioAlta;
            trabajo.Bhabilitado = 1;
            
            var reclamo = this.dbContext.Reclamos.Where(rec => rec.NroReclamo == trabajoDto.nroReclamo).FirstOrDefault();
            trabajo.NroAreaTrabajo = (int)reclamo.NroArea;
            
            this.dbContext.TrabajoReclamos.Add(trabajo);
            this.dbContext.SaveChanges();

        }

        public List<TrabajoReclamoDto> obtenerTrabajosReclamo(int nroReclamo)
        {
            List<TrabajoReclamoDto> result = new List<TrabajoReclamoDto>();
       
                this.dbContext.TrabajoReclamos.Where((reclamo) => reclamo.NroReclamo == nroReclamo)
                .OrderBy(tra => tra.FechaTrabajo)
                .ToList()
                .ForEach(trabajo => {
                    TrabajoReclamoDto dto = new TrabajoReclamoDto();
                    dto.areaTrabajo = trabajo.NroAreaTrabajoNavigation.Nombre;
                    dto.descripcion = trabajo.Descripcion;
                    dto.fechaAlta = trabajo.FechaAlta;
                    dto.fechaTrabajo = trabajo.FechaTrabajo;
                    dto.nroAreaTrabajo = trabajo.NroAreaTrabajo;
                    dto.nroReclamo = trabajo.NroReclamo;
                    dto.nroTrabajo = trabajo.NroTrabajo;
                    result.Add(dto);
                });

                return result;

        }
    }
}
