using System;

namespace TobaccoShop.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public bool IsPresent { get; set; } = true;
        public string Notes { get; set; } = "";

        public TimeSpan WorkHours => CheckOut > CheckIn ? CheckOut - CheckIn : TimeSpan.Zero;
    }
}
