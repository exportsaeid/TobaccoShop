using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using TobaccoShop.Services;

namespace TobaccoShop
{
    public partial class LoginForm : Form
    {
        private bool isFirstRun;

        public LoginForm()
        {
            isFirstRun = !Shop.Instance.HasAnyUser();

            // تنظیمات فرم
            this.Text = "ورود به سیستم - فروشگاه دخانیات علی";
            this.Size = new Size(450, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.RightToLeft = RightToLeft.Yes;
            this.Font = new Font("Tahoma", 10);
            this.BackColor = Color.White;
            this.Padding = new Padding(10);
            this.KeyPreview = true;

            // افکت محو شدن هنگام باز شدن
            this.Opacity = 0;
            Timer fadeTimer = new Timer { Interval = 20 };
            fadeTimer.Tick += (s, e) =>
            {
                if (this.Opacity < 1) this.Opacity += 0.05;
                else fadeTimer.Stop();
            };
            fadeTimer.Start();

            InitializeComponent();
            ApplyFirstRunMode();

            // استایل دادن به کنترل‌ها
            StyleControls();

            this.Paint += LoginForm_Paint;
            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnLoginOrSetup_Click(null, null); };
        }

        private void StyleControls()
        {
            // استایل دکمه ورود
            btnLogin.BackColor = Color.FromArgb(139, 69, 19);
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(160, 90, 30);
            btnLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(100, 50, 10);
            btnLogin.Font = new Font("Tahoma", 11, FontStyle.Bold);

            // استایل دکمه ایجاد مدیر
            btnSetup.BackColor = Color.FromArgb(60, 120, 60);
            btnSetup.ForeColor = Color.White;
            btnSetup.FlatStyle = FlatStyle.Flat;
            btnSetup.FlatAppearance.BorderSize = 0;
            btnSetup.Font = new Font("Tahoma", 11, FontStyle.Bold);

            // استایل فیلدها
            foreach (Control c in this.Controls)
            {
                if (c is TextBox txt)
                {
                    txt.BackColor = Color.FromArgb(250, 245, 240);
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.Font = new Font("Tahoma", 11);
                    txt.Enter += (s, e) => txt.SelectAll();
                }
            }
        }

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = this.ClientRectangle;
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect, Color.FromArgb(255, 248, 240), Color.FromArgb(240, 225, 210), LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, rect);
            }

            using (Pen pen = new Pen(Color.FromArgb(180, 120, 70), 2))
            {
                e.Graphics.DrawLine(pen, 50, 280, 400, 280);
            }
        }

        private void ApplyFirstRunMode()
        {
            if (isFirstRun)
            {
                this.Text = "فروشگاه دخانیات علی - تنظیم حساب مدیر";
                this.Height = 580;
                lblTitle.Text = "تنظیم حساب مدیر سیستم";
                lblInfo.Text = "اولین اجرای برنامه\nلطفاً حساب مدیر را ایجاد کنید";
                lblConfirm.Visible = true;
                txtConfirm.Visible = true;
                lblFullName.Visible = true;
                txtFullName.Visible = true;
                btnLogin.Visible = false;
                btnSetup.Visible = true;
            }
            else
            {
                this.Text = "فروشگاه دخانیات علی - ورود";
                this.Height = 500;
                lblTitle.Text = "ورود به سیستم";
                lblInfo.Text = "نام کاربری و رمز عبور را وارد کنید";
                lblConfirm.Visible = false;
                txtConfirm.Visible = false;
                lblFullName.Visible = false;
                txtFullName.Visible = false;
                btnLogin.Visible = true;
                btnSetup.Visible = false;
            }
            txtUsername.Focus();
        }

        private void BtnLoginOrSetup_Click(object sender, EventArgs e)
        {
            if (isFirstRun)
                btnSetup.PerformClick();
            else
                btnLogin.PerformClick();
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;
            string confirm = txtConfirm.Text;
            string fullName = txtFullName.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass) || string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("تمام فیلدها الزامی هستند.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (pass.Length < 4)
            {
                MessageBox.Show("رمز عبور باید حداقل ۴ کاراکتر باشد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (pass != confirm)
            {
                MessageBox.Show("رمز عبور و تکرار آن یکسان نیستند.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Shop.Instance.CreateFirstAdmin(user, pass, fullName);

            if (Shop.Instance.Login(user, pass))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("نام کاربری و رمز عبور را وارد کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Shop.Instance.Login(user, pass))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // افکت لرزش در صورت خطا
                Timer errorTimer = new Timer { Interval = 30 };
                int shakeCount = 0;
                int originalLeft = this.Left;
                errorTimer.Tick += (s, ev) =>
                {
                    if (shakeCount < 10)
                    {
                        this.Left = originalLeft + (shakeCount % 2 == 0 ? -12 : 12);
                        shakeCount++;
                    }
                    else
                    {
                        this.Left = originalLeft;
                        errorTimer.Stop();
                        MessageBox.Show("نام کاربری یا رمز عبور اشتباه است.", "خطای ورود", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Clear();
                        txtPassword.Focus();
                    }
                };
                errorTimer.Start();
            }
        }
    }
}