namespace MVPSA_V2022.clases.Mobbex
{
    public class PagoData
    {
        public Boolean result { get; set; }
        public DataViewCLS view { get; set; }
        public DataPaymentCLS payment { get; set; }
        public DataEntityCLS entity { get; set; }
        public DataCustomerCLS customer { get; set; }
        public DataUserCLS user { get; set; }
        public DataSourceCLS source { get; set; }
        public DataCheckoutCLS checkout { get; set; }
    }
}
