using System;
using System.Collections.Generic;

namespace TobaccoShop.Models
{
    public class Stocktake
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string PerformedBy { get; set; } = "";
        public List<StocktakeItem> Items { get; set; } = new List<StocktakeItem>();
        public string Notes { get; set; } = "";
    }

    public class StocktakeItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public string Unit { get; set; } = "عدد";
        public double SystemStock { get; set; }
        public double ActualStock { get; set; }
        public double Difference => ActualStock - SystemStock;
    }
}
