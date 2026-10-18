namespace TobaccoShop
{
    partial class EmployeeForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgv = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSalary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblFirst = new System.Windows.Forms.Label(); this.txtFirst = new System.Windows.Forms.TextBox();
            this.lblLast = new System.Windows.Forms.Label(); this.txtLast = new System.Windows.Forms.TextBox();
            this.lblNational = new System.Windows.Forms.Label(); this.txtNational = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label(); this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPosition = new System.Windows.Forms.Label(); this.txtPosition = new System.Windows.Forms.TextBox();
            this.lblSalary = new System.Windows.Forms.Label(); this.txtSalary = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button(); this.btnEdit = new System.Windows.Forms.Button(); this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            this.dgv.AllowUserToAddRows = false; this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colId, this.colName, this.colPos, this.colPhone, this.colSalary, this.colActive });
            this.dgv.Location = new System.Drawing.Point(20, 20); this.dgv.Size = new System.Drawing.Size(790, 250);
            this.dgv.ReadOnly = true; this.dgv.RowHeadersVisible = false; this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.SelectionChanged += new System.EventHandler(this.dgv_SelectionChanged);
            this.colId.HeaderText = "کد"; this.colName.HeaderText = "نام کامل"; this.colPos.HeaderText = "سمت";
            this.colPhone.HeaderText = "تلفن"; this.colSalary.HeaderText = "حقوق پایه"; this.colActive.HeaderText = "فعال";
            this.lblFirst.AutoSize = true; this.lblFirst.Location = new System.Drawing.Point(20, 290); this.lblFirst.Text = "نام:";
            this.txtFirst.Location = new System.Drawing.Point(100, 287); this.txtFirst.Size = new System.Drawing.Size(120, 24);
            this.lblLast.AutoSize = true; this.lblLast.Location = new System.Drawing.Point(240, 290); this.lblLast.Text = "نام خانوادگی:";
            this.txtLast.Location = new System.Drawing.Point(340, 287); this.txtLast.Size = new System.Drawing.Size(120, 24);
            this.lblNational.AutoSize = true; this.lblNational.Location = new System.Drawing.Point(480, 290); this.lblNational.Text = "کد ملی:";
            this.txtNational.Location = new System.Drawing.Point(550, 287); this.txtNational.Size = new System.Drawing.Size(120, 24);
            this.lblPhone.AutoSize = true; this.lblPhone.Location = new System.Drawing.Point(20, 330); this.lblPhone.Text = "تلفن:";
            this.txtPhone.Location = new System.Drawing.Point(100, 327); this.txtPhone.Size = new System.Drawing.Size(120, 24);
            this.lblPosition.AutoSize = true; this.lblPosition.Location = new System.Drawing.Point(240, 330); this.lblPosition.Text = "سمت:";
            this.txtPosition.Location = new System.Drawing.Point(340, 327); this.txtPosition.Size = new System.Drawing.Size(120, 24);
            this.lblSalary.AutoSize = true; this.lblSalary.Location = new System.Drawing.Point(480, 330); this.lblSalary.Text = "حقوق پایه:";
            this.txtSalary.Location = new System.Drawing.Point(560, 327); this.txtSalary.Size = new System.Drawing.Size(110, 24);
            this.btnAdd.BackColor = System.Drawing.Color.SeaGreen; this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(20, 380); this.btnAdd.Size = new System.Drawing.Size(100, 35); this.btnAdd.Text = "افزودن"; this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnEdit.BackColor = System.Drawing.Color.SteelBlue; this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(140, 380); this.btnEdit.Size = new System.Drawing.Size(100, 35); this.btnEdit.Text = "ویرایش"; this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            this.btnDelete.BackColor = System.Drawing.Color.IndianRed; this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(260, 380); this.btnDelete.Size = new System.Drawing.Size(100, 35); this.btnDelete.Text = "حذف"; this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 450);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.dgv, this.lblFirst, this.txtFirst, this.lblLast, this.txtLast, this.lblNational, this.txtNational, this.lblPhone, this.txtPhone, this.lblPosition, this.txtPosition, this.lblSalary, this.txtSalary, this.btnAdd, this.btnEdit, this.btnDelete });
            this.Font = new System.Drawing.Font("Tahoma", 10F); this.Name = "EmployeeForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes; this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "مدیریت کارکنان - فروشگاه دخانیات علی";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit(); this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId, colName, colPos, colPhone, colSalary, colActive;
        private System.Windows.Forms.Label lblFirst, lblLast, lblNational, lblPhone, lblPosition, lblSalary;
        private System.Windows.Forms.TextBox txtFirst, txtLast, txtNational, txtPhone, txtPosition, txtSalary;
        private System.Windows.Forms.Button btnAdd, btnEdit, btnDelete;
    }
}
