using System;
using System.Threading;
using System.Windows.Forms;
using TobaccoShop.Services;

namespace TobaccoShop
{
    static class Program
    {
        private static Mutex mutex = new Mutex(true, "TobaccoShop_Ali_Unique_Mutex_2026");

        [STAThread]
        static void Main()
        {
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("برنامه در حال حاضر در حال اجرا است.",
                    "توجه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // اطمینان از ایجاد سرویس
                var service = Shop.Instance;

                using (var login = new LoginForm())
                {
                    if (login.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(new FormMain());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در اجرای برنامه:\n{ex.Message}",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}
