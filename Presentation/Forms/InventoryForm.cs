using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TobaccoShop.Services;

namespace TobaccoShop
{
    public partial class InventoryForm : Form
    {
        public InventoryForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgv.Rows.Clear();
            foreach (var p in Shop.Instance.GetProducts().OrderBy(x => x.Name))
            {
                string status = p.Stock <= p.LowStockThreshold ? "⚠ کمبود" : "عادی";
                int row = dgv.Rows.Add(p.Name, p.Unit, p.Stock.ToString("N3"), p.LowStockThreshold.ToString("N3"), status);
                if (p.Stock <= p.LowStockThreshold)
                    dgv.Rows[row].DefaultCellStyle.ForeColor = Color.DarkRed;
            }
        }
    }
}
