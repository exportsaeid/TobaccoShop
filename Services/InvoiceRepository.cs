using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TobaccoShop.Models;

namespace TobaccoShop.Services
{
    public class InvoiceRepository
    {
        private readonly string _filePath;
        private List<Invoice> _invoices = new List<Invoice>();
        private readonly object _lock = new object();

        public InvoiceRepository(string filePath)
        {
            _filePath = filePath;
            Load();
        }

        public List<Invoice> GetAll() => _invoices.ToList();

        public Invoice? GetById(int id) => _invoices.FirstOrDefault(i => i.Id == id);

        public void Add(Invoice invoice)
        {
            lock (_lock)
            {
                invoice.Id = _invoices.Count > 0 ? _invoices.Max(i => i.Id) + 1 : 1;
                _invoices.Add(invoice);
                Save();
            }
        }

        public void Update(Invoice invoice)
        {
            lock (_lock)
            {
                var idx = _invoices.FindIndex(i => i.Id == invoice.Id);
                if (idx >= 0)
                {
                    _invoices[idx] = invoice;
                    Save();
                }
            }
        }

        public void Delete(int id)
        {
            lock (_lock)
            {
                var inv = _invoices.FirstOrDefault(i => i.Id == id);
                if (inv != null)
                {
                    _invoices.Remove(inv);
                    Save();
                }
            }
        }

        public void Save()
        {
            try
            {
                var sb = new StringBuilder();
                foreach (var inv in _invoices)
                {
                    sb.AppendLine($"H|{inv.Id}|{Escape(inv.CustomerName)}|{inv.OrderDate:o}|{inv.IsSettled}|{Escape(inv.PayMethod)}");
                    foreach (var item in inv.Items)
                    {
                        // I|ProductId|Name|Unit|Qty|UnitPrice|IsManual|CostPrice
                        sb.AppendLine($"I|{item.ProductId}|{Escape(item.ProductName)}|{item.Unit}|{item.Quantity.ToString(CultureInfo.InvariantCulture)}|{item.UnitPrice.ToString(CultureInfo.InvariantCulture)}|{item.IsManualPrice}|{item.CostPrice.ToString(CultureInfo.InvariantCulture)}");
                    }
                    foreach (var pay in inv.Payments)
                    {
                        sb.AppendLine($"P|{Escape(pay.Method)}|{pay.Amount.ToString(CultureInfo.InvariantCulture)}|{pay.PaymentDate:o}");
                    }
                    sb.AppendLine("---");
                }
                File.WriteAllText(_filePath, sb.ToString());
            }
            catch { }
        }

        public void Load()
        {
            _invoices.Clear();
            if (!File.Exists(_filePath)) return;

            try
            {
                Invoice? current = null;
                foreach (var line in File.ReadAllLines(_filePath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (line == "---")
                    {
                        if (current != null) _invoices.Add(current);
                        current = null;
                        continue;
                    }

                    var parts = line.Split('|');
                    if (parts[0] == "H" && parts.Length >= 6)
                    {
                        current = new Invoice
                        {
                            Id = int.Parse(parts[1]),
                            CustomerName = Unescape(parts[2]),
                            OrderDate = DateTime.Parse(parts[3]),
                            IsSettled = bool.Parse(parts[4]),
                            PayMethod = Unescape(parts[5])
                        };
                    }
                    else if (parts[0] == "I" && current != null && parts.Length >= 7)
                    {
                        current.Items.Add(new OrderItem
                        {
                            ProductId = int.Parse(parts[1]),
                            ProductName = Unescape(parts[2]),
                            Unit = parts[3],
                            Quantity = double.Parse(parts[4], CultureInfo.InvariantCulture),
                            UnitPrice = double.Parse(parts[5], CultureInfo.InvariantCulture),
                            IsManualPrice = bool.Parse(parts[6]),
                            CostPrice = parts.Length >= 8
                                ? double.Parse(parts[7], CultureInfo.InvariantCulture)
                                : 0
                        });
                    }
                    else if (parts[0] == "P" && current != null && parts.Length >= 4)
                    {
                        current.Payments.Add(new Payment
                        {
                            Method = Unescape(parts[1]),
                            Amount = double.Parse(parts[2], CultureInfo.InvariantCulture),
                            PaymentDate = DateTime.Parse(parts[3])
                        });
                    }
                }
                if (current != null) _invoices.Add(current);
            }
            catch { }
        }

        private string Escape(string s) => s?.Replace("|", "¦") ?? "";
        private string Unescape(string s) => s?.Replace("¦", "|") ?? "";
    }
}
