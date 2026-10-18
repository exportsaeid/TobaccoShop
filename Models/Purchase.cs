using System;
using System.Collections.Generic;
using System.Linq;

namespace TobaccoShop.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public string SupplierName { get; set; } = "";
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        // 👇 ویژگی‌های اضافه شده 👇
        public string InvoiceNumber { get; set; } = "";
        public bool IsConfirmed { get; set; } = true;

        public List<PurchaseItem> Items { get; set; } = new List<PurchaseItem>();
        public string Notes { get; set; } = "";

        public double TotalAmount => Items.Sum(i => i.TotalPrice);
    }

    public class PurchaseItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public string Unit { get; set; } = "عدد";
        public double Quantity { get; set; }

        // 👇 ویژگی‌های اضافه شده 👇
        public double UnitPurchasePrice { get; set; }
        public Product? Product { get; set; }

        // نگه داشتن UnitPrice برای سازگاری با کدهای قبلی
        public double UnitPrice
        {
            get => UnitPurchasePrice;
            set => UnitPurchasePrice = value;
        }

        public double TotalPrice => Quantity * UnitPurchasePrice;
    }
}