using System;

namespace TobaccoShop.Models
{
    public class Payroll
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = "";
        public int Year { get; set; }
        public int Month { get; set; }
        public double BaseSalary { get; set; }
        public double OvertimeHours { get; set; }
        public double OvertimeAmount { get; set; }
        public double Deductions { get; set; }
        public double Bonuses { get; set; }
        public double NetSalary { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string Notes { get; set; } = "";
        public bool IsPaid { get; set; }
    }
}
