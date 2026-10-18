namespace TobaccoShop
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            menuStrip = new System.Windows.Forms.MenuStrip();
            headerPanel = new System.Windows.Forms.Panel();
            btnLogout = new System.Windows.Forms.Button();
            lblUser = new System.Windows.Forms.Label();
            lblAppTitle = new System.Windows.Forms.Label();
            lblSearch = new System.Windows.Forms.Label();
            txtSearch = new System.Windows.Forms.TextBox();
            lblProducts = new System.Windows.Forms.Label();
            lstProducts = new System.Windows.Forms.ListBox();
            panelAdd = new System.Windows.Forms.Panel();
            btnAdd = new System.Windows.Forms.Button();
            txtManualPrice = new System.Windows.Forms.TextBox();
            lblPrice = new System.Windows.Forms.Label();
            chkManualPrice = new System.Windows.Forms.CheckBox();
            numQty = new System.Windows.Forms.NumericUpDown();
            lblQty = new System.Windows.Forms.Label();
            lblUnitInfo = new System.Windows.Forms.Label();
            lblOrder = new System.Windows.Forms.Label();
            dgvOrder = new System.Windows.Forms.DataGridView();
            colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colManual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lblCust = new System.Windows.Forms.Label();
            txtCustomer = new System.Windows.Forms.TextBox();
            lblTotal = new System.Windows.Forms.Label();
            btnRemove = new System.Windows.Forms.Button();
            btnClear = new System.Windows.Forms.Button();
            btnCheckout = new System.Windows.Forms.Button();
            headerPanel.SuspendLayout();
            panelAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numQty).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrder).BeginInit();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Font = new System.Drawing.Font("Tahoma", 10F);
            menuStrip.Location = new System.Drawing.Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            menuStrip.Size = new System.Drawing.Size(1000, 24);
            menuStrip.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.BackColor = System.Drawing.Color.FromArgb(70, 40, 20);
            headerPanel.Controls.Add(btnLogout);
            headerPanel.Controls.Add(lblUser);
            headerPanel.Controls.Add(lblAppTitle);
            headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            headerPanel.Location = new System.Drawing.Point(0, 24);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new System.Drawing.Size(1000, 50);
            headerPanel.TabIndex = 1;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = System.Drawing.Color.FromArgb(180, 50, 50);
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLogout.ForeColor = System.Drawing.Color.White;
            btnLogout.Location = new System.Drawing.Point(12, 10);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new System.Drawing.Size(70, 30);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "خروج";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Font = new System.Drawing.Font("Tahoma", 9F);
            lblUser.ForeColor = System.Drawing.Color.WhiteSmoke;
            lblUser.Location = new System.Drawing.Point(100, 15);
            lblUser.Name = "lblUser";
            lblUser.Size = new System.Drawing.Size(33, 14);
            lblUser.TabIndex = 1;
            lblUser.Text = "کاربر:";
            // 
            // lblAppTitle
            // 
            lblAppTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblAppTitle.AutoSize = true;
            lblAppTitle.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            lblAppTitle.ForeColor = System.Drawing.Color.White;
            lblAppTitle.Location = new System.Drawing.Point(710, 11);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new System.Drawing.Size(237, 23);
            lblAppTitle.TabIndex = 0;
            lblAppTitle.Text = "🚬 فروشگاه دخانیات علی";
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new System.Drawing.Point(20, 90);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new System.Drawing.Size(58, 17);
            lblSearch.TabIndex = 2;
            lblSearch.Text = "جستجو:";
            // 
            // txtSearch
            // 
            txtSearch.Location = new System.Drawing.Point(80, 87);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new System.Drawing.Size(250, 24);
            txtSearch.TabIndex = 3;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblProducts
            // 
            lblProducts.AutoSize = true;
            lblProducts.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            lblProducts.Location = new System.Drawing.Point(20, 120);
            lblProducts.Name = "lblProducts";
            lblProducts.Size = new System.Drawing.Size(121, 17);
            lblProducts.TabIndex = 4;
            lblProducts.Text = "لیست محصولات:";
            // 
            // lstProducts
            // 
            lstProducts.Font = new System.Drawing.Font("Tahoma", 10F);
            lstProducts.FormattingEnabled = true;
            lstProducts.Location = new System.Drawing.Point(20, 145);
            lstProducts.Name = "lstProducts";
            lstProducts.Size = new System.Drawing.Size(394, 340);
            lstProducts.TabIndex = 5;
            lstProducts.SelectedIndexChanged += lstProducts_SelectedIndexChanged;
            // 
            // panelAdd
            // 
            panelAdd.BackColor = System.Drawing.Color.White;
            panelAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panelAdd.Controls.Add(btnAdd);
            panelAdd.Controls.Add(txtManualPrice);
            panelAdd.Controls.Add(lblPrice);
            panelAdd.Controls.Add(chkManualPrice);
            panelAdd.Controls.Add(numQty);
            panelAdd.Controls.Add(lblQty);
            panelAdd.Controls.Add(lblUnitInfo);
            panelAdd.Location = new System.Drawing.Point(20, 500);
            panelAdd.Name = "panelAdd";
            panelAdd.Size = new System.Drawing.Size(394, 120);
            panelAdd.TabIndex = 6;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = System.Drawing.Color.FromArgb(50, 120, 50);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAdd.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            btnAdd.ForeColor = System.Drawing.Color.White;
            btnAdd.Location = new System.Drawing.Point(10, 75);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(350, 35);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "➕ افزودن به فاکتور";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtManualPrice
            // 
            txtManualPrice.Enabled = false;
            txtManualPrice.Location = new System.Drawing.Point(270, 37);
            txtManualPrice.Name = "txtManualPrice";
            txtManualPrice.Size = new System.Drawing.Size(90, 24);
            txtManualPrice.TabIndex = 5;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new System.Drawing.Point(190, 40);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new System.Drawing.Size(76, 17);
            lblPrice.TabIndex = 4;
            lblPrice.Text = "قیمت واحد:";
            // 
            // chkManualPrice
            // 
            chkManualPrice.AutoSize = true;
            chkManualPrice.Location = new System.Drawing.Point(190, 10);
            chkManualPrice.Name = "chkManualPrice";
            chkManualPrice.Size = new System.Drawing.Size(104, 21);
            chkManualPrice.TabIndex = 3;
            chkManualPrice.Text = "قیمت دستی";
            chkManualPrice.UseVisualStyleBackColor = true;
            chkManualPrice.CheckedChanged += chkManualPrice_CheckedChanged;
            // 
            // numQty
            // 
            numQty.DecimalPlaces = 3;
            numQty.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numQty.Location = new System.Drawing.Point(70, 37);
            numQty.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            numQty.Minimum = new decimal(new int[] { 1, 0, 0, 196608 });
            numQty.Name = "numQty";
            numQty.Size = new System.Drawing.Size(100, 24);
            numQty.TabIndex = 2;
            numQty.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblQty
            // 
            lblQty.AutoSize = true;
            lblQty.Location = new System.Drawing.Point(10, 40);
            lblQty.Name = "lblQty";
            lblQty.Size = new System.Drawing.Size(43, 17);
            lblQty.TabIndex = 1;
            lblQty.Text = "مقدار:";
            // 
            // lblUnitInfo
            // 
            lblUnitInfo.AutoSize = true;
            lblUnitInfo.ForeColor = System.Drawing.Color.DarkBlue;
            lblUnitInfo.Location = new System.Drawing.Point(10, 10);
            lblUnitInfo.Name = "lblUnitInfo";
            lblUnitInfo.Size = new System.Drawing.Size(56, 17);
            lblUnitInfo.TabIndex = 0;
            lblUnitInfo.Text = "واحد: —";
            // 
            // lblOrder
            // 
            lblOrder.AutoSize = true;
            lblOrder.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            lblOrder.Location = new System.Drawing.Point(420, 90);
            lblOrder.Name = "lblOrder";
            lblOrder.Size = new System.Drawing.Size(93, 17);
            lblOrder.TabIndex = 7;
            lblOrder.Text = "فاکتور جاری:";
            // 
            // dgvOrder
            // 
            dgvOrder.AllowUserToAddRows = false;
            dgvOrder.AllowUserToDeleteRows = false;
            dgvOrder.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrder.BackgroundColor = System.Drawing.Color.White;
            dgvOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colName, colQty, colUnit, colPrice, colTotal, colManual });
            dgvOrder.Location = new System.Drawing.Point(420, 115);
            dgvOrder.Name = "dgvOrder";
            dgvOrder.ReadOnly = true;
            dgvOrder.RowHeadersVisible = false;
            dgvOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvOrder.Size = new System.Drawing.Size(540, 310);
            dgvOrder.TabIndex = 8;
            // 
            // colName
            // 
            colName.HeaderText = "محصول";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colQty
            // 
            colQty.HeaderText = "مقدار";
            colQty.Name = "colQty";
            colQty.ReadOnly = true;
            // 
            // colUnit
            // 
            colUnit.HeaderText = "واحد";
            colUnit.Name = "colUnit";
            colUnit.ReadOnly = true;
            // 
            // colPrice
            // 
            colPrice.HeaderText = "قیمت واحد";
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            // 
            // colTotal
            // 
            colTotal.HeaderText = "جمع";
            colTotal.Name = "colTotal";
            colTotal.ReadOnly = true;
            // 
            // colManual
            // 
            colManual.HeaderText = "دستی";
            colManual.Name = "colManual";
            colManual.ReadOnly = true;
            // 
            // lblCust
            // 
            lblCust.AutoSize = true;
            lblCust.Location = new System.Drawing.Point(420, 440);
            lblCust.Name = "lblCust";
            lblCust.Size = new System.Drawing.Size(78, 17);
            lblCust.TabIndex = 9;
            lblCust.Text = "نام مشتری:";
            // 
            // txtCustomer
            // 
            txtCustomer.Location = new System.Drawing.Point(510, 437);
            txtCustomer.Name = "txtCustomer";
            txtCustomer.Size = new System.Drawing.Size(200, 24);
            txtCustomer.TabIndex = 10;
            // 
            // lblTotal
            // 
            lblTotal.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            lblTotal.ForeColor = System.Drawing.Color.FromArgb(100, 40, 20);
            lblTotal.Location = new System.Drawing.Point(420, 475);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new System.Drawing.Size(300, 30);
            lblTotal.TabIndex = 11;
            lblTotal.Text = "جمع کل: ۰ تومان";
            // 
            // btnRemove
            // 
            btnRemove.BackColor = System.Drawing.Color.FromArgb(180, 60, 60);
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnRemove.ForeColor = System.Drawing.Color.White;
            btnRemove.Location = new System.Drawing.Point(420, 520);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new System.Drawing.Size(110, 40);
            btnRemove.TabIndex = 12;
            btnRemove.Text = "حذف ردیف";
            btnRemove.UseVisualStyleBackColor = false;
            btnRemove.Click += btnRemove_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = System.Drawing.Color.Gray;
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClear.ForeColor = System.Drawing.Color.White;
            btnClear.Location = new System.Drawing.Point(545, 520);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(130, 40);
            btnClear.TabIndex = 13;
            btnClear.Text = "پاک کردن فاکتور";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnCheckout
            // 
            btnCheckout.BackColor = System.Drawing.Color.FromArgb(40, 90, 140);
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCheckout.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            btnCheckout.ForeColor = System.Drawing.Color.White;
            btnCheckout.Location = new System.Drawing.Point(690, 520);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new System.Drawing.Size(270, 40);
            btnCheckout.TabIndex = 14;
            btnCheckout.Text = "✅ ثبت و تسویه فاکتور";
            btnCheckout.UseVisualStyleBackColor = false;
            btnCheckout.Click += btnCheckout_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(250, 248, 245);
            ClientSize = new System.Drawing.Size(1000, 650);
            Controls.Add(btnCheckout);
            Controls.Add(btnClear);
            Controls.Add(btnRemove);
            Controls.Add(lblTotal);
            Controls.Add(txtCustomer);
            Controls.Add(lblCust);
            Controls.Add(dgvOrder);
            Controls.Add(lblOrder);
            Controls.Add(panelAdd);
            Controls.Add(lstProducts);
            Controls.Add(lblProducts);
            Controls.Add(txtSearch);
            Controls.Add(lblSearch);
            Controls.Add(headerPanel);
            Controls.Add(menuStrip);
            Font = new System.Drawing.Font("Tahoma", 10F);
            MainMenuStrip = menuStrip;
            Name = "FormMain";
            RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "فروشگاه دخانیات علی";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            panelAdd.ResumeLayout(false);
            panelAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numQty).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrder).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblProducts;
        private System.Windows.Forms.ListBox lstProducts;
        private System.Windows.Forms.Panel panelAdd;
        private System.Windows.Forms.Label lblUnitInfo;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.NumericUpDown numQty;
        private System.Windows.Forms.CheckBox chkManualPrice;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtManualPrice;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblOrder;
        private System.Windows.Forms.DataGridView dgvOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colManual;
        private System.Windows.Forms.Label lblCust;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCheckout;
    }
}
