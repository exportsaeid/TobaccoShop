using System;
using System.Linq;
using System.Windows.Forms;
using TobaccoShop.Services;

namespace TobaccoShop
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string current = txtCurrent.Text;
            string newPass = txtNew.Text;
            string confirm = txtConfirm.Text;

            if (string.IsNullOrEmpty(current) || string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("تمام فیلدها الزامی هستند.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (newPass.Length < 4)
            {
                MessageBox.Show("رمز جدید باید حداقل ۴ کاراکتر باشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (newPass != confirm)
            {
                MessageBox.Show("رمز جدید و تکرار آن یکسان نیستند.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = Shop.Instance.GetUsers(false)
                .FirstOrDefault(u => u.Id == Shop.Instance.CurrentUser.UserId);

            if (user == null || !SecurityHelper.VerifyPassword(current, user.Password))
            {
                MessageBox.Show("رمز فعلی اشتباه است.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCurrent.Clear();
                txtCurrent.Focus();
                return;
            }

            Shop.Instance.ChangePassword(user.Id, newPass);
            MessageBox.Show("رمز عبور با موفقیت تغییر کرد.", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
