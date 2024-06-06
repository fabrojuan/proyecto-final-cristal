using System;
using MVPSA_V2022.clases;
using MVPSA_V2022.Modelos;

namespace MVPSA_V2022.Utils
{
	public class Conversor
	{
		public Conversor()
		{
		}

		public static Reclamo convertToReclamo(CrearReclamoRequestDto origen) {
			Reclamo destino = new Reclamo();
			destino.Descripcion = origen.descripcion;
			destino.CodTipoReclamo = origen.codTipoReclamo;
			destino.Calle = origen.calle;
			destino.Altura = origen.altura;
			destino.EntreCalles = origen.entreCalles;
			destino.NomApeVecino = origen.NomApeVecino;
			destino.MailVecino = origen.MailVecino;
			destino.TelefonoVecino = origen.TelefonoVecino;
			destino.NroArea = origen.nroArea;
			return destino;
        }

		public static ReclamoDto convertToReclamoDto(Reclamo origen) {
			ReclamoDto destino = new ReclamoDto();
			destino.nroReclamo = origen.NroReclamo;
			destino.descripcion = origen.Descripcion;
			destino.codTipoReclamo = (int)origen.CodTipoReclamo;
			destino.codEstadoReclamo = (int)origen.CodEstadoReclamo;
			destino.Bhabilitado = origen.Bhabilitado;
			destino.calle = origen.Calle;
			destino.altura = origen.Altura;
			destino.entreCalles = origen.EntreCalles;
			destino.idVecino = origen.IdVecino;
			destino.idUsuario = origen.IdUsuario;
			destino.Fecha = (DateTime)origen.Fecha;
			destino.nombreYapellido = origen.NomApeVecino;
			destino.mailVecino = origen.MailVecino;
			destino.telefonoVecino = origen.TelefonoVecino;
			destino.nroPrioridad = (int)origen.NroPrioridad;

			return destino;

		}
    }
}

