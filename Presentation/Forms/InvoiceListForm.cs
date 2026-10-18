using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TobaccoShop.Models;
using TobaccoShop.Services;

namespace TobaccoShop
{
    public partial class InvoiceListForm : Form
    {
        public InvoiceListForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgv.Rows.Clear();
            txtDetails.Clear();
            foreach (var inv in Shop.Instance.GetInvoices().OrderByDescending(i => i.Id))
                dgv.Rows.Add(
                    inv.Id,
                    inv.CustomerName,
                    PersianDateHelper.ToPersianDateTime(inv.OrderDate),
                    inv.TotalAmount.ToString("N0"),
                    inv.PayMethod,
                    inv.IsSettled ? "تسویه" : "بدهکار");
        }

        private Invoice? GetSelectedInvoice()
        {
            if (dgv.CurrentRow == null || dgv.CurrentRow.Index < 0) return null;
            if (dgv.CurrentRow.Cells[0].Value == null) return null;
            int id = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            return Shop.Instance.GetInvoiceById(id);
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            var inv = GetSelectedInvoice();
            if (inv == null)
            {
                txtDetails.Clear();
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine($"فاکتور شماره {inv.Id}");
            sb.AppendLine($"مشتری: {inv.CustomerName}");
            sb.AppendLine($"تاریخ: {PersianDateHelper.ToPersianDateTime(inv.OrderDate)}");
            sb.AppendLine($"روش پرداخت: {inv.PayMethod}");
            sb.AppendLine($"وضعیت: {(inv.IsSettled ? "تسویه شده" : "بدهکار")}");
            sb.AppendLine("----------------------------------------");
            foreach (var item in inv.Items)
            {
                sb.AppendLine(
                    $"{item.ProductName}  |  {item.Quantity:N3} {item.Unit}  ×  {item.UnitPrice:N0}  =  {item.TotalPrice:N0}" +
                    (item.IsManualPrice ? "  (قیمت دستی)" : ""));
            }
            sb.AppendLine("----------------------------------------");
            sb.AppendLine($"جمع کل: {inv.TotalAmount:N0} تومان");
            if (inv.Payments.Count > 0)
            {
                sb.AppendLine("پرداخت‌ها:");
                foreach (var p in inv.Payments)
                    sb.AppendLine($"  • {p.Method}: {p.Amount:N0} تومان  ({PersianDateHelper.ToPersianDate(p.PaymentDate)})");
            }
            txtDetails.Text = sb.ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var inv = GetSelectedInvoice();
            if (inv == null)
            {
                MessageBox.Show("لطفاً یک فاکتور را انتخاب کنید.", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            PrintHelper.PrintInvoice(inv);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var inv = GetSelectedInvoice();
            if (inv == null)
            {
                MessageBox.Show("لطفاً یک فاکتور را انتخاب کنید.", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"آیا از حذف فاکتور #{inv.Id} مربوط به «{inv.CustomerName}» مطمئن هستید؟\n\n" +
                $"مبلغ: {inv.TotalAmount:N0} تومان\n\n" +
                "با حذف فاکتور، موجودی کالاها به انبار برمی‌گردد.",
                "تأیید حذف فاکتور",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes) return;

            Shop.Instance.DeleteInvoice(inv.Id);
            MessageBox.Show($"فاکتور #{inv.Id} حذف شد و موجودی کالاها بازگردانده شد.", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var inv = GetSelectedInvoice();
            if (inv == null)
            {
                MessageBox.Show("لطفاً یک فاکتور را انتخاب کنید.", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new InvoiceEditDialog(inv))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    Shop.Instance.UpdateInvoice(dlg.EditedInvoice);
                    MessageBox.Show($"فاکتور #{inv.Id} با موفقیت ویرایش شد.", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();

                    // انتخاب مجدد همان فاکتور
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.Cells[0].Value != null && Convert.ToInt32(row.Cells[0].Value) == inv.Id)
                        {
                            row.Selected = true;
                            dgv.CurrentCell = row.Cells[0];
                            break;
                        }
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();
    }

    /// <summary>
    /// دیالوگ ویرایش فاکتور: نام مشتری، روش پرداخت، وضعیت تسویه، و آیتم‌ها (مقدار و قیمت)
    /// </summary>
    internal class InvoiceEditDialog : Form
    {
        private readonly Invoice _original;
        public Invoice EditedInvoice { get; private set; } = null!;

        private TextBox txtCustomer = null!;
        private ComboBox cmbPayMethod = null!;
        private CheckBox chkSettled = null!;
        private DataGridView dgvItems = null!;
        private Label lblTotal = null!;
        private Button btnOk = null!;
        private Button btnCancel = null!;
        private Button btnRemoveItem = null!;
        private Button btnClearAll = null!;

        // 👇 کنترل‌های جدید برای افزودن محصول 👇
        private ComboBox cmbNewProduct = null!;
        private NumericUpDown numNewQty = null!;
        private Button btnAddNewItem = null!;

        private List<OrderItem> _items;

        public InvoiceEditDialog(Invoice invoice)
        {
            _original = invoice;
            _items = invoice.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Unit = i.Unit,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                CostPrice = i.CostPrice,
                IsManualPrice = i.IsManualPrice
            }).ToList();

            BuildUi();
            LoadFields();
            LoadProductsToCombo();
            RefreshItemsGrid();
        }

        private void BuildUi()
        {
            this.Text = $"ویرایش فاکتور #{_original.Id}";
            this.Size = new Size(760, 650);
            this.MinimumSize = new Size(680, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Tahoma", 10F);
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;

            var lblCust = new Label { Text = "نام مشتری:", Location = new Point(20, 20), AutoSize = true };
            txtCustomer = new TextBox { Location = new Point(120, 17), Width = 280 };

            var lblPay = new Label { Text = "روش پرداخت:", Location = new Point(20, 55), AutoSize = true };
            cmbPayMethod = new ComboBox
            {
                Location = new Point(120, 52),
                Width = 180,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPayMethod.Items.AddRange(new object[] { "نقدی", "کارتخوان", "انتقال", "نسیه", "ترکیبی" });

            chkSettled = new CheckBox
            {
                Text = "تسویه شده",
                Location = new Point(320, 54),
                AutoSize = true
            };

            var lblItems = new Label
            {
                Text = "اقلام فاکتور (می‌توانید مقدار و قیمت را ویرایش کنید):",
                Location = new Point(20, 95),
                AutoSize = true
            };

            dgvItems = new DataGridView
            {
                Location = new Point(20, 120),
                Size = new Size(700, 220),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "محصول", ReadOnly = true, FillWeight = 30 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colQty", HeaderText = "مقدار", FillWeight = 15 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colUnit", HeaderText = "واحد", ReadOnly = true, FillWeight = 12 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPrice", HeaderText = "قیمت واحد", FillWeight = 18 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotal", HeaderText = "جمع", ReadOnly = true, FillWeight = 18 });
            dgvItems.CellEndEdit += DgvItems_CellEndEdit;

            // دکمه حذف ردیف (بدون محدودیت)
            btnRemoveItem = new Button
            {
                Text = "حذف ردیف انتخاب‌شده",
                Location = new Point(20, 350),
                Size = new Size(150, 32),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            btnRemoveItem.Click += (s, e) =>
            {
                if (dgvItems.CurrentRow == null || dgvItems.CurrentRow.Index < 0) return;
                int idx = dgvItems.CurrentRow.Index;
                if (idx >= 0 && idx < _items.Count)
                {
                    _items.RemoveAt(idx);
                    RefreshItemsGrid();
                }
            };

            // دکمه حذف همه اقلام
            btnClearAll = new Button
            {
                Text = "حذف همه اقلام",
                Location = new Point(180, 350),
                Size = new Size(120, 32),
                BackColor = Color.MistyRose,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            btnClearAll.Click += (s, e) =>
            {
                if (_items.Count == 0) return;
                if (MessageBox.Show("آیا از حذف تمام اقلام این فاکتور مطمئن هستید؟", "تأیید", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    _items.Clear();
                    RefreshItemsGrid();
                }
            };

            // 👇 بخش اضافه کردن محصول جدید 👇
            var grpAdd = new GroupBox
            {
                Text = "افزودن محصول جدید به فاکتور",
                Location = new Point(20, 390),
                Size = new Size(700, 80),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            var lblNewProd = new Label { Text = "انتخاب محصول:", Location = new Point(540, 32), Size = new Size(100, 25), TextAlign = ContentAlignment.MiddleRight };
            cmbNewProduct = new ComboBox
            {
                Location = new Point(310, 30),
                Width = 220,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DisplayMember = "DisplayText"
            };

            var lblNewQty = new Label { Text = "تعداد:", Location = new Point(240, 32), Size = new Size(50, 25), TextAlign = ContentAlignment.MiddleRight };
            numNewQty = new NumericUpDown
            {
                Location = new Point(160, 30),
                Width = 70,
                Minimum = 1,
                Maximum = 1000,
                Value = 1
            };

            btnAddNewItem = new Button
            {
                Text = "➕ افزودن به فاکتور",
                Location = new Point(20, 25),
                Size = new Size(130, 36),
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat
            };
            btnAddNewItem.Click += BtnAddNewItem_Click;

            grpAdd.Controls.AddRange(new Control[] { lblNewProd, cmbNewProduct, lblNewQty, numNewQty, btnAddNewItem });
            // 👆 تا اینجا 👆

            lblTotal = new Label
            {
                Text = "جمع کل: 0 تومان",
                Location = new Point(20, 485),
                AutoSize = true,
                Font = new Font("Tahoma", 11F, FontStyle.Bold),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            btnOk = new Button
            {
                Text = "ذخیره تغییرات",
                Size = new Size(130, 36),
                Location = new Point(480, 560),
                DialogResult = DialogResult.None,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnOk.Click += BtnOk_Click;

            btnCancel = new Button
            {
                Text = "انصراف",
                Size = new Size(100, 36),
                Location = new Point(620, 560),
                DialogResult = DialogResult.Cancel,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            this.Controls.AddRange(new Control[]
            {
                lblCust, txtCustomer, lblPay, cmbPayMethod, chkSettled,
                lblItems, dgvItems, btnRemoveItem, btnClearAll, grpAdd, lblTotal, btnOk, btnCancel
            });
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private void LoadProductsToCombo()
        {
            var products = Shop.Instance.GetProducts().OrderBy(p => p.Name).ToList();
            cmbNewProduct.Items.Clear();
            foreach (var p in products)
            {
                cmbNewProduct.Items.Add(new ProductItem { Text = p.DisplayText, Product = p });
            }
            if (cmbNewProduct.Items.Count > 0)
                cmbNewProduct.SelectedIndex = 0;
        }

        private void BtnAddNewItem_Click(object? sender, EventArgs e)
        {
            if (cmbNewProduct.SelectedItem is not ProductItem item)
            {
                MessageBox.Show("لطفاً یک محصول را انتخاب کنید.", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var product = item.Product;
            double qty = (double)numNewQty.Value;

            var existing = _items.FirstOrDefault(x => x.ProductId == product.Id && !x.IsManualPrice);
            if (existing != null)
            {
                existing.Quantity += qty;
            }
            else
            {
                _items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Unit = product.Unit,
                    Quantity = qty,
                    UnitPrice = product.Price,
                    CostPrice = product.CostPrice,
                    IsManualPrice = false
                });
            }

            RefreshItemsGrid();
            numNewQty.Value = 1;
        }

        private void LoadFields()
        {
            txtCustomer.Text = _original.CustomerName;
            if (cmbPayMethod.Items.Contains(_original.PayMethod))
                cmbPayMethod.SelectedItem = _original.PayMethod;
            else
            {
                cmbPayMethod.Items.Add(_original.PayMethod);
                cmbPayMethod.SelectedItem = _original.PayMethod;
            }
            chkSettled.Checked = _original.IsSettled;
        }

        private void RefreshItemsGrid()
        {
            dgvItems.Rows.Clear();
            foreach (var item in _items)
            {
                dgvItems.Rows.Add(
                    item.ProductName,
                    item.Quantity.ToString("N3"),
                    item.Unit,
                    item.UnitPrice.ToString("N0"),
                    item.TotalPrice.ToString("N0"));
            }
            UpdateTotalLabel();
        }

        private void UpdateTotalLabel()
        {
            double total = _items.Sum(x => x.TotalPrice);
            lblTotal.Text = $"جمع کل: {total:N0} تومان";
        }

        private void DgvItems_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _items.Count) return;
            var item = _items[e.RowIndex];
            var cell = dgvItems.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string val = cell.Value?.ToString()?.Replace(",", "") ?? "";

            if (e.ColumnIndex == 1) // مقدار
            {
                if (double.TryParse(val, out double qty) && qty > 0)
                    item.Quantity = qty;
                else
                {
                    MessageBox.Show("مقدار باید عدد بزرگ‌تر از صفر باشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (e.ColumnIndex == 3) // قیمت
            {
                if (double.TryParse(val, out double price) && price >= 0)
                {
                    item.UnitPrice = price;
                    item.IsManualPrice = true;
                }
                else
                {
                    MessageBox.Show("قیمت معتبر نیست.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            dgvItems.Rows[e.RowIndex].Cells[1].Value = item.Quantity.ToString("N3");
            dgvItems.Rows[e.RowIndex].Cells[3].Value = item.UnitPrice.ToString("N0");
            dgvItems.Rows[e.RowIndex].Cells[4].Value = item.TotalPrice.ToString("N0");
            UpdateTotalLabel();
        }

        private void BtnOk_Click(object? sender, EventArgs e)
        {
            string customer = txtCustomer.Text.Trim();
            if (string.IsNullOrEmpty(customer))
            {
                MessageBox.Show("نام مشتری الزامی است.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_items.Count == 0)
            {
                MessageBox.Show("فاکتور باید حداقل یک قلم داشته باشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // اعمال آخرین ویرایش سلول
            dgvItems.EndEdit();

            double total = _items.Sum(x => x.TotalPrice);
            string payMethod = cmbPayMethod.SelectedItem?.ToString() ?? "نقدی";

            EditedInvoice = new Invoice
            {
                Id = _original.Id,
                CustomerName = customer,
                OrderDate = _original.OrderDate,
                IsSettled = chkSettled.Checked,
                PayMethod = payMethod,
                Items = new List<OrderItem>(_items),
                Payments = new List<Payment>
                {
                    new Payment
                    {
                        Method = payMethod,
                        Amount = total,
                        PaymentDate = _original.Payments.FirstOrDefault()?.PaymentDate ?? DateTime.Now
                    }
                }
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // کلاس کمکی برای نمایش محصول در کوموباکس
        private class ProductItem
        {
            public string Text { get; set; } = "";
            public Product Product { get; set; } = null!;
            public override string ToString() => Text;
        }
    }
}