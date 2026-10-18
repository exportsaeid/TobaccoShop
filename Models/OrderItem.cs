using System;

namespace TobaccoShop.Models
{
    public class OrderItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public string Unit { get; set; } = "عدد";
        public double Quantity { get; set; }
        /// <summary>قیمت فروش واحد</summary>
        public double UnitPrice { get; set; }
        /// <summary>قیمت خرید واحد در لحظه فروش (برای سود)</summary>
        public double CostPrice { get; set; }
        public bool IsManualPrice { get; set; }

        public double TotalPrice => Quantity * UnitPrice;
        public double TotalCost => Quantity * CostPrice;
        public double TotalProfit => TotalPrice - TotalCost;

        public string DisplayText =>
            $"{ProductName}  |  {Quantity:N3} {Unit}  ×  {UnitPrice:N0}  =  {TotalPrice:N0} تومان" +
            (IsManualPrice ? "  (قیمت دستی)" : "");
    }
}
