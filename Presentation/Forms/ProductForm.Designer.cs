namespace TobaccoShop
{
    partial class ProductForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgv = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThreshold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblCostPrice = new System.Windows.Forms.Label();
            this.txtCostPrice = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.lblThreshold = new System.Windows.Forms.Label();
            this.txtThreshold = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();

            this.dgv.AllowUserToAddRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colId, this.colName, this.colBarcode, this.colCost, this.colPrice, this.colUnit, this.colStock, this.colThreshold });
            this.dgv.Location = new System.Drawing.Point(20, 20);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(840, 260);
            this.dgv.TabIndex = 0;
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            this.dgv.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.colId.HeaderText = "کد"; this.colId.FillWeight = 8F;
            this.colName.HeaderText = "نام محصول"; this.colName.FillWeight = 22F;
            this.colBarcode.HeaderText = "بارکد"; this.colBarcode.FillWeight = 16F;
            this.colCost.HeaderText = "قیمت خرید"; this.colCost.FillWeight = 12F;
            this.colPrice.HeaderText = "قیمت فروش"; this.colPrice.FillWeight = 12F;
            this.colUnit.HeaderText = "واحد"; this.colUnit.FillWeight = 8F;
            this.colStock.HeaderText = "موجودی"; this.colStock.FillWeight = 10F;
            this.colThreshold.HeaderText = "آستانه"; this.colThreshold.FillWeight = 10F;

            // ردیف ۱: نام + بارکد
            this.lblName.AutoSize = true; this.lblName.Location = new System.Drawing.Point(20, 300); this.lblName.Text = "نام:";
            this.txtName.Location = new System.Drawing.Point(70, 297); this.txtName.Size = new System.Drawing.Size(200, 24);

            this.lblBarcode.AutoSize = true; this.lblBarcode.Location = new System.Drawing.Point(290, 300); this.lblBarcode.Text = "بارکد:";
            this.txtBarcode.Location = new System.Drawing.Point(350, 297); this.txtBarcode.Size = new System.Drawing.Size(200, 24);

            // ردیف ۲: قیمت خرید + فروش
            this.lblCostPrice.AutoSize = true; this.lblCostPrice.Location = new System.Drawing.Point(20, 340); this.lblCostPrice.Text = "قیمت خرید:";
            this.txtCostPrice.Location = new System.Drawing.Point(110, 337); this.txtCostPrice.Size = new System.Drawing.Size(120, 24);

            this.lblPrice.AutoSize = true; this.lblPrice.Location = new System.Drawing.Point(250, 340); this.lblPrice.Text = "قیمت فروش:";
            this.txtPrice.Location = new System.Drawing.Point(350, 337); this.txtPrice.Size = new System.Drawing.Size(120, 24);

            // ردیف ۳: واحد + موجودی + آستانه
            this.lblUnit.AutoSize = true; this.lblUnit.Location = new System.Drawing.Point(20, 380); this.lblUnit.Text = "واحد:";
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Items.AddRange(new object[] { "عدد", "کیلوگرم" });
            this.cmbUnit.Location = new System.Drawing.Point(70, 377); this.cmbUnit.Size = new System.Drawing.Size(100, 24);
            this.cmbUnit.SelectedIndex = 0;

            this.lblStock.AutoSize = true; this.lblStock.Location = new System.Drawing.Point(190, 380); this.lblStock.Text = "موجودی:";
            this.txtStock.Location = new System.Drawing.Point(260, 377); this.txtStock.Size = new System.Drawing.Size(100, 24);

            this.lblThreshold.AutoSize = true; this.lblThreshold.Location = new System.Drawing.Point(380, 380); this.lblThreshold.Text = "آستانه کمبود:";
            this.txtThreshold.Location = new System.Drawing.Point(490, 377); this.txtThreshold.Size = new System.Drawing.Size(80, 24);
            this.txtThreshold.Text = "1";

            this.btnAdd.Location = new System.Drawing.Point(20, 425); this.btnAdd.Size = new System.Drawing.Size(100, 34);
            this.btnAdd.Text = "افزودن"; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnEdit.Location = new System.Drawing.Point(130, 425); this.btnEdit.Size = new System.Drawing.Size(100, 34);
            this.btnEdit.Text = "ویرایش"; this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            this.btnDelete.Location = new System.Drawing.Point(240, 425); this.btnDelete.Size = new System.Drawing.Size(100, 34);
            this.btnDelete.Text = "حذف"; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            this.btnRefresh.Location = new System.Drawing.Point(350, 425); this.btnRefresh.Size = new System.Drawing.Size(100, 34);
            this.btnRefresh.Text = "بروزرسانی"; this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 480);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.lblName); this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblBarcode); this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.lblCostPrice); this.Controls.Add(this.txtCostPrice);
            this.Controls.Add(this.lblPrice); this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblUnit); this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.lblStock); this.Controls.Add(this.txtStock);
            this.Controls.Add(this.lblThreshold); this.Controls.Add(this.txtThreshold);
            this.Controls.Add(this.btnAdd); this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete); this.Controls.Add(this.btnRefresh);
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.Name = "ProductForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "مدیریت محصولات - فروشگاه دخانیات علی";
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MinimumSize = new System.Drawing.Size(900, 500);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId, colName, colBarcode, colCost, colPrice, colUnit, colStock, colThreshold;
        private System.Windows.Forms.Label lblName, lblBarcode, lblCostPrice, lblPrice, lblUnit, lblStock, lblThreshold;
        private System.Windows.Forms.TextBox txtName, txtBarcode, txtCostPrice, txtPrice, txtStock, txtThreshold;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Button btnAdd, btnEdit, btnDelete, btnRefresh;
    }
}
