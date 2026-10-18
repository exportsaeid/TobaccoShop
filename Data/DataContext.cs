using System;
using System.IO;

namespace TobaccoShop.Data
{
    public class DataContext
    {
        public string RootPath { get; }

        public string ProductsPath { get; }
        public string InvoicesPath { get; }
        public string PurchasesPath { get; }
        public string StocktakesPath { get; }
        public string EmployeesPath { get; }
        public string AttendancesPath { get; }
        public string PayrollsPath { get; }
        public string MiscExpensesPath { get; }
        public string UsersPath { get; }

        public DataContext()
        {
            RootPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "TobaccoShopData"
            );

            if (!Directory.Exists(RootPath))
                Directory.CreateDirectory(RootPath);

            ProductsPath = Path.Combine(RootPath, "products_db.txt");
            InvoicesPath = Path.Combine(RootPath, "invoices_db.txt");
            PurchasesPath = Path.Combine(RootPath, "purchases_db.txt");
            StocktakesPath = Path.Combine(RootPath, "stocktakes_db.txt");
            EmployeesPath = Path.Combine(RootPath, "employees_db.txt");
            AttendancesPath = Path.Combine(RootPath, "attendances_db.txt");
            PayrollsPath = Path.Combine(RootPath, "payrolls_db.txt");
            MiscExpensesPath = Path.Combine(RootPath, "misc_expenses_db.txt");
            UsersPath = Path.Combine(RootPath, "users_db.txt");
        }
    }
}
