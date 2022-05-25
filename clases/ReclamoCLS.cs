using System;

namespace MVPSA_V2022.clases
{
    public class ReclamoCLS
    {
        public int nroReclamo { get; set; }
        public string descripcion { get; set; }
        public int codTipoReclamo { get; set; }
        public int codEstadoReclamo { get; set; }
        public int? Bhabilitado { get; set; }
        public string calle { get; set; }
        public string altura { get; set; }
        public string entreCalles { get; set; }

        public int idVecino { get; set; }
        public int idUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public string estadoReclamo { get; set; }
        public string tipoReclamo { get; set; }
        public string nombreYapellido { get; set; }



    }
}
