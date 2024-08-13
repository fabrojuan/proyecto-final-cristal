using System;
namespace MVPSA_V2022.clases
{
	public class AplicarAccionDto
	{
		public AplicarAccionDto()
		{
		}

		public int nroReclamo { get; set; }
		public string codAccion { get; set; }
		public int? codArea { get; set; }
		public string? observacion { get; set; }
		public string? idUsuario { get; set; }
	}
}

