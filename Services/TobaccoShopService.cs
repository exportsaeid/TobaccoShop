using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using TobaccoShop.Data;
using TobaccoShop.Models;

namespace TobaccoShop.Services
{
    public class TobaccoShopService
    {
        private readonly DataContext _context;
        private readonly ProductRepository _productRepo;
        private readonly InvoiceRepository _invoiceRepo;
        private readonly EmployeeRepository _employeeRepo;
        private readonly AttendanceRepository _attendanceRepo;
        private readonly PayrollRepository _payrollRepo;
        private readonly UserRepository _userRepo;

        private List<MiscellaneousExpense> _miscExpenses = new List<MiscellaneousExpense>();
        private List<Purchase> _purchases = new List<Purchase>();
        private List<Stocktake> _stocktakes = new List<Stocktake>();

        public UserSession CurrentUser { get; set; } = new UserSession();

        public TobaccoShopService()
        {
            _context = new DataContext();
            _productRepo = new ProductRepository(_context.ProductsPath);
            _invoiceRepo = new InvoiceRepository(_context.InvoicesPath);
            _employeeRepo = new EmployeeRepository(_context.EmployeesPath);
            _attendanceRepo = new AttendanceRepository(_context.AttendancesPath);
            _payrollRepo = new PayrollRepository(_context.PayrollsPath);
            _userRepo = new UserRepository(_context.UsersPath);

            LoadMiscExpenses();
            LoadPurchases();
            LoadStocktakes();
        }

        // ==================== Authentication ====================
        public bool HasAnyUser() => _userRepo.GetAll().Any();

        public bool Login(string username, string password)
        {
            var user = _userRepo.GetByUsername(username);
            if (user != null && user.IsActive && SecurityHelper.VerifyPassword(password, user.Password))
            {
                CurrentUser = new UserSession
                {
                    UserId = user.Id,
                    Username = user.Username,
                    FullName = user.FullName,
                    Role = user.Role,
                    LoginTime = DateTime.Now
                };
                user.LastLogin = DateTime.Now;
                _userRepo.Update(user);
                return true;
            }
            CurrentUser = new UserSession();
            return false;
        }

        public void Logout() => CurrentUser = new UserSession();

        public bool IsAuthenticated() => CurrentUser.IsAuthenticated;
        public bool IsAdmin() => CurrentUser.IsAdmin;

        public void CreateFirstAdmin(string username, string password, string fullName)
        {
            if (HasAnyUser()) return;

            var user = new User
            {
                Username = username,
                Password = SecurityHelper.HashPassword(password),
                FullName = fullName,
                Role = "Admin",
                IsActive = true
            };
            _userRepo.Add(user);
        }

        public List<User> GetUsers(bool activeOnly = true)
        {
            var list = _userRepo.GetAll();
            return activeOnly ? list.Where(u => u.IsActive).ToList() : list;
        }

        public void AddUser(User user)
        {
            user.Password = SecurityHelper.HashPassword(user.Password);
            _userRepo.Add(user);
        }

        public void UpdateUser(User user) => _userRepo.Update(user);

        public void ChangePassword(int userId, string newPassword)
        {
            var user = _userRepo.GetById(userId);
            if (user != null)
            {
                user.Password = SecurityHelper.HashPassword(newPassword);
                _userRepo.Update(user);
            }
        }

        // ==================== Products ====================
        public List<Product> GetProducts() => _productRepo.GetAll();

        public Product? GetProductById(int id) => _productRepo.GetById(id);

        public Product? GetProductByBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode)) return null;
            barcode = barcode.Trim();
            return _productRepo.GetAll()
                .FirstOrDefault(p => !string.IsNullOrEmpty(p.Barcode) &&
                    string.Equals(p.Barcode.Trim(), barcode, StringComparison.OrdinalIgnoreCase));
        }

        public void AddProduct(Product p) => _productRepo.Add(p);

        public void UpdateProduct(Product p) => _productRepo.Update(p);

        public void DeleteProduct(int id) => _productRepo.Delete(id);

        public void UpdateStock(int productId, double delta)
        {
            var p = _productRepo.GetById(productId);
            if (p != null)
            {
                p.Stock += delta;
                if (p.Stock < 0) p.Stock = 0;
                _productRepo.Update(p);
            }
        }

        // ==================== Invoices / Sales ====================
        public List<Invoice> GetInvoices() => _invoiceRepo.GetAll();

        public Invoice? GetInvoiceById(int id) => _invoiceRepo.GetById(id);

        public void AddInvoice(Invoice inv)
        {
            // کاهش موجودی
            foreach (var item in inv.Items)
            {
                UpdateStock(item.ProductId, -item.Quantity);
            }
            _invoiceRepo.Add(inv);
        }

        public void UpdateInvoice(Invoice inv)
        {
            var old = _invoiceRepo.GetById(inv.Id);
            if (old == null) return;

            // برگرداندن موجودی قبلی
            foreach (var item in old.Items)
                UpdateStock(item.ProductId, item.Quantity);

            // کسر موجودی جدید
            foreach (var item in inv.Items)
                UpdateStock(item.ProductId, -item.Quantity);

            _invoiceRepo.Update(inv);
        }

        public void DeleteInvoice(int id)
        {
            var inv = _invoiceRepo.GetById(id);
            if (inv == null) return;

            // برگرداندن موجودی به انبار
            foreach (var item in inv.Items)
                UpdateStock(item.ProductId, item.Quantity);

            _invoiceRepo.Delete(id);
        }

        // ==================== Employees ====================
        public List<Employee> GetEmployees(bool activeOnly = true)
        {
            var list = _employeeRepo.GetAll();
            return activeOnly ? list.Where(e => e.IsActive).ToList() : list;
        }

        public void AddEmployee(Employee e) => _employeeRepo.Add(e);
        public void UpdateEmployee(Employee e) => _employeeRepo.Update(e);
        public void DeleteEmployee(int id) => _employeeRepo.Delete(id);

        // ==================== Attendance ====================
        public List<Attendance> GetAttendances() => _attendanceRepo.GetAll();
        public void AddAttendance(Attendance a) => _attendanceRepo.Add(a);
        public void UpdateAttendance(Attendance a) => _attendanceRepo.Update(a);

        // ==================== Payroll ====================
        public List<Payroll> GetPayrolls() => _payrollRepo.GetAll();
        public void AddPayroll(Payroll p) => _payrollRepo.Add(p);
        public void UpdatePayroll(Payroll p) => _payrollRepo.Update(p);

        // ==================== Misc Expenses ====================
        public List<MiscellaneousExpense> GetMiscExpenses() => _miscExpenses.ToList();

        public void AddMiscExpense(MiscellaneousExpense e)
        {
            e.Id = _miscExpenses.Count > 0 ? _miscExpenses.Max(x => x.Id) + 1 : 1;
            _miscExpenses.Add(e);
            SaveMiscExpenses();
        }

        private void LoadMiscExpenses()
        {
            _miscExpenses.Clear();
            if (!File.Exists(_context.MiscExpensesPath)) return;
            try
            {
                foreach (var line in File.ReadAllLines(_context.MiscExpensesPath))
                {
                    var p = line.Split('|');
                    if (p.Length < 7) continue;
                    _miscExpenses.Add(new MiscellaneousExpense
                    {
                        Id = int.Parse(p[0]),
                        Title = p[1].Replace("¦", "|"),
                        Amount = double.Parse(p[2], CultureInfo.InvariantCulture),
                        Date = DateTime.Parse(p[3]),
                        Category = p[4].Replace("¦", "|"),
                        Notes = p[5].Replace("¦", "|"),
                        RecordedBy = p[6].Replace("¦", "|")
                    });
                }
            }
            catch { }
        }

        private void SaveMiscExpenses()
        {
            try
            {
                var lines = _miscExpenses.Select(e =>
                    $"{e.Id}|{e.Title.Replace("|", "¦")}|{e.Amount.ToString(CultureInfo.InvariantCulture)}|{e.Date:o}|{e.Category.Replace("|", "¦")}|{e.Notes.Replace("|", "¦")}|{e.RecordedBy.Replace("|", "¦")}");
                File.WriteAllLines(_context.MiscExpensesPath, lines);
            }
            catch { }
        }

        // ==================== Purchases ====================
        public List<Purchase> GetPurchases() => _purchases.ToList();

        public void AddPurchase(Purchase pur)
        {
            pur.Id = _purchases.Count > 0 ? _purchases.Max(x => x.Id) + 1 : 1;
            foreach (var item in pur.Items)
            {
                UpdateStock(item.ProductId, item.Quantity);
            }
            _purchases.Add(pur);
            SavePurchases();
        }

        private void LoadPurchases()
        {
            _purchases.Clear();
            if (!File.Exists(_context.PurchasesPath)) return;
            try
            {
                Purchase? current = null;
                foreach (var line in File.ReadAllLines(_context.PurchasesPath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (line == "---")
                    {
                        if (current != null) _purchases.Add(current);
                        current = null;
                        continue;
                    }

                    var parts = line.Split('|');
                    if (parts[0] == "H" && parts.Length >= 4)
                    {
                        current = new Purchase
                        {
                            Id = int.Parse(parts[1]),
                            SupplierName = parts[2].Replace("¦", "|"),
                            PurchaseDate = DateTime.Parse(parts[3]),
                            Notes = parts.Length > 4 ? parts[4].Replace("¦", "|") : ""
                        };
                    }
                    else if (parts[0] == "I" && current != null && parts.Length >= 6)
                    {
                        current.Items.Add(new PurchaseItem
                        {
                            ProductId = int.Parse(parts[1]),
                            ProductName = parts[2].Replace("¦", "|"),
                            Unit = parts[3],
                            Quantity = double.Parse(parts[4], CultureInfo.InvariantCulture),
                            UnitPrice = double.Parse(parts[5], CultureInfo.InvariantCulture)
                        });
                    }
                }
                if (current != null) _purchases.Add(current);
            }
            catch { }
        }

        private void SavePurchases()
        {
            try
            {
                var lines = new List<string>();
                foreach (var pur in _purchases)
                {
                    lines.Add($"H|{pur.Id}|{pur.SupplierName.Replace("|", "¦")}|{pur.PurchaseDate:o}|{pur.Notes.Replace("|", "¦")}");
                    foreach (var item in pur.Items)
                    {
                        lines.Add($"I|{item.ProductId}|{item.ProductName.Replace("|", "¦")}|{item.Unit}|{item.Quantity.ToString(CultureInfo.InvariantCulture)}|{item.UnitPrice.ToString(CultureInfo.InvariantCulture)}");
                    }
                    lines.Add("---");
                }
                File.WriteAllLines(_context.PurchasesPath, lines);
            }
            catch { }
        }

        // ==================== Stocktake ====================
        public List<Stocktake> GetStocktakes() => _stocktakes.ToList();

        public void AddStocktake(Stocktake st)
        {
            st.Id = _stocktakes.Count > 0 ? _stocktakes.Max(x => x.Id) + 1 : 1;
            foreach (var item in st.Items)
            {
                var p = _productRepo.GetById(item.ProductId);
                if (p != null)
                {
                    p.Stock = item.ActualStock;
                    _productRepo.Update(p);
                }
            }
            _stocktakes.Add(st);
        }

        private void LoadStocktakes() { }

        // ==================== Reports ====================
        public double GetTotalSales(DateTime from, DateTime to)
        {
            return _invoiceRepo.GetAll()
                .Where(i => i.OrderDate >= from && i.OrderDate <= to)
                .Sum(i => i.TotalAmount);
        }

        /// <summary>فقط هزینه‌های متفرقه (سازگاری با کد قبلی)</summary>
        public double GetTotalExpenses(DateTime from, DateTime to) => GetTotalMiscExpenses(from, to);

        public double GetTotalMiscExpenses(DateTime from, DateTime to)
        {
            return _miscExpenses
                .Where(e => e.Date >= from && e.Date <= to)
                .Sum(e => e.Amount);
        }

        public double GetTotalPurchaseCosts(DateTime from, DateTime to)
        {
            return _purchases
                .Where(p => p.PurchaseDate >= from && p.PurchaseDate <= to)
                .Sum(p => p.TotalAmount);
        }

        public double GetTotalPayroll(DateTime from, DateTime to)
        {
            return _payrollRepo.GetAll()
                .Where(p => p.IsPaid && p.PaymentDate >= from && p.PaymentDate <= to)
                .Sum(p => p.NetSalary);
        }

        /// <summary>
        /// بهای تمام‌شده کالای فروش‌رفته = مجموع (مقدار × قیمت خرید ثبت‌شده روی قلم فاکتور)
        /// </summary>
        public double GetTotalCogs(DateTime from, DateTime to)
        {
            return _invoiceRepo.GetAll()
                .Where(i => i.OrderDate >= from && i.OrderDate <= to)
                .SelectMany(i => i.Items)
                .Sum(item => item.Quantity * item.CostPrice);
        }

        public int GetInvoiceCount(DateTime from, DateTime to)
        {
            return _invoiceRepo.GetAll()
                .Count(i => i.OrderDate >= from && i.OrderDate <= to);
        }
    }

    // Singleton برای دسترسی آسان
    public static class Shop
    {
        private static TobaccoShopService? _instance;
        public static TobaccoShopService Instance => _instance ??= new TobaccoShopService();
    }
}
