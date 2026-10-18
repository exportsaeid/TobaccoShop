using System;
using System.Windows.Forms;
using TobaccoShop.Models;
using TobaccoShop.Services;

namespace TobaccoShop
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgv.Rows.Clear();
            foreach (var u in Shop.Instance.GetUsers(false))
                dgv.Rows.Add(u.Id, u.Username, u.FullName, u.Role == "Admin" ? "مدیر" : "صندوق‌دار", u.IsActive ? "بله" : "خیر");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            { MessageBox.Show("نام کاربری و رمز الزامی است."); return; }
            Shop.Instance.AddUser(new User {
                Username = txtUser.Text.Trim(), Password = txtPass.Text,
                FullName = txtName.Text.Trim(), Role = cmbRole.SelectedItem?.ToString() ?? "Cashier", IsActive = true });
            LoadData(); txtUser.Clear(); txtPass.Clear(); txtName.Clear();
            MessageBox.Show("کاربر اضافه شد.");
        }
    }
}
