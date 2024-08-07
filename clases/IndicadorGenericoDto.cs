using MVPSA_V2022.Enums;

namespace MVPSA_V2022.clases
{
    public class IndicadorGenericoDto {
        
        public String titulo { get; set; }
        public String valorActual { get; set; }
        public String valorAnterior { get; set; }
        public String periodo { get; set; }
        public String valorVariacion { get; set; }
        public String tipoVariacion { get; set; }

    }
}