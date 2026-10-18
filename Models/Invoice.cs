using System;
using System.Collections.Generic;
using System.Linq;

namespace TobaccoShop.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = "";
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public bool IsSettled { get; set; } = true;
        public string PayMethod { get; set; } = "نقدی";

        public List<Payment> Payments { get; set; } = new List<Payment>();
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public double TotalAmount => Items.Sum(i => i.TotalPrice);

        public double PaidAmount => Payments.Sum(p => p.Amount);

        public string DisplayText =>
            $"فاکتور #{Id}  |  {CustomerName}  |  {OrderDate:yyyy/MM/dd HH:mm}  |  {TotalAmount:N0} تومان";
    }
}
