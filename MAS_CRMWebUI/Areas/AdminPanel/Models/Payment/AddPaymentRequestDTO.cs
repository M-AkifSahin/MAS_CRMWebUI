namespace MAS_CRMWebUI.Areas.AdminPanel.Models.Payment
{
    public class AddPaymentRequestDTO
    {
        public int TotalPrice { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
    }
}
