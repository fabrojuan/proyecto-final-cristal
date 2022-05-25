﻿using System;

namespace MVPSA_V2022.clases
{
    public class VecinoCLS
    {
        public int IdVecino { get; set; }
        public string NombreUser { get; set; }
        public string Contrasenia { get; set; }
        public int IdPersona { get; set; }
        public string NombrePersona { get; set; }
        public int BHabilitado { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaBaja { get; set; }
        public string Telefono { get; set; }
        public string Mail { get; set; }
        public string Domicilio { get; set; }
        public string Dni { get; set; }
        public DateTime FechaNac { get; set; }
        public string Apellido { get; set; }
        public string Altura { get; set; }
    }
}
