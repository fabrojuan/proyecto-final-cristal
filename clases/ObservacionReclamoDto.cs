using System;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.clases
{
	public class ObservacionReclamoDto
	{
		public ObservacionReclamoDto()
		{
		}

        public int id { get; set; }

        public int nroReclamo { get; set; }

        public string observacion { get; set; } = null!;

        public int codEstadoReclamo { get; set; }

        public string estadoReclamo { get; set; }

        public int idUsuarioAlta { get; set; }

        public string usuarioAlta { get; set; }

        public DateTime fechaAlta { get; set; }

        public string codAccion { get; set; } = null!;

    }
}

