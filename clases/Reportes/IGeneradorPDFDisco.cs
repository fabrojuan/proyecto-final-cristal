namespace MVPSA_V2022.clases.Reportes
{
    public interface IGeneradorPDFDisco
    {
        string CreateSamplePDf();
        string CreateSamplePDf_Parametros(int DatasetElegido);
    }
}