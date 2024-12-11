﻿namespace MAS_CRMWebUI.Areas.AdminPanel.Models.Payment
{
    public class PaymentUpdateRequestDTO
    {
        public int TotalPrice { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }

        public Guid Guid { get; set; }
    }
}