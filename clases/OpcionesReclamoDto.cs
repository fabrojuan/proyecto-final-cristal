using System;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.clases
{
	public class OpcionesReclamoDto
	{
		public OpcionesReclamoDto()
		{
		}

        public int nroReclamo { get; set; }

        public Boolean puedeRechazar { get; set; }
        public Boolean puedeFinalizar { get; set; }
        public Boolean puedeAsignar { get; set; }
        public Boolean puedeCargarTrabajo { get; set; }
        public Boolean puedeVerObservaciones { get; set; }
        public Boolean puedeVerTrabajos { get; set; }
    }
}

