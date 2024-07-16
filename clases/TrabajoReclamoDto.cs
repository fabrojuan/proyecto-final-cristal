using MVPSA_V2022.Modelos;
using System;

namespace MVPSA_V2022.clases
{
    public class TrabajoReclamoDto
    {

        public int nroReclamo { get; set; }
        public int nroTrabajo { get; set; }

        public int nroAreaTrabajo { get; set; }
        public String areaTrabajo { get; set; }

        public DateTime fechaTrabajo { get; set; }

        public string descripcion { get; set; } = null!;

        public DateTime fechaAlta { get; set; }

    }
}
