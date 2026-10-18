using System;
using System.Linq;
using System.Windows.Forms;
using TobaccoShop.Models;
using TobaccoShop.Services;

namespace TobaccoShop
{
    public partial class MiscExpenseForm : Form
    {
        public MiscExpenseForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgv.Rows.Clear();
            foreach (var e in Shop.Instance.GetMiscExpenses().OrderByDescending(x => x.Date))
                dgv.Rows.Add(PersianDateHelper.ToPersianDate(e.Date), e.Title, e.Amount.ToString("N0"), e.RecordedBy);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text)) return;
            if (!double.TryParse(txtAmount.Text.Replace(",", ""), out double amount) || amount <= 0)
            { MessageBox.Show("مبلغ معتبر نیست."); return; }
            Shop.Instance.AddMiscExpense(new MiscellaneousExpense {
                Title = txtTitle.Text.Trim(), Amount = amount, Date = DateTime.Now,
                Notes = txtNotes.Text.Trim(), RecordedBy = Shop.Instance.CurrentUser.FullName });
            LoadData(); txtTitle.Clear(); txtAmount.Clear(); txtNotes.Clear();
            MessageBox.Show("ثبت شد.");
        }
    }
}
