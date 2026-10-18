using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TobaccoShop.Models;
using TobaccoShop.Services;

namespace TobaccoShop
{
    public partial class PurchaseForm : Form
    {
        private readonly List<PurchaseItem> _currentPurchaseItems = new List<PurchaseItem>();
        private List<Product> _allProducts = new List<Product>();

        public PurchaseForm()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            _allProducts = Shop.Instance.GetProducts().OrderBy(p => p.Name).ToList();
            FilterProducts();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterProducts();
        }

        private void FilterProducts()
        {
            string q = txtSearch.Text.Trim();
            lstProducts.Items.Clear();
            foreach (var p in _allProducts.Where(p => p.Name.Contains(q, StringComparison.OrdinalIgnoreCase)))
            {
                lstProducts.Items.Add(new ProductItem { Text = p.Name, Product = p });
            }
            lstProducts.DisplayMember = "Text";
        }

        private void LstProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstProducts.SelectedItem is ProductItem item)
            {
                lblUnitInfo.Text = $"واحد: {item.Product.Unit} | موجودی فعلی: {item.Product.Stock:N3}";
                numBuyPrice.Value = (decimal)item.Product.CostPrice;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (lstProducts.SelectedItem is not ProductItem item)
            {
                MessageBox.Show("لطفاً یک محصول را انتخاب کنید.", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var product = item.Product;
            double qty = (double)numQty.Value;
            double buyPrice = (double)numBuyPrice.Value;

            if (qty <= 0)
            {
                MessageBox.Show("تعداد باید بزرگ‌تر از صفر باشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existing = _currentPurchaseItems.FirstOrDefault(x => x.ProductId == product.Id);
            if (existing != null)
            {
                existing.Quantity += qty;
                existing.UnitPurchasePrice = buyPrice;
            }
            else
            {
                _currentPurchaseItems.Add(new PurchaseItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Unit = product.Unit,
                    Quantity = qty,
                    UnitPurchasePrice = buyPrice
                });
            }

            RefreshGrid();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dgvPurchaseItems.CurrentRow == null || dgvPurchaseItems.CurrentRow.Index < 0) return;
            _currentPurchaseItems.RemoveAt(dgvPurchaseItems.CurrentRow.Index);
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dgvPurchaseItems.Rows.Clear();
            foreach (var item in _currentPurchaseItems)
            {
                string qtyFormat = item.Unit == "کیلوگرم" ? "N3" : "N0";
                dgvPurchaseItems.Rows.Add(
                    item.ProductName,
                    item.Quantity.ToString(qtyFormat),
                    item.Unit,
                    item.UnitPurchasePrice.ToString("N0"),
                    item.TotalPrice.ToString("N0"));
            }

            double total = _currentPurchaseItems.Sum(x => x.TotalPrice);
            lblTotal.Text = $"جمع کل خرید: {total:N0} تومان";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (_currentPurchaseItems.Count == 0)
            {
                MessageBox.Show("لیست خرید خالی است.", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string supplier = string.IsNullOrWhiteSpace(txtSupplier.Text) ? "تامین‌کننده ناشناس" : txtSupplier.Text.Trim();
            string invoiceNumber = txtInvoiceNumber.Text.Trim();
            double total = _currentPurchaseItems.Sum(x => x.TotalPrice);

            if (MessageBox.Show($"آیا از ثبت فاکتور خرید به مبلغ {total:N0} تومان مطمئن هستید؟\n\nپس از ثبت، موجودی کالاها به انبار اضافه می‌شود.", "تایید نهایی", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var purchase = new Purchase
            {
                SupplierName = supplier,
                InvoiceNumber = invoiceNumber,
                PurchaseDate = DateTime.Now,
                Notes = "",
                IsConfirmed = true,
                Items = _currentPurchaseItems.Select(item => new PurchaseItem
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Unit = item.Unit,
                    Quantity = item.Quantity,
                    UnitPurchasePrice = item.UnitPurchasePrice
                }).ToList()
            };

            Shop.Instance.AddPurchase(purchase);

            MessageBox.Show("فاکتور خرید با موفقیت ثبت شد و موجودی انبار افزایش یافت.", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _currentPurchaseItems.Clear();
            txtSupplier.Clear();
            txtInvoiceNumber.Clear();
            RefreshGrid();
        }

        private class ProductItem
        {
            public string Text { get; set; } = "";
            public Product Product { get; set; } = null!;
            public override string ToString() => Text;
        }
    }
}