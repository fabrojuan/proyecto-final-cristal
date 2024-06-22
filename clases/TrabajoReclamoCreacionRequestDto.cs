using MVPSA_V2022.Modelos;
using System;

namespace MVPSA_V2022.clases
{
    public class TrabajoReclamoCreacionRequestDto
    {

        public int nroReclamo { get; set; }
        public DateTime fechaTrabajo { get; set; }
        public String descripcion { get; set; }

    }
}
