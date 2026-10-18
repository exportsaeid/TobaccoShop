using System;
using System.Globalization;
using TobaccoShop.Models;

namespace TobaccoShop.Services
{
    public class EmployeeRepository : BaseRepository<Employee>
    {
        public EmployeeRepository(string filePath) : base(filePath) { }

        protected override int GetId(Employee e) => e.Id;
        protected override void SetId(Employee e, int id) => e.Id = id;

        protected override string Serialize(Employee e)
        {
            return $"{e.Id}|{Escape(e.FirstName)}|{Escape(e.LastName)}|{Escape(e.NationalCode)}|{Escape(e.PhoneNumber)}|{Escape(e.Position)}|{e.HireDate:o}|{e.IsActive}|{e.BaseSalary.ToString(CultureInfo.InvariantCulture)}|{e.HourlyRate.ToString(CultureInfo.InvariantCulture)}|{e.OvertimeRate.ToString(CultureInfo.InvariantCulture)}|{Escape(e.Notes)}";
        }

        protected override Employee? Deserialize(string line)
        {
            try
            {
                var p = line.Split('|');
                if (p.Length < 12) return null;
                return new Employee
                {
                    Id = int.Parse(p[0]),
                    FirstName = Unescape(p[1]),
                    LastName = Unescape(p[2]),
                    NationalCode = Unescape(p[3]),
                    PhoneNumber = Unescape(p[4]),
                    Position = Unescape(p[5]),
                    HireDate = DateTime.Parse(p[6]),
                    IsActive = bool.Parse(p[7]),
                    BaseSalary = double.Parse(p[8], CultureInfo.InvariantCulture),
                    HourlyRate = double.Parse(p[9], CultureInfo.InvariantCulture),
                    OvertimeRate = double.Parse(p[10], CultureInfo.InvariantCulture),
                    Notes = Unescape(p[11])
                };
            }
            catch { return null; }
        }

        private string Escape(string s) => s?.Replace("|", "¦") ?? "";
        private string Unescape(string s) => s?.Replace("¦", "|") ?? "";
    }
}
