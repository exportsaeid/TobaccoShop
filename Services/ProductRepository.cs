using System;
using System.Globalization;
using TobaccoShop.Models;

namespace TobaccoShop.Services
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(string filePath) : base(filePath) { }

        protected override int GetId(Product entity) => entity.Id;
        protected override void SetId(Product entity, int id) => entity.Id = id;

        protected override string Serialize(Product p)
        {
            // Id|Name|Price|Unit|Stock|LowStock|CostPrice|Barcode
            return $"{p.Id}|{Escape(p.Name)}|{p.Price.ToString(CultureInfo.InvariantCulture)}|{p.Unit}|{p.Stock.ToString(CultureInfo.InvariantCulture)}|{p.LowStockThreshold.ToString(CultureInfo.InvariantCulture)}|{p.CostPrice.ToString(CultureInfo.InvariantCulture)}|{Escape(p.Barcode)}";
        }

        protected override Product? Deserialize(string line)
        {
            try
            {
                var parts = line.Split('|');
                if (parts.Length < 6) return null;
                return new Product
                {
                    Id = int.Parse(parts[0]),
                    Name = Unescape(parts[1]),
                    Price = double.Parse(parts[2], CultureInfo.InvariantCulture),
                    Unit = parts[3],
                    Stock = double.Parse(parts[4], CultureInfo.InvariantCulture),
                    LowStockThreshold = double.Parse(parts[5], CultureInfo.InvariantCulture),
                    CostPrice = parts.Length >= 7 ? double.Parse(parts[6], CultureInfo.InvariantCulture) : 0,
                    Barcode = parts.Length >= 8 ? Unescape(parts[7]) : ""
                };
            }
            catch { return null; }
        }

        private string Escape(string s) => s?.Replace("|", "¦") ?? "";
        private string Unescape(string s) => s?.Replace("¦", "|") ?? "";
    }
}
