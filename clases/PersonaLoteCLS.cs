using System;

namespace MVPSA_V2022.clases
{
    public class PersonaLoteCLS
    {
        public string? Error { get; internal set; }
        public string? Telefono { get; set; }

        public int Iidpersona { get; set; }

        public string? Mail { get; set; }
        public string? Domicilio { get; set; }
        public string? Dni { get; set; }
        public int Bhabilitado { get; set; }
        public string? FechaNac { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Altura { get; set; }
    }
}
