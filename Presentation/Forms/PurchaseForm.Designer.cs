namespace TobaccoShop
{
    partial class PurchaseForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.lblInvoiceNum = new System.Windows.Forms.Label();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();

            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();

            // --- پنل سمت راست (لیست محصولات و افزودن) ---
            this.rightPanel = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblProductsTitle = new System.Windows.Forms.Label();
            this.lstProducts = new System.Windows.Forms.ListBox();
            this.panelAdd = new System.Windows.Forms.Panel();
            this.lblUnitInfo = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.numQty = new System.Windows.Forms.NumericUpDown();
            this.lblBuyPrice = new System.Windows.Forms.Label();
            this.numBuyPrice = new System.Windows.Forms.NumericUpDown();
            this.btnAdd = new System.Windows.Forms.Button();

            // --- پنل سمت چپ (جدول اقلام خرید) ---
            this.leftPanel = new System.Windows.Forms.Panel();
            this.lblOrderTitle = new System.Windows.Forms.Label();
            this.dgvPurchaseItems = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBuyPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();

            this.headerPanel.SuspendLayout();
            this.mainLayout.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.panelAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBuyPrice)).BeginInit();
            this.leftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseItems)).BeginInit();
            this.SuspendLayout();

            // ==================== headerPanel ====================
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Height = 70;
            this.headerPanel.BackColor = System.Drawing.Color.White;
            this.headerPanel.Padding = new System.Windows.Forms.Padding(15);

            this.lblSupplier.Text = "نام تامین‌کننده:";
            this.lblSupplier.Location = new System.Drawing.Point(800, 25);
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblSupplier.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblSupplier.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;

            this.txtSupplier.Location = new System.Drawing.Point(680, 22);
            this.txtSupplier.Width = 200;
            this.txtSupplier.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSupplier.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;

            this.lblInvoiceNum.Text = "شماره فاکتور خرید:";
            this.lblInvoiceNum.Location = new System.Drawing.Point(550, 25);
            this.lblInvoiceNum.AutoSize = true;
            this.lblInvoiceNum.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblInvoiceNum.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblInvoiceNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;

            this.txtInvoiceNumber.Location = new System.Drawing.Point(430, 22);
            this.txtInvoiceNumber.Width = 150;
            this.txtInvoiceNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtInvoiceNumber.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;

            this.headerPanel.Controls.Add(this.lblSupplier);
            this.headerPanel.Controls.Add(this.txtSupplier);
            this.headerPanel.Controls.Add(this.lblInvoiceNum);
            this.headerPanel.Controls.Add(this.txtInvoiceNumber);

            // ==================== mainLayout (تقسیم‌بندی صفحه) ====================
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.ColumnCount = 2;
            this.mainLayout.RowCount = 1;
            // ستون راست (لیست) عرض ثابت 400، ستون چپ (جدول) پرکننده فضا
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Padding = new System.Windows.Forms.Padding(10);

            // ==================== rightPanel (لیست) ====================
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Padding = new System.Windows.Forms.Padding(10);

            this.lblSearch.Text = "🔍 جستجوی محصول:";
            this.lblSearch.Location = new System.Drawing.Point(10, 10);
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);

            this.txtSearch.Location = new System.Drawing.Point(130, 7);
            this.txtSearch.Width = 200;
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);

            this.lblProductsTitle.Text = "📦 لیست کالاها:";
            this.lblProductsTitle.Location = new System.Drawing.Point(10, 45);
            this.lblProductsTitle.AutoSize = true;
            this.lblProductsTitle.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblProductsTitle.ForeColor = System.Drawing.Color.DarkBlue;

            this.lstProducts.Location = new System.Drawing.Point(10, 70);
            this.lstProducts.Size = new System.Drawing.Size(350, 250);
            this.lstProducts.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lstProducts.DisplayMember = "Text";
            this.lstProducts.SelectedIndexChanged += new System.EventHandler(this.LstProducts_SelectedIndexChanged);
            this.lstProducts.DoubleClick += new System.EventHandler(this.BtnAdd_Click);
            this.lstProducts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right));

            this.panelAdd.Location = new System.Drawing.Point(10, 330);
            this.panelAdd.Size = new System.Drawing.Size(350, 130);
            this.panelAdd.BackColor = System.Drawing.Color.White;
            this.panelAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));

            this.lblUnitInfo.Text = "وضعیت: -";
            this.lblUnitInfo.Location = new System.Drawing.Point(10, 10);
            this.lblUnitInfo.AutoSize = true;
            this.lblUnitInfo.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblUnitInfo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);

            this.lblQty.Text = "تعداد:";
            this.lblQty.Location = new System.Drawing.Point(280, 40);
            this.lblQty.AutoSize = true;

            this.numQty.Location = new System.Drawing.Point(200, 37);
            this.numQty.Width = 70;
            this.numQty.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numQty.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.numQty.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.numQty.DecimalPlaces = 3;

            this.lblBuyPrice.Text = "قیمت خرید (واحد):";
            this.lblBuyPrice.Location = new System.Drawing.Point(280, 75);
            this.lblBuyPrice.AutoSize = true;

            this.numBuyPrice.Location = new System.Drawing.Point(130, 72);
            this.numBuyPrice.Width = 140;
            this.numBuyPrice.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.numBuyPrice.ThousandsSeparator = true;

            this.btnAdd.Text = "➕ افزودن به لیست";
            this.btnAdd.Location = new System.Drawing.Point(10, 65);
            this.btnAdd.Size = new System.Drawing.Size(110, 40);
            this.btnAdd.BackColor = System.Drawing.Color.SeaGreen;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);

            this.panelAdd.Controls.Add(this.lblUnitInfo);
            this.panelAdd.Controls.Add(this.lblQty);
            this.panelAdd.Controls.Add(this.numQty);
            this.panelAdd.Controls.Add(this.lblBuyPrice);
            this.panelAdd.Controls.Add(this.numBuyPrice);
            this.panelAdd.Controls.Add(this.btnAdd);

            this.rightPanel.Controls.Add(this.lblSearch);
            this.rightPanel.Controls.Add(this.txtSearch);
            this.rightPanel.Controls.Add(this.lblProductsTitle);
            this.rightPanel.Controls.Add(this.lstProducts);
            this.rightPanel.Controls.Add(this.panelAdd);

            // ==================== leftPanel (جدول) ====================
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanel.Padding = new System.Windows.Forms.Padding(10);

            this.lblOrderTitle.Text = "📋 اقلام فاکتور خرید:";
            this.lblOrderTitle.Location = new System.Drawing.Point(10, 10);
            this.lblOrderTitle.AutoSize = true;
            this.lblOrderTitle.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblOrderTitle.ForeColor = System.Drawing.Color.DarkRed;

            this.dgvPurchaseItems.Location = new System.Drawing.Point(10, 40);
            this.dgvPurchaseItems.Size = new System.Drawing.Size(500, 350);
            this.dgvPurchaseItems.AllowUserToAddRows = false;
            this.dgvPurchaseItems.ReadOnly = true;
            this.dgvPurchaseItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPurchaseItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchaseItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvPurchaseItems.RowHeadersVisible = false;
            this.dgvPurchaseItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPurchaseItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPurchaseItems.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);
            this.dgvPurchaseItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

            this.dgvPurchaseItems.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.dgvPurchaseItems.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvPurchaseItems.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.dgvPurchaseItems.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvPurchaseItems.ColumnHeadersHeight = 40;
            this.dgvPurchaseItems.EnableHeadersVisualStyles = false;

            this.dgvPurchaseItems.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvPurchaseItems.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(52, 152, 219);
            this.dgvPurchaseItems.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;

            this.colName.HeaderText = "نام محصول";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;

            this.colQty.HeaderText = "تعداد";
            this.colQty.Name = "colQty";
            this.colQty.ReadOnly = true;

            this.colUnit.HeaderText = "واحد";
            this.colUnit.Name = "colUnit";
            this.colUnit.ReadOnly = true;

            this.colBuyPrice.HeaderText = "قیمت خرید واحد";
            this.colBuyPrice.Name = "colBuyPrice";
            this.colBuyPrice.ReadOnly = true;

            this.colTotal.HeaderText = "جمع";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;

            this.dgvPurchaseItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colQty,
            this.colUnit,
            this.colBuyPrice,
            this.colTotal});

            this.btnRemove.Text = "❌ حذف ردیف";
            this.btnRemove.Location = new System.Drawing.Point(10, 410);
            this.btnRemove.Size = new System.Drawing.Size(120, 40);
            this.btnRemove.BackColor = System.Drawing.Color.MistyRose;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Click += new System.EventHandler(this.BtnRemove_Click);

            this.lblTotal.Text = "جمع کل خرید: 0 تومان";
            this.lblTotal.Location = new System.Drawing.Point(150, 415);
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));

            this.btnSave.Text = "✅ ثبت نهایی خرید و افزایش موجودی";
            this.btnSave.Location = new System.Drawing.Point(300, 400);
            this.btnSave.Size = new System.Drawing.Size(280, 45);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            this.leftPanel.Controls.Add(this.lblOrderTitle);
            this.leftPanel.Controls.Add(this.dgvPurchaseItems);
            this.leftPanel.Controls.Add(this.btnRemove);
            this.leftPanel.Controls.Add(this.lblTotal);
            this.leftPanel.Controls.Add(this.btnSave);

            // اضافه کردن به TableLayout
            this.mainLayout.Controls.Add(this.rightPanel, 0, 0);
            this.mainLayout.Controls.Add(this.leftPanel, 1, 0);

            // ==================== PurchaseForm ====================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.mainLayout);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.Name = "PurchaseForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "🛒 ثبت فاکتور خرید کالا (افزایش موجودی)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.ShowInTaskbar = false;

            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.rightPanel.ResumeLayout(false);
            this.rightPanel.PerformLayout();
            this.panelAdd.ResumeLayout(false);
            this.panelAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBuyPrice)).EndInit();
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseItems)).EndInit();
            this.mainLayout.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.Label lblInvoiceNum;
        private System.Windows.Forms.TextBox txtInvoiceNumber;

        private System.Windows.Forms.TableLayoutPanel mainLayout;

        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblProductsTitle;
        private System.Windows.Forms.ListBox lstProducts;
        private System.Windows.Forms.Panel panelAdd;
        private System.Windows.Forms.Label lblUnitInfo;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.NumericUpDown numQty;
        private System.Windows.Forms.Label lblBuyPrice;
        private System.Windows.Forms.NumericUpDown numBuyPrice;
        private System.Windows.Forms.Button btnAdd;

        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Label lblOrderTitle;
        private System.Windows.Forms.DataGridView dgvPurchaseItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBuyPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnSave;
    }
}