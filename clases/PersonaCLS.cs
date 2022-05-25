using System;

namespace MVPSA_V2022.clases
{
    public class PersonaCLS
    {
        public int Iidpersona { get; set; }
        // prop Iventada solo nombre completo y domicilio
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Mail { get; set; }
        public string Domicilio { get; set; }
        public string Dni { get; set; }
        public int BHabilitado { get; set; }
        public string BTieneuser { get; set; }
        public DateTime FechaNac { get; set; }
        //Prop de la base
        public string Nombre { get; set; }
        public string apellido { get; set; }
        public string altura { get; set; }
    }
}
