using System;

namespace MVPSA_V2022.clases
{
    public class TrabajoCLS
    {
        public int Nro_Trabajo { get; set; }
        public string Descripcion { get; set; }
        public int Nro_Denuncia { get; set; }
        public int Id_Usuario { get; set; }
        public int BHabilitado { get; set; }
        public DateTime Fecha { get; set; }
        //Para TRabajo Reclamo
        public int Nro_Reclamo { get; set; }
        public int Id_Vecino { get; set; }
        public string Foto { get; set; }
        public string Foto2 { get; set; }
        public string Foto3 { get; set; }
        public PruebaImagenCLS pruebaP { get; set; }
        //Borrar los prueba graffica
        public string PruebaGrafica { get; set; }
        public string PruebaGrafica2 { get; set; }
        public string PruebaGrafica3 { get; set; }


    }
}
