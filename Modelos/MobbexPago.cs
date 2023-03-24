using System;
using System.Collections.Generic;

namespace MVPSA_V2022.Modelos
{
    public partial class MobbexPago
    {
        public int IdMobbexPago { get; set; }
        public string Type { get; set; } = null!;
        public string? ViewType { get; set; }
        public string PaymentId { get; set; } = null!;
        public string PaymentStatusCode { get; set; } = null!;
        public string PaymentStatusText { get; set; } = null!;
        public string? PaymentStatusMessage { get; set; }
        public decimal PaymentTotal { get; set; }
        public string PaymentCurrencyCode { get; set; } = null!;
        public string PaymentCurrencySymbol { get; set; } = null!;
        public string PaymentCreated { get; set; } = null!;
        public string? PaymentUpdated { get; set; }
        public string PaymentReference { get; set; } = null!;
        public string? CustomerUid { get; set; }
        public string? CustomerEmail { get; set; }
        public string CheckoutUid { get; set; } = null!;
        public DateTime? FechaAlta { get; set; }
    }
}
