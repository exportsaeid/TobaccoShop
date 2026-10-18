namespace TobaccoShop
{
    partial class MiscExpenseForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgv = new System.Windows.Forms.DataGridView();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTitle = new System.Windows.Forms.Label(); this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label(); this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label(); this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            this.dgv.AllowUserToAddRows = false; this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colDate, this.colTitle, this.colAmount, this.colBy });
            this.dgv.Location = new System.Drawing.Point(20, 20); this.dgv.Size = new System.Drawing.Size(640, 250);
            this.dgv.ReadOnly = true; this.dgv.RowHeadersVisible = false;
            this.colDate.HeaderText = "تاریخ"; this.colTitle.HeaderText = "عنوان"; this.colAmount.HeaderText = "مبلغ"; this.colBy.HeaderText = "ثبت‌کننده";
            this.lblTitle.AutoSize = true; this.lblTitle.Location = new System.Drawing.Point(20, 290); this.lblTitle.Text = "عنوان:";
            this.txtTitle.Location = new System.Drawing.Point(80, 287); this.txtTitle.Size = new System.Drawing.Size(250, 24);
            this.lblAmount.AutoSize = true; this.lblAmount.Location = new System.Drawing.Point(350, 290); this.lblAmount.Text = "مبلغ:";
            this.txtAmount.Location = new System.Drawing.Point(400, 287); this.txtAmount.Size = new System.Drawing.Size(150, 24);
            this.lblNotes.AutoSize = true; this.lblNotes.Location = new System.Drawing.Point(20, 330); this.lblNotes.Text = "توضیحات:";
            this.txtNotes.Location = new System.Drawing.Point(100, 327); this.txtNotes.Size = new System.Drawing.Size(450, 24);
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(140, 60, 40); this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(20, 380); this.btnAdd.Size = new System.Drawing.Size(150, 35); this.btnAdd.Text = "ثبت هزینه";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 450);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.dgv, this.lblTitle, this.txtTitle, this.lblAmount, this.txtAmount, this.lblNotes, this.txtNotes, this.btnAdd });
            this.Font = new System.Drawing.Font("Tahoma", 10F); this.Name = "MiscExpenseForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes; this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "هزینه‌های متفرقه - فروشگاه دخانیات علی";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit(); this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate, colTitle, colAmount, colBy;
        private System.Windows.Forms.Label lblTitle, lblAmount, lblNotes;
        private System.Windows.Forms.TextBox txtTitle, txtAmount, txtNotes;
        private System.Windows.Forms.Button btnAdd;
    }
}
