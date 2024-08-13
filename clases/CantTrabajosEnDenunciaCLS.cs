using MVPSA_V2022.Modelos;
using System;
using System.Globalization;

namespace MVPSA_V2022.clases
{
    public class CantTrabajosEnDenunciaCLS
    {
        private DateTime _fecha;

        public DateTime Fecha
        {
            get { return _fecha; }
            set
            {
                // Formatear la fecha en el formato "dd/MM/yy"
                string fechaFormateada = value.ToString("dd/MM/yy");

                // Parsear la fecha formateada de nuevo a un objeto DateTime
                _fecha = DateTime.ParseExact(fechaFormateada, "dd/MM/yy", CultureInfo.InvariantCulture);
            }
        }

        public int cantidadPorFecha { get; set; }

    }
}









