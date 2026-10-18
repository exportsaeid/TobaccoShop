using System;

namespace TobaccoShop.Models
{
    public class MiscellaneousExpense
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public double Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Category { get; set; } = "عمومی";
        public string Notes { get; set; } = "";
        public string RecordedBy { get; set; } = "";
    }
}
