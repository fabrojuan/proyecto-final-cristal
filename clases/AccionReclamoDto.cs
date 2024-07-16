using System;
namespace MVPSA_V2022.clases
{
	public class CambioEstadoReclamoDto
	{
		public CambioEstadoReclamoDto()
		{
		}

		public int nroReclamo { get; set; }
		public int idUsuarioCambioEstado { get; set; }
		public int codEstadoReclamoDestino { get; set; }
		public string observacionCambioEstado { get; set; }
	}
}

