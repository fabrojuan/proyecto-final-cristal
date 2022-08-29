namespace MVPSA_V2022.clases.Mobbex
{
    public class DataPaymentCLS
    {
        public string id { get; set; }
        public string description { get; set; }
        public PaymentOperationCLS operation { get; set; }
        public PaymentStatusCLS status { get; set; }
        public decimal total { get; set; }
        public PaymentCurrencyCLS currency { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
        public string reference { get; set; }
        public PaymentSourceCLS source { get; set; }

    }
}
