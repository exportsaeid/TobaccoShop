using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using TobaccoShop.Services;

namespace TobaccoShop
{
    public class ProfitReportDialog : Form
    {
        private static readonly PersianCalendar Pc = new PersianCalendar();
        private static readonly string[] MonthNames =
        {
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور",
            "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"
        };

        // انتخاب تاریخ
        private NumericUpDown numFromYear = null!;
        private ComboBox cmbFromMonth = null!;
        private NumericUpDown numFromDay = null!;
        private NumericUpDown numToYear = null!;
        private ComboBox cmbToMonth = null!;
        private NumericUpDown numToDay = null!;

        // کارت‌های خلاصه
        private Label lblSalesValue = null!;
        private Label lblCogsValue = null!;
        private Label lblGrossProfitValue = null!;
        private Label lblExpensesValue = null!;
        private Label lblNetProfitValue = null!;

        private DataGridView dgvDetails = null!;
        private Label lblInfoMessage = null!;

        public ProfitReportDialog()
        {
            BuildUi();
            SetPersianDate(numFromYear, cmbFromMonth, numFromDay, DateTime.Today.AddDays(-30));
            SetPersianDate(numToYear, cmbToMonth, numToDay, DateTime.Today);
            Calculate();
        }

        private void BuildUi()
        {
            this.Text = "📊 گزارش جامع سود و زیان";
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Tahoma", 10F);
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.BackColor = Color.FromArgb(245, 248, 250);
            this.ShowInTaskbar = false;

            // =========================================================
            // پنل بالایی: انتخاب تاریخ (وسط‌چین با TableLayoutPanel)
            // =========================================================
            var topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 170,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            var lblTitle = new Label
            {
                Text = "بازه زمانی گزارش (تاریخ شمسی)",
                Font = new Font("Tahoma", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };
            topPanel.Controls.Add(lblTitle);

            // 👇 کانتینر اصلی برای وسط‌چین کردن دقیق 👇
            var tlpMain = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = Color.Transparent
            };

            // سه ستون: چپ (50%) ، وسط (AutoSize) ، راست (50%)
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            // پنل داخلی که فیلدها و دکمه‌ها را در خود جای می‌دهد
            var innerPanel = new TableLayoutPanel
            {
                ColumnCount = 1,
                RowCount = 2,
                AutoSize = true,
                Anchor = AnchorStyles.None, // 👈 تراز وسط برای کل مجموعه
                BackColor = Color.Transparent
            };

            // ردیف اول: فیلدهای تاریخ
            var datePanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                Anchor = AnchorStyles.None, // 👈 تراز وسط برای فیلدها
                Margin = new Padding(0, 10, 0, 0)
            };

            var lblFrom = new Label { Text = "از تاریخ:", AutoSize = true, Font = new Font("Tahoma", 10, FontStyle.Bold), Anchor = AnchorStyles.Left, Margin = new Padding(5, 7, 5, 0) };
            numFromYear = MakeYearBox(); numFromYear.Margin = new Padding(0, 0, 10, 0);
            cmbFromMonth = MakeMonthBox(); cmbFromMonth.Margin = new Padding(0, 0, 10, 0);
            numFromDay = MakeDayBox(); numFromDay.Margin = new Padding(0, 0, 20, 0);

            var lblTo = new Label { Text = "تا تاریخ:", AutoSize = true, Font = new Font("Tahoma", 10, FontStyle.Bold), Anchor = AnchorStyles.Left, Margin = new Padding(5, 7, 5, 0) };
            numToYear = MakeYearBox(); numToYear.Margin = new Padding(0, 0, 10, 0);
            cmbToMonth = MakeMonthBox(); cmbToMonth.Margin = new Padding(0, 0, 10, 0);
            numToDay = MakeDayBox(); numToDay.Margin = new Padding(0, 0, 5, 0);

            datePanel.Controls.AddRange(new Control[] { lblFrom, numFromYear, cmbFromMonth, numFromDay, lblTo, numToYear, cmbToMonth, numToDay });

            EventHandler refreshDays = (s, e) =>
            {
                ClampDay(numFromYear, cmbFromMonth, numFromDay);
                ClampDay(numToYear, cmbToMonth, numToDay);
            };
            numFromYear.ValueChanged += refreshDays;
            cmbFromMonth.SelectedIndexChanged += refreshDays;
            numToYear.ValueChanged += refreshDays;
            cmbToMonth.SelectedIndexChanged += refreshDays;

            // ردیف دوم: دکمه‌ها
            var btnPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                Anchor = AnchorStyles.None, // 👈 تراز وسط برای دکمه‌ها
                Margin = new Padding(0, 15, 0, 0)
            };

            var btnToday = MakeButton("امروز", Color.LightGray);
            btnToday.Click += (s, e) =>
            {
                SetPersianDate(numFromYear, cmbFromMonth, numFromDay, DateTime.Today);
                SetPersianDate(numToYear, cmbToMonth, numToDay, DateTime.Today);
                Calculate();
            };

            var btnThisMonth = MakeButton("این ماه", Color.LightBlue);
            btnThisMonth.Click += (s, e) =>
            {
                SetPersianDate(numFromYear, cmbFromMonth, numFromDay, PersianDateHelper.StartOfPersianMonth(DateTime.Today));
                SetPersianDate(numToYear, cmbToMonth, numToDay, DateTime.Today);
                Calculate();
            };

            var btnLast30 = MakeButton("۳۰ روز اخیر", Color.LightBlue);
            btnLast30.Click += (s, e) =>
            {
                SetPersianDate(numFromYear, cmbFromMonth, numFromDay, DateTime.Today.AddDays(-30));
                SetPersianDate(numToYear, cmbToMonth, numToDay, DateTime.Today);
                Calculate();
            };

            var btnCalc = MakeButton("محاسبه گزارش", Color.FromArgb(46, 204, 113), Color.White);
            btnCalc.Size = new Size(140, 36);
            btnCalc.Font = new Font("Tahoma", 10, FontStyle.Bold);
            btnCalc.Click += (s, e) => Calculate();

            btnPanel.Controls.AddRange(new Control[] { btnToday, btnThisMonth, btnLast30, btnCalc });

            // اضافه کردن ردیف‌ها به پنل داخلی
            innerPanel.Controls.Add(datePanel, 0, 0);
            innerPanel.Controls.Add(btnPanel, 0, 1);

            // قرار دادن پنل داخلی در ستون وسطِ TableLayoutPanel اصلی
            tlpMain.Controls.Add(innerPanel, 1, 0);
            topPanel.Controls.Add(tlpMain);

            // =========================================================
            // پنل وسط: کارت‌های خلاصه
            // =========================================================
            var summaryPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 150,
                BackColor = Color.FromArgb(245, 248, 250),
                Padding = new Padding(15)
            };

            var tlpCards = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                RowCount = 1
            };
            for (int i = 0; i < 5; i++)
                tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            var card1 = CreateCard("💰 کل فروش", out lblSalesValue, Color.FromArgb(52, 152, 219));
            var card2 = CreateCard("📦 بهای تمام‌شده", out lblCogsValue, Color.FromArgb(243, 156, 18));
            var card3 = CreateCard("📈 سود ناخالص", out lblGrossProfitValue, Color.FromArgb(39, 174, 96));
            var card4 = CreateCard("💸 هزینه‌ها", out lblExpensesValue, Color.FromArgb(231, 76, 60));
            var card5 = CreateCard("🎯 سود خالص", out lblNetProfitValue, Color.FromArgb(142, 68, 173));

            tlpCards.Controls.Add(card1, 0, 0);
            tlpCards.Controls.Add(card2, 1, 0);
            tlpCards.Controls.Add(card3, 2, 0);
            tlpCards.Controls.Add(card4, 3, 0);
            tlpCards.Controls.Add(card5, 4, 0);

            summaryPanel.Controls.Add(tlpCards);

            // =========================================================
            // پنل پایین: جدول جزئیات
            // =========================================================
            var bottomPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(25)
            };

            var lblDetailsTitle = new Label
            {
                Text = "📋 ریز تفکیک مالی",
                Font = new Font("Tahoma", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Dock = DockStyle.Top,
                Height = 30
            };

            dgvDetails = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(230, 230, 230),
                Font = new Font("Tahoma", 10),
                RowTemplate = { Height = 40 }
            };

            dgvDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 11, FontStyle.Bold);
            dgvDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDetails.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(44, 62, 80);
            dgvDetails.ColumnHeadersHeight = 40;
            dgvDetails.EnableHeadersVisualStyles = false;

            dgvDetails.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvDetails.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvDetails.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colItem", HeaderText = "شرح", FillWeight = 40 });
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAmount", HeaderText = "مبلغ (تومان)", FillWeight = 30 });
            dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPercent", HeaderText = "درصد از فروش", FillWeight = 30 });

            lblInfoMessage = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Tahoma", 9, FontStyle.Italic),
                ForeColor = Color.Gray
            };

            bottomPanel.Controls.Add(dgvDetails);
            bottomPanel.Controls.Add(lblInfoMessage);
            bottomPanel.Controls.Add(lblDetailsTitle);

            // =========================================================
            // پنل بسیار پایین: دکمه بستن (همیشه در پایین صفحه)
            // =========================================================
            var btnPanelBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(245, 248, 250)
            };

            var btnClose = new Button
            {
                Text = "❌ بستن",
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Tahoma", 10, FontStyle.Bold),
                DialogResult = DialogResult.Cancel
            };
            btnClose.FlatAppearance.BorderSize = 0;

            // وسط‌چین کردن دکمه بستن در پنل پایینی
            btnPanelBottom.Resize += (s, e) => {
                btnClose.Location = new Point((btnPanelBottom.Width - btnClose.Width) / 2, 10);
            };
            btnClose.Location = new Point((this.Width - btnClose.Width) / 2, 10);

            btnPanelBottom.Controls.Add(btnClose);

            // =========================================================
            // اضافه کردن به فرم (ترتیب Dock از پایین به بالا)
            // =========================================================
            this.Controls.Add(bottomPanel);      // Fill (باقی فضا)
            this.Controls.Add(summaryPanel);     // Top (کارت‌ها)
            this.Controls.Add(topPanel);         // Top (انتخاب تاریخ)
            this.Controls.Add(btnPanelBottom);   // Bottom (دکمه بستن)

            this.CancelButton = btnClose;
        }

        // =========================================================
        // متدهای ساخت کنترل‌ها
        // =========================================================
        private Panel CreateCard(string title, out Label valueLabel, Color accentColor)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Margin = new Padding(5),
                Padding = new Padding(0)
            };

            var colorBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 8,
                BackColor = accentColor
            };
            card.Controls.Add(colorBar);

            var lblTitle = new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                Font = new Font("Tahoma", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30,
                Padding = new Padding(5, 8, 5, 0)
            };
            card.Controls.Add(lblTitle);

            var lblValue = new Label
            {
                Text = "0",
                Dock = DockStyle.Fill,
                Font = new Font("Tahoma", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblValue);

            valueLabel = lblValue;
            return card;
        }

        private Button MakeButton(string text, Color backColor, Color? foreColor = null)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(95, 32),
                BackColor = backColor,
                ForeColor = foreColor ?? Color.Black,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Tahoma", 9, FontStyle.Bold),
                Margin = new Padding(5, 0, 5, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private NumericUpDown MakeYearBox()
        {
            int cy = Pc.GetYear(DateTime.Today);
            return new NumericUpDown
            {
                Width = 70,
                Minimum = 1390,
                Maximum = 1450,
                Value = cy,
                TextAlign = HorizontalAlignment.Center,
                Font = new Font("Tahoma", 10)
            };
        }

        private ComboBox MakeMonthBox()
        {
            var c = new ComboBox
            {
                Width = 90,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Tahoma", 10)
            };
            c.Items.AddRange(MonthNames);
            c.SelectedIndex = 0;
            return c;
        }

        private NumericUpDown MakeDayBox()
        {
            return new NumericUpDown
            {
                Width = 55,
                Minimum = 1,
                Maximum = 31,
                Value = 1,
                TextAlign = HorizontalAlignment.Center,
                Font = new Font("Tahoma", 10)
            };
        }

        private void SetPersianDate(NumericUpDown year, ComboBox month, NumericUpDown day, DateTime dt)
        {
            int y = Pc.GetYear(dt);
            int m = Pc.GetMonth(dt);
            int d = Pc.GetDayOfMonth(dt);
            year.Value = Math.Max(year.Minimum, Math.Min(year.Maximum, y));
            month.SelectedIndex = Math.Max(0, Math.Min(11, m - 1));
            int maxDay = Pc.GetDaysInMonth(y, m);
            day.Maximum = maxDay;
            day.Value = Math.Max(1, Math.Min(maxDay, d));
        }

        private void ClampDay(NumericUpDown year, ComboBox month, NumericUpDown day)
        {
            int y = (int)year.Value;
            int m = month.SelectedIndex + 1;
            if (m < 1) m = 1;
            int maxDay = 30;
            try { maxDay = Pc.GetDaysInMonth(y, m); } catch { }
            day.Maximum = maxDay;
            if (day.Value > maxDay) day.Value = maxDay;
        }

        private bool TryGetDate(NumericUpDown year, ComboBox month, NumericUpDown day, out DateTime result)
        {
            result = DateTime.MinValue;
            try
            {
                int y = (int)year.Value;
                int m = month.SelectedIndex + 1;
                int d = (int)day.Value;
                result = Pc.ToDateTime(y, m, d, 0, 0, 0, 0);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Calculate()
        {
            if (!TryGetDate(numFromYear, cmbFromMonth, numFromDay, out DateTime from))
            {
                MessageBox.Show("تاریخ شروع نامعتبر است.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!TryGetDate(numToYear, cmbToMonth, numToDay, out DateTime toDate))
            {
                MessageBox.Show("تاریخ پایان نامعتبر است.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var to = toDate.Date.AddDays(1).AddSeconds(-1);
            if (to < from)
            {
                MessageBox.Show("تاریخ پایان نباید قبل از تاریخ شروع باشد.", "خطا",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double sales = Shop.Instance.GetTotalSales(from, to);
            double cogs = Shop.Instance.GetTotalCogs(from, to);
            double grossProfit = sales - cogs;
            double misc = Shop.Instance.GetTotalMiscExpenses(from, to);
            double payroll = Shop.Instance.GetTotalPayroll(from, to);
            double purchases = Shop.Instance.GetTotalPurchaseCosts(from, to);
            double totalExpenses = misc + payroll;
            double netProfit = grossProfit - totalExpenses;
            int invCount = Shop.Instance.GetInvoiceCount(from, to);

            // به‌روزرسانی کارت‌ها
            lblSalesValue.Text = sales.ToString("N0");
            lblCogsValue.Text = cogs.ToString("N0");
            lblGrossProfitValue.Text = grossProfit.ToString("N0");
            lblExpensesValue.Text = totalExpenses.ToString("N0");
            lblNetProfitValue.Text = netProfit.ToString("N0");

            // رنگ سود خالص
            lblNetProfitValue.ForeColor = netProfit >= 0
                ? Color.FromArgb(39, 174, 96)
                : Color.FromArgb(231, 76, 60);
            lblGrossProfitValue.ForeColor = grossProfit >= 0
                ? Color.FromArgb(39, 174, 96)
                : Color.FromArgb(231, 76, 60);

            // پر کردن جدول
            dgvDetails.Rows.Clear();

            // محاسبه درصد از فروش
            double PercentOf(double val) => sales > 0 ? (val / sales) * 100 : 0;

            dgvDetails.Rows.Add("💰 کل فروش", sales.ToString("N0"), "100%");
            dgvDetails.Rows[dgvDetails.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(235, 245, 251);
            dgvDetails.Rows[dgvDetails.Rows.Count - 1].DefaultCellStyle.Font = new Font("Tahoma", 10, FontStyle.Bold);

            dgvDetails.Rows.Add("📦 بهای تمام‌شده کالای فروش‌رفته", cogs.ToString("N0"), $"{PercentOf(cogs):0.0}%");
            dgvDetails.Rows[dgvDetails.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(252, 245, 230);

            dgvDetails.Rows.Add("📈 سود ناخالص", grossProfit.ToString("N0"), $"{PercentOf(grossProfit):0.0}%");
            dgvDetails.Rows[dgvDetails.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(235, 251, 238);
            dgvDetails.Rows[dgvDetails.Rows.Count - 1].DefaultCellStyle.Font = new Font("Tahoma", 10, FontStyle.Bold);

            dgvDetails.Rows.Add("💸 هزینه‌های متفرقه", misc.ToString("N0"), $"{PercentOf(misc):0.0}%");
            dgvDetails.Rows.Add("👤 حقوق و دستمزد", payroll.ToString("N0"), $"{PercentOf(payroll):0.0}%");

            dgvDetails.Rows.Add("💵 جمع هزینه‌ها", totalExpenses.ToString("N0"), $"{PercentOf(totalExpenses):0.0}%");
            dgvDetails.Rows[dgvDetails.Rows.Count - 1].DefaultCellStyle.BackColor = Color.FromArgb(251, 235, 235);

            dgvDetails.Rows.Add("🎯 سود خالص نهایی", netProfit.ToString("N0"), $"{PercentOf(netProfit):0.0}%");
            dgvDetails.Rows[dgvDetails.Rows.Count - 1].DefaultCellStyle.BackColor = netProfit >= 0
                ? Color.FromArgb(235, 251, 238)
                : Color.FromArgb(251, 235, 235);
            dgvDetails.Rows[dgvDetails.Rows.Count - 1].DefaultCellStyle.Font = new Font("Tahoma", 11, FontStyle.Bold);
            dgvDetails.Rows[dgvDetails.Rows.Count - 1].DefaultCellStyle.ForeColor = netProfit >= 0
                ? Color.FromArgb(39, 174, 96)
                : Color.FromArgb(231, 76, 60);

            // اطلاعات تکمیلی
            string fromFa = PersianDateHelper.ToPersianDate(from);
            string toFa = PersianDateHelper.ToPersianDate(toDate);
            lblInfoMessage.Text = $"بازه: {fromFa} تا {toFa}  |  تعداد فاکتورهای صادر شده: {invCount:N0}  |  خرید کالا (انبار): {purchases:N0} تومان";
        }
    }
}