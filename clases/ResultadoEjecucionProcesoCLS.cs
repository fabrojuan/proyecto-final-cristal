namespace MVPSA_V2022.clases
{
    public class ResultadoEjecucionProcesoCLS
    {
        public string resultado { get; set; }
        public string? mensaje { get; set; }


        public ResultadoEjecucionProcesoCLS() {}

        public ResultadoEjecucionProcesoCLS(string resultado)
        {
            this.resultado = resultado;
        }

        public ResultadoEjecucionProcesoCLS(string resultado, string? mensaje)
        {
            this.resultado = resultado;
            this.mensaje = mensaje;
        }
    }
}