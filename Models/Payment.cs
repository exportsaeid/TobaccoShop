using System;

namespace TobaccoShop.Models
{
    public class Payment
    {
        public string Method { get; set; } = "نقدی"; // نقدی، کارتخوان، انتقال
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
    }
}
