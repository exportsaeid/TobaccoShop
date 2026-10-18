using System;
using System.Globalization;
using TobaccoShop.Models;

namespace TobaccoShop.Services
{
    public class PayrollRepository : BaseRepository<Payroll>
    {
        public PayrollRepository(string filePath) : base(filePath) { }

        protected override int GetId(Payroll p) => p.Id;
        protected override void SetId(Payroll p, int id) => p.Id = id;

        protected override string Serialize(Payroll p)
        {
            return $"{p.Id}|{p.EmployeeId}|{Escape(p.EmployeeName)}|{p.Year}|{p.Month}|{p.BaseSalary.ToString(CultureInfo.InvariantCulture)}|{p.OvertimeHours.ToString(CultureInfo.InvariantCulture)}|{p.OvertimeAmount.ToString(CultureInfo.InvariantCulture)}|{p.Deductions.ToString(CultureInfo.InvariantCulture)}|{p.Bonuses.ToString(CultureInfo.InvariantCulture)}|{p.NetSalary.ToString(CultureInfo.InvariantCulture)}|{p.PaymentDate:o}|{Escape(p.Notes)}|{p.IsPaid}";
        }

        protected override Payroll? Deserialize(string line)
        {
            try
            {
                var p = line.Split('|');
                if (p.Length < 14) return null;
                return new Payroll
                {
                    Id = int.Parse(p[0]),
                    EmployeeId = int.Parse(p[1]),
                    EmployeeName = Unescape(p[2]),
                    Year = int.Parse(p[3]),
                    Month = int.Parse(p[4]),
                    BaseSalary = double.Parse(p[5], CultureInfo.InvariantCulture),
                    OvertimeHours = double.Parse(p[6], CultureInfo.InvariantCulture),
                    OvertimeAmount = double.Parse(p[7], CultureInfo.InvariantCulture),
                    Deductions = double.Parse(p[8], CultureInfo.InvariantCulture),
                    Bonuses = double.Parse(p[9], CultureInfo.InvariantCulture),
                    NetSalary = double.Parse(p[10], CultureInfo.InvariantCulture),
                    PaymentDate = DateTime.Parse(p[11]),
                    Notes = Unescape(p[12]),
                    IsPaid = bool.Parse(p[13])
                };
            }
            catch { return null; }
        }

        private string Escape(string s) => s?.Replace("|", "¦") ?? "";
        private string Unescape(string s) => s?.Replace("¦", "|") ?? "";
    }
}
