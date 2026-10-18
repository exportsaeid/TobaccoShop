using System;

namespace TobaccoShop.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FullName => $"{FirstName} {LastName}";
        public string NationalCode { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Position { get; set; } = "";
        public DateTime HireDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public double BaseSalary { get; set; }
        public double HourlyRate { get; set; }
        public double OvertimeRate { get; set; } = 1.4;
        public string Notes { get; set; } = "";
    }
}
