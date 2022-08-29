namespace MVPSA_V2022.clases.Mobbex
{
    public class PaymentSourceCLS
    {
        public string name { get; set; }
        public string type { get; set; }
        public string reference { get; set; }
        public string number { get; set; }
        public SourceExpirationCLS expiration { get; set; }
        public SourceInstallmentCLS installment { get; set; }

    }
}
