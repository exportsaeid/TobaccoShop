using System;
using System.Linq;
using System.Windows.Forms;
using TobaccoShop.Models;
using TobaccoShop.Services;

namespace TobaccoShop
{
    public partial class EmployeeForm : Form
    {
        private Employee? _selected;

        public EmployeeForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgv.Rows.Clear();
            foreach (var e in Shop.Instance.GetEmployees(false))
                dgv.Rows.Add(e.Id, e.FullName, e.Position, e.PhoneNumber, e.BaseSalary.ToString("N0"), e.IsActive ? "بله" : "خیر");
            _selected = null;
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null) return;
            int id = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            _selected = Shop.Instance.GetEmployees(false).FirstOrDefault(x => x.Id == id);
            if (_selected != null)
            {
                txtFirst.Text = _selected.FirstName; txtLast.Text = _selected.LastName;
                txtNational.Text = _selected.NationalCode; txtPhone.Text = _selected.PhoneNumber;
                txtPosition.Text = _selected.Position; txtSalary.Text = _selected.BaseSalary.ToString("N0");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirst.Text) || string.IsNullOrWhiteSpace(txtLast.Text))
            { MessageBox.Show("نام و نام خانوادگی الزامی است."); return; }
            double.TryParse(txtSalary.Text.Replace(",", ""), out double salary);
            Shop.Instance.AddEmployee(new Employee {
                FirstName = txtFirst.Text.Trim(), LastName = txtLast.Text.Trim(),
                NationalCode = txtNational.Text.Trim(), PhoneNumber = txtPhone.Text.Trim(),
                Position = txtPosition.Text.Trim(), BaseSalary = salary, IsActive = true });
            LoadData(); MessageBox.Show("کارمند اضافه شد.");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selected == null) return;
            _selected.FirstName = txtFirst.Text.Trim(); _selected.LastName = txtLast.Text.Trim();
            _selected.NationalCode = txtNational.Text.Trim(); _selected.PhoneNumber = txtPhone.Text.Trim();
            _selected.Position = txtPosition.Text.Trim();
            double.TryParse(txtSalary.Text.Replace(",", ""), out double salary); _selected.BaseSalary = salary;
            Shop.Instance.UpdateEmployee(_selected); LoadData(); MessageBox.Show("ویرایش شد.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selected == null) return;
            if (MessageBox.Show($"حذف {_selected.FullName}؟", "تأیید", MessageBoxButtons.YesNo) == DialogResult.Yes)
            { _selected.IsActive = false; Shop.Instance.UpdateEmployee(_selected); LoadData(); }
        }
    }
}
