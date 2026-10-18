namespace TobaccoShop
{
    partial class UserForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.dgv = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblUser = new System.Windows.Forms.Label(); this.txtUser = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label(); this.txtPass = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label(); this.txtName = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label(); this.cmbRole = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            this.dgv.AllowUserToAddRows = false; this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.colId, this.colUser, this.colName, this.colRole, this.colActive });
            this.dgv.Location = new System.Drawing.Point(20, 20); this.dgv.Size = new System.Drawing.Size(590, 220);
            this.dgv.ReadOnly = true; this.dgv.RowHeadersVisible = false;
            this.colId.HeaderText = "کد"; this.colUser.HeaderText = "نام کاربری"; this.colName.HeaderText = "نام کامل";
            this.colRole.HeaderText = "نقش"; this.colActive.HeaderText = "فعال";
            this.lblUser.AutoSize = true; this.lblUser.Location = new System.Drawing.Point(20, 260); this.lblUser.Text = "نام کاربری:";
            this.txtUser.Location = new System.Drawing.Point(110, 257); this.txtUser.Size = new System.Drawing.Size(150, 24);
            this.lblPass.AutoSize = true; this.lblPass.Location = new System.Drawing.Point(280, 260); this.lblPass.Text = "رمز عبور:";
            this.txtPass.Location = new System.Drawing.Point(360, 257); this.txtPass.Size = new System.Drawing.Size(150, 24); this.txtPass.PasswordChar = '●';
            this.lblName.AutoSize = true; this.lblName.Location = new System.Drawing.Point(20, 300); this.lblName.Text = "نام کامل:";
            this.txtName.Location = new System.Drawing.Point(110, 297); this.txtName.Size = new System.Drawing.Size(150, 24);
            this.lblRole.AutoSize = true; this.lblRole.Location = new System.Drawing.Point(280, 300); this.lblRole.Text = "نقش:";
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.Items.AddRange(new object[] { "Admin", "Cashier" }); this.cmbRole.SelectedIndex = 1;
            this.cmbRole.Location = new System.Drawing.Point(360, 297); this.cmbRole.Size = new System.Drawing.Size(150, 24);
            this.btnAdd.BackColor = System.Drawing.Color.SeaGreen; this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(20, 350); this.btnAdd.Size = new System.Drawing.Size(150, 35); this.btnAdd.Text = "افزودن کاربر";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 410);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.dgv, this.lblUser, this.txtUser, this.lblPass, this.txtPass, this.lblName, this.txtName, this.lblRole, this.cmbRole, this.btnAdd });
            this.Font = new System.Drawing.Font("Tahoma", 10F); this.Name = "UserForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes; this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "مدیریت کاربران - فروشگاه دخانیات علی";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit(); this.ResumeLayout(false); this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId, colUser, colName, colRole, colActive;
        private System.Windows.Forms.Label lblUser, lblPass, lblName, lblRole;
        private System.Windows.Forms.TextBox txtUser, txtPass, txtName;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Button btnAdd;
    }
}
