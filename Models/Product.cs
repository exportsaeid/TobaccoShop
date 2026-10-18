using System;

namespace TobaccoShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        /// <summary>کد بارکد (اختیاری)</summary>
        public string Barcode { get; set; } = "";
        /// <summary>قیمت فروش</summary>
        public double Price { get; set; }
        /// <summary>قیمت خرید / بهای تمام‌شده</summary>
        public double CostPrice { get; set; }
        public string Unit { get; set; } = "عدد";
        public double Stock { get; set; }
        public double LowStockThreshold { get; set; } = 1;

        public double ProfitPerUnit => Price - CostPrice;

        public string DisplayText =>
            string.IsNullOrEmpty(Barcode)
                ? $"{Name}  |  فروش: {Price:N0}  |  خرید: {CostPrice:N0}  |  {Unit}  |  موجودی: {Stock:N3}"
                : $"{Name}  |  بارکد: {Barcode}  |  فروش: {Price:N0}  |  {Unit}  |  موجودی: {Stock:N3}";
    }
}
