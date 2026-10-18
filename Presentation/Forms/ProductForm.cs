using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TobaccoShop.Models;
using TobaccoShop.Services;

namespace TobaccoShop
{
    public partial class ProductForm : Form
    {
        private Product? _selected;

        public ProductForm()
        {
            InitializeComponent();


            // فقط این دو خط را اضافه کنید
            txtPrice.TextChanged += (s, e) => NumberHelper.FormatTextBoxNumber(txtPrice);
            txtCostPrice.TextChanged += (s, e) => NumberHelper.FormatTextBoxNumber(txtCostPrice);


            // 👇 این ۴ خط را اضافه کنید 👇
            // چسباندن فیلدها و دکمه‌ها به پایین فرم
            foreach (Control c in this.Controls)
            {
                if (c != dgv) // به جز گرید، بقیه به پایین بچسبند
                    c.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            }
            // 👆 تا اینجا 👆
            LoadData();
        }

        private void LoadData()
        {
            dgv.Rows.Clear();
            foreach (var p in Shop.Instance.GetProducts().OrderBy(x => x.Name))
            {
                dgv.Rows.Add(
                    p.Id,
                    p.Name,
                    p.Barcode ?? "",
                    FormatNumber(p.CostPrice),
                    FormatNumber(p.Price),
                    p.Unit,
                    FormatNumber(p.Stock, 3),
                    FormatNumber(p.LowStockThreshold, 3));
            }
            _selected = null;
            ClearFields();
        }


        private void ClearFields()
        {
            txtName.Clear();
            txtBarcode.Clear();
            txtCostPrice.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            txtThreshold.Text = "1";
            if (cmbUnit.Items.Count > 0)
                cmbUnit.SelectedIndex = 0;
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null || dgv.CurrentRow.Index < 0) return;
            if (dgv.CurrentRow.Cells[0].Value == null) return;

            int id = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            _selected = Shop.Instance.GetProductById(id);
            if (_selected == null) return;

            txtName.Text = _selected.Name;
            txtBarcode.Text = _selected.Barcode ?? "";
            txtCostPrice.Text = FormatNumber(_selected.CostPrice);
            txtPrice.Text = FormatNumber(_selected.Price);
            txtStock.Text = FormatNumber(_selected.Stock, 3);
            txtThreshold.Text = FormatNumber(_selected.LowStockThreshold, 3);

            if (_selected.Unit == "کیلوگرم")
                cmbUnit.SelectedItem = "کیلوگرم";
            else
                cmbUnit.SelectedItem = "عدد";
        }

        private static string NormalizeNumber(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "";
            var sb = new StringBuilder();
            foreach (char c in input.Trim())
            {
                if (c >= '\u06F0' && c <= '\u06F9') { sb.Append((char)('0' + (c - '\u06F0'))); continue; }
                if (c >= '\u0660' && c <= '\u0669') { sb.Append((char)('0' + (c - '\u0660'))); continue; }
                if (c == ',' || c == '٬' || c == '٫' || c == ' ' || c == '\u00A0' || c == '/') continue;
                if (c == '.' ) { sb.Append('.'); continue; }
                if (char.IsDigit(c) || c == '-' || c == '.') sb.Append(c);
            }
            return sb.ToString();
        }

        private static bool TryParseNumber(string? text, out double value)
        {
            value = 0;
            string n = NormalizeNumber(text);
            if (string.IsNullOrEmpty(n)) return true;
            return double.TryParse(n, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
        }

        private static string FormatNumber(double v, int decimals = 0)
        {
            if (decimals <= 0)
                return ((long)Math.Round(v)).ToString(CultureInfo.InvariantCulture);
            return v.ToString("0.###", CultureInfo.InvariantCulture);
        }

        private bool TryReadFields(out string name, out string barcode, out double cost, out double price, out double stock, out double th, out string unit)
        {
            name = txtName.Text.Trim();
            barcode = txtBarcode.Text.Trim();
            cost = 0; price = 0; stock = 0; th = 1;
            unit = cmbUnit.SelectedItem?.ToString() ?? "عدد";

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("نام محصول الزامی است.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (!TryParseNumber(txtCostPrice.Text, out cost) || cost < 0)
            {
                MessageBox.Show("قیمت خرید معتبر نیست.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCostPrice.Focus();
                return false;
            }

            if (!TryParseNumber(txtPrice.Text, out price) || price < 0)
            {
                MessageBox.Show("قیمت فروش معتبر نیست.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            if (!TryParseNumber(txtStock.Text, out stock) || stock < 0) stock = 0;
            if (!TryParseNumber(txtThreshold.Text, out th) || th <= 0) th = 1;
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!TryReadFields(out var name, out var barcode, out var cost, out var price, out var stock, out var th, out var unit))
                return;

            // جلوگیری از بارکد تکراری
            if (!string.IsNullOrEmpty(barcode))
            {
                var exists = Shop.Instance.GetProductByBarcode(barcode);
                if (exists != null)
                {
                    MessageBox.Show($"این بارکد قبلاً برای «{exists.Name}» ثبت شده است.", "بارکد تکراری",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            Shop.Instance.AddProduct(new Product
            {
                Name = name,
                Barcode = barcode,
                CostPrice = cost,
                Price = price,
                Unit = unit,
                Stock = stock,
                LowStockThreshold = th
            });
            LoadData();
            MessageBox.Show($"محصول «{name}» ثبت شد.", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selected == null)
            {
                MessageBox.Show("ابتدا یک محصول از جدول انتخاب کنید.", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!TryReadFields(out var name, out var barcode, out var cost, out var price, out var stock, out var th, out var unit))
                return;

            if (!string.IsNullOrEmpty(barcode))
            {
                var exists = Shop.Instance.GetProductByBarcode(barcode);
                if (exists != null && exists.Id != _selected.Id)
                {
                    MessageBox.Show($"این بارکد قبلاً برای «{exists.Name}» ثبت شده است.", "بارکد تکراری",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            _selected.Name = name;
            _selected.Barcode = barcode;
            _selected.CostPrice = cost;
            _selected.Price = price;
            _selected.Unit = unit;
            _selected.Stock = stock;
            _selected.LowStockThreshold = th;

            Shop.Instance.UpdateProduct(_selected);
            int keepId = _selected.Id;
            LoadData();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value != null && Convert.ToInt32(row.Cells[0].Value) == keepId)
                {
                    row.Selected = true;
                    dgv.CurrentCell = row.Cells[0];
                    break;
                }
            }

            MessageBox.Show("محصول ویرایش شد.", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selected == null) return;
            if (MessageBox.Show($"حذف «{_selected.Name}»؟", "تأیید", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Shop.Instance.DeleteProduct(_selected.Id);
                LoadData();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();
    }
}
