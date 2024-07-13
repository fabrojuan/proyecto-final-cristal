using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;
using MVPSA_V2022.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVPSA_V2022.Controllers
{
    [ApiController]
    [Route("api/areas")]
    [AllowAnonymous]
    public class AreaController : Controller
    {
        private readonly M_VPSA_V3Context dbContext;

        public AreaController(M_VPSA_V3Context dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult consultarAreas() {
            List<AreaDto> areas = new List<AreaDto>();
            dbContext.Areas.OrderBy(area => area.Nombre).ToList()
                .ForEach(area => {
                    AreaDto areaDto = new AreaDto();
                    areaDto.NroArea = area.NroArea;
                    areaDto.Nombre = area.Nombre;
                    areas.Add(areaDto);
                });
            return Ok(areas);
        }

    }
}

