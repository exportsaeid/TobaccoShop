namespace TobaccoShop
{
    partial class InventoryForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgv = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colThreshold = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colName, this.colUnit, this.colStock, this.colThreshold, this.colStatus });
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Name = "dgv"; this.dgv.ReadOnly = true; this.dgv.RowHeadersVisible = false;
            this.colName.HeaderText = "محصول"; this.colName.Name = "colName"; this.colName.ReadOnly = true;
            this.colUnit.HeaderText = "واحد"; this.colUnit.Name = "colUnit"; this.colUnit.ReadOnly = true;
            this.colStock.HeaderText = "موجودی"; this.colStock.Name = "colStock"; this.colStock.ReadOnly = true;
            this.colThreshold.HeaderText = "آستانه"; this.colThreshold.Name = "colThreshold"; this.colThreshold.ReadOnly = true;
            this.colStatus.HeaderText = "وضعیت"; this.colStatus.Name = "colStatus"; this.colStatus.ReadOnly = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 450);
            this.Controls.Add(this.dgv);
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.Name = "InventoryForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "موجودی انبار - فروشگاه دخانیات علی";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName, colUnit, colStock, colThreshold, colStatus;
    }
}
