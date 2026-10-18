using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TobaccoShop.Models;
using TobaccoShop.Services;

namespace TobaccoShop
{
    public partial class FormMain : Form
    {
        private readonly List<OrderItem> _currentOrder = new List<OrderItem>();

        public FormMain()
        {
            InitializeComponent();
            txtSearch.KeyDown += txtSearch_KeyDown;
            lstProducts.DoubleClick += lstProducts_DoubleClick;

            BuildMenu();
            LoadProducts();
            UpdateHeader();

            this.MinimumSize = new Size(900, 600);
            this.Load += (s, e) =>
            {
                this.WindowState = FormWindowState.Maximized;
                this.BeginInvoke(new Action(() =>
                {
                    FormMain_SizeChanged(this, EventArgs.Empty);
                    txtSearch.Focus();
                }));
            };
            this.SizeChanged += FormMain_SizeChanged;
        }

        private void FormMain_SizeChanged(object? sender, EventArgs e)
        {
            if (ClientSize.Width < 850 || ClientSize.Height < 550)
                return;

            int margin = 15;
            int gap = 15;
            int top = menuStrip.Height + headerPanel.Height + 8;

            // ردیف جستجو و مشتری
            lblSearch.Location = new Point(margin, top);
            txtSearch.Location = new Point(margin + 70, top - 2);
            txtSearch.Width = 220;

            lblCust.Location = new Point(margin + 310, top);
            txtCustomer.Location = new Point(margin + 400, top - 2);
            txtCustomer.Width = 200;

            // عناوین
            int titleY = top + 35;
            lblProducts.Location = new Point(margin, titleY);
            lblOrder.Location = new Point(margin + 360 + gap, titleY);

            // ارتفاع بدنه
            int bodyTop = titleY + 28;
            int bottomBarH = 130; // پنل افزودن + دکمه‌های پایین
            int bodyHeight = ClientSize.Height - bodyTop - bottomBarH;
            if (bodyHeight < 200) bodyHeight = 200;

            int menuWidth = 360;
            lstProducts.Location = new Point(margin, bodyTop);
            lstProducts.Size = new Size(menuWidth, bodyHeight);

            int gridX = margin + menuWidth + gap;
            int gridWidth = ClientSize.Width - gridX - margin;
            if (gridWidth < 300) gridWidth = 300;

            dgvOrder.Location = new Point(gridX, bodyTop);
            dgvOrder.Size = new Size(gridWidth, bodyHeight);

            // پنل افزودن (زیر لیست محصولات)
            int addY = bodyTop + bodyHeight + 8;
            panelAdd.Location = new Point(margin, addY);
            panelAdd.Size = new Size(menuWidth, 115);

            // دکمه‌ها و جمع زیر گرید
            int btnY = addY + 20;
            btnRemove.Location = new Point(gridX, btnY);
            btnClear.Location = new Point(gridX + 120, btnY);
            btnCheckout.Location = new Point(gridX + 260, btnY);
            btnCheckout.Width = Math.Max(200, gridWidth - 270);

            lblTotal.Location = new Point(gridX, btnY + 50);
            lblTotal.Width = gridWidth;
        }

        private void BuildMenu()
        {
            menuStrip.Items.Clear();

            // منوی مشترک برای همه (مدیر و صندوق‌دار)
            var mOperations = new ToolStripMenuItem("عملیات");
            mOperations.DropDownItems.Add("تاریخچه فاکتورها", null, (s, e) => OpenInvoices());
            mOperations.DropDownItems.Add(new ToolStripSeparator());
            mOperations.DropDownItems.Add("خروج از حساب", null, (s, e) => Logout());

            // 👇 اگر کاربر مدیر باشد، بقیه منوها را اضافه کن 👇
            if (Shop.Instance.IsAdmin())
            {
                mOperations.DropDownItems.Insert(0, new ToolStripMenuItem("مدیریت محصولات", null, (s, e) => OpenProducts()));

                var mInventory = new ToolStripMenuItem("انبار");
                mInventory.DropDownItems.Add("مشاهده موجودی", null, (s, e) => OpenInventory());
                //mInventory.DropDownItems.Add("ثبت خرید کالا", null, (s, e) => MessageBox.Show("در نسخه بعدی تکمیل می‌شود.", "اطلاع"));
                mInventory.DropDownItems.Add("ثبت خرید کالا", null, (s, e) => {
                    using var f = new PurchaseForm();
                    f.ShowDialog();
                    LoadProducts(); // بعد از بستن فرم خرید، لیست محصولات به‌روزرسانی شود تا موجودی جدید دیده شود
                });

                var mPersonnel = new ToolStripMenuItem("پرسنل");
                mPersonnel.DropDownItems.Add("مدیریت کارکنان", null, (s, e) => OpenEmployees());
                mPersonnel.DropDownItems.Add("حضور و غیاب", null, (s, e) => MessageBox.Show("در نسخه بعدی تکمیل می‌شود.", "اطلاع"));
                mPersonnel.DropDownItems.Add("حقوق و دستمزد", null, (s, e) => MessageBox.Show("در نسخه بعدی تکمیل می‌شود.", "اطلاع"));

                var mReports = new ToolStripMenuItem("گزارش‌ها");
                mReports.DropDownItems.Add("گزارش فروش", null, (s, e) => ShowSalesReport());
                mReports.DropDownItems.Add("هزینه‌های متفرقه", null, (s, e) => OpenMiscExpense());

                var mUsers = new ToolStripMenuItem("کاربران / تنظیمات");
                mUsers.DropDownItems.Add("تغییر رمز عبور", null, (s, e) => OpenChangePassword());
                mUsers.DropDownItems.Add(new ToolStripSeparator());
                mUsers.DropDownItems.Add("مدیریت کاربران", null, (s, e) => OpenUsers());

                menuStrip.Items.Add(mOperations);
                menuStrip.Items.Add(mInventory);
                menuStrip.Items.Add(mPersonnel);
                menuStrip.Items.Add(mReports);
                menuStrip.Items.Add(mUsers);
            }
            else
            {
                // صندوق‌دار فقط منوی عملیات (تاریخچه فاکتورها و خروج) را می‌بیند
                menuStrip.Items.Add(mOperations);
            }
        }

        private void UpdateHeader()
        {
            if (Shop.Instance.IsAuthenticated())
            {
                var u = Shop.Instance.CurrentUser;
                lblUser.Text = $"کاربر: {u.FullName}  |  نقش: {(u.IsAdmin ? "مدیر" : "صندوق‌دار")}  |  ورود: {PersianDateHelper.ToPersianDateTime(u.LoginTime)}";
            }
        }

        // ==========================================================
        // فرمت‌بندی موجودی بر اساس واحد
        // ==========================================================
        private void LoadProducts()
        {
            lstProducts.Items.Clear();
            foreach (var p in Shop.Instance.GetProducts().OrderBy(x => x.Name))
            {
                string stockFormat = p.Unit == "کیلوگرم" ? "N3" : "N0";
                string displayText = string.IsNullOrEmpty(p.Barcode)
                    ? $"{p.Name}  |  فروش: {p.Price:N0}  |  {p.Unit}  |  موجودی: {p.Stock.ToString(stockFormat)}"
                    : $"{p.Name}  |  بارکد: {p.Barcode}  |  فروش: {p.Price:N0}  |  {p.Unit}  |  موجودی: {p.Stock.ToString(stockFormat)}";

                lstProducts.Items.Add(new ProductItem { Text = displayText, Product = p });
            }
            lstProducts.DisplayMember = "Text";
        }

        private void FilterProducts()
        {
            string q = txtSearch.Text.Trim();
            lstProducts.Items.Clear();
            var list = Shop.Instance.GetProducts()
                .Where(p => string.IsNullOrEmpty(q) ||
                            p.Name.Contains(q, StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p.Name);

            foreach (var p in list)
            {
                string stockFormat = p.Unit == "کیلوگرم" ? "N3" : "N0";
                string displayText = string.IsNullOrEmpty(p.Barcode)
                    ? $"{p.Name}  |  فروش: {p.Price:N0}  |  {p.Unit}  |  موجودی: {p.Stock.ToString(stockFormat)}"
                    : $"{p.Name}  |  بارکد: {p.Barcode}  |  فروش: {p.Price:N0}  |  {p.Unit}  |  موجودی: {p.Stock.ToString(stockFormat)}";

                lstProducts.Items.Add(new ProductItem { Text = displayText, Product = p });
            }
            lstProducts.DisplayMember = "Text";
        }

        // ==========================================================
        // تغییرات در رویدادها به دلیل استفاده از ProductItem
        // ==========================================================
        private void lstProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstProducts.SelectedItem is ProductItem item)
            {
                var p = item.Product;
                string stockFormat = p.Unit == "کیلوگرم" ? "N3" : "N0";
                lblUnitInfo.Text = $"واحد: {p.Unit}  |  خرید: {p.CostPrice:N0}  |  فروش: {p.Price:N0}  |  موجودی: {p.Stock.ToString(stockFormat)}";
                numQty.DecimalPlaces = p.Unit == "کیلوگرم" ? 3 : 0;
                numQty.Increment = p.Unit == "کیلوگرم" ? 0.1M : 1;
                numQty.Value = p.Unit == "کیلوگرم" ? 0.5M : 1;
            }
        }

        private void lstProducts_DoubleClick(object? sender, EventArgs e)
        {
            if (lstProducts.SelectedItem is ProductItem item)
            {
                var p = item.Product;
                double qtyToAdd = p.Unit == "کیلوگرم" ? (double)numQty.Value : 1;
                AddProductToOrder(p, qtyToAdd);
                lstProducts.Focus();
            }
        }

        private void chkManualPrice_CheckedChanged(object sender, EventArgs e)
        {
            txtManualPrice.Enabled = chkManualPrice.Checked;
            if (!chkManualPrice.Checked) txtManualPrice.Clear();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => FilterProducts();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lstProducts.SelectedItem is not ProductItem item)
            {
                MessageBox.Show("لطفاً یک محصول انتخاب کنید.", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var p = item.Product;
            string stockFormat = p.Unit == "کیلوگرم" ? "N3" : "N0";

            double qty = (double)numQty.Value;
            if (qty <= 0)
            {
                MessageBox.Show("مقدار باید بزرگ‌تر از صفر باشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (qty > p.Stock)
            {
                MessageBox.Show($"موجودی کافی نیست.\nموجودی فعلی: {p.Stock.ToString(stockFormat)} {p.Unit}", "کمبود موجودی", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double price = p.Price;
            bool isManual = false;
            if (chkManualPrice.Checked)
            {
                if (!double.TryParse(txtManualPrice.Text.Replace(",", ""), out price) || price < 0)
                {
                    MessageBox.Show("قیمت دستی معتبر نیست.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                isManual = true;
            }

            var existing = _currentOrder.FirstOrDefault(x => x.ProductId == p.Id && x.UnitPrice == price && x.IsManualPrice == isManual);
            if (existing != null)
                existing.Quantity += qty;
            else
            {
                _currentOrder.Add(new OrderItem
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Unit = p.Unit,
                    Quantity = qty,
                    UnitPrice = price,
                    CostPrice = p.CostPrice,
                    IsManualPrice = isManual
                });
            }

            RefreshOrderGrid();
            chkManualPrice.Checked = false;
            txtManualPrice.Clear();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvOrder.CurrentRow == null || dgvOrder.CurrentRow.Index < 0) return;
            int idx = dgvOrder.CurrentRow.Index;
            if (idx < _currentOrder.Count)
            {
                _currentOrder.RemoveAt(idx);
                RefreshOrderGrid();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _currentOrder.Clear();
            RefreshOrderGrid();
        }

        private void RefreshOrderGrid()
        {
            dgvOrder.Rows.Clear();
            foreach (var item in _currentOrder)
            {
                string qtyFormat = item.Unit == "کیلوگرم" ? "N3" : "N0";
                dgvOrder.Rows.Add(
                    item.ProductName,
                    item.Quantity.ToString(qtyFormat),
                    item.Unit,
                    item.UnitPrice.ToString("N0"),
                    item.TotalPrice.ToString("N0"),
                    item.IsManualPrice ? "بله" : "");
            }
            double total = _currentOrder.Sum(x => x.TotalPrice);
            lblTotal.Text = $"جمع کل فاکتور: {total:N0} تومان";
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_currentOrder.Count == 0)
            {
                MessageBox.Show("فاکتور خالی است.", "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string customer = string.IsNullOrWhiteSpace(txtCustomer.Text) ? "مشتری متفرقه" : txtCustomer.Text.Trim();
            double total = _currentOrder.Sum(x => x.TotalPrice);

            var invoice = new Invoice
            {
                CustomerName = customer,
                OrderDate = DateTime.Now,
                IsSettled = true,
                PayMethod = "نقدی",
                Items = new List<OrderItem>(_currentOrder),
                Payments = new List<Payment> { new Payment { Method = "نقدی", Amount = total, PaymentDate = DateTime.Now } }
            };

            Shop.Instance.AddInvoice(invoice);

            MessageBox.Show("فاکتور با موفقیت ثبت شد.", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _currentOrder.Clear();
            txtCustomer.Clear();
            RefreshOrderGrid();
            LoadProducts();
        }

        private void btnLogout_Click(object sender, EventArgs e) => Logout();

        private void Logout()
        {
            var r = MessageBox.Show("آیا از حساب کاربری خارج می‌شوید؟", "خروج", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                Shop.Instance.Logout();
                this.Close();
            }
        }

        private void OpenProducts() { using var f = new ProductForm(); f.ShowDialog(); LoadProducts(); }
        private void OpenInvoices() { using var f = new InvoiceListForm(); f.ShowDialog(); }
        private void OpenInventory() { using var f = new InventoryForm(); f.ShowDialog(); LoadProducts(); }
        private void OpenEmployees()
        {
            if (!Shop.Instance.IsAdmin())
            {
                MessageBox.Show("فقط مدیر سیستم دسترسی دارد.", "دسترسی", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var f = new EmployeeForm(); f.ShowDialog();
        }
        private void OpenUsers() { using var f = new UserForm(); f.ShowDialog(); }
        private void OpenChangePassword() { using var f = new ChangePasswordForm(); f.ShowDialog(); }
        private void OpenMiscExpense() { using var f = new MiscExpenseForm(); f.ShowDialog(); }

        /// <summary>
        /// بارکدخوان معمولاً کد را تایپ کرده و Enter می‌زند.
        /// اگر بارکد پیدا شد، محصول را به سبد اضافه می‌کند.
        /// </summary>
        private void txtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            e.SuppressKeyPress = true;

            string code = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(code)) return;

            var byBarcode = Shop.Instance.GetProductByBarcode(code);
            if (byBarcode != null)
            {
                AddProductToOrder(byBarcode, 1);
                txtSearch.Clear();
                FilterProducts();
                txtSearch.Focus();
                return;
            }

            FilterProducts();
            if (lstProducts.Items.Count == 1)
            {
                lstProducts.SelectedIndex = 0;
                if (lstProducts.SelectedItem is ProductItem item && item.Product is Product p)
                {
                    AddProductToOrder(p, 1);
                    txtSearch.Clear();
                    FilterProducts();
                    txtSearch.Focus();
                }
            }
        }

        private void AddProductToOrder(Product p, double qty)
        {
            if (qty <= 0) qty = 1;

            string stockFormat = p.Unit == "کیلوگرم" ? "N3" : "N0";

            if (qty > p.Stock)
            {
                MessageBox.Show(
                    $@"موجودی کافی نیست.
«{p.Name}»
موجودی: {p.Stock.ToString(stockFormat)} {p.Unit}",
                    "کمبود موجودی", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existing = _currentOrder.FirstOrDefault(x =>
                x.ProductId == p.Id && x.UnitPrice == p.Price && !x.IsManualPrice);

            if (existing != null)
            {
                if (existing.Quantity + qty > p.Stock)
                {
                    MessageBox.Show(
                        $@"موجودی کافی نیست.
موجودی: {p.Stock.ToString(stockFormat)}",
                        "کمبود موجودی", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                existing.Quantity += qty;
            }
            else
            {
                _currentOrder.Add(new OrderItem
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Unit = p.Unit,
                    Quantity = qty,
                    UnitPrice = p.Price,
                    CostPrice = p.CostPrice,
                    IsManualPrice = false
                });
            }
            RefreshOrderGrid();
        }

        private void ShowSalesReport()
        {
            using var dlg = new ProfitReportDialog();
            dlg.ShowDialog(this);
        }
    }

    // کلاس کمکی برای نمایش متن سفارشی در لیست
    public class ProductItem
    {
        public string Text { get; set; } = "";
        public Product Product { get; set; } = null!;
    }
}