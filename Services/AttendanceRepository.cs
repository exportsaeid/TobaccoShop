using System;
using System.Globalization;
using TobaccoShop.Models;

namespace TobaccoShop.Services
{
    public class AttendanceRepository : BaseRepository<Attendance>
    {
        public AttendanceRepository(string filePath) : base(filePath) { }

        protected override int GetId(Attendance a) => a.Id;
        protected override void SetId(Attendance a, int id) => a.Id = id;

        protected override string Serialize(Attendance a)
        {
            return $"{a.Id}|{a.EmployeeId}|{a.Date:o}|{a.CheckIn}|{a.CheckOut}|{a.IsPresent}|{Escape(a.Notes)}";
        }

        protected override Attendance? Deserialize(string line)
        {
            try
            {
                var p = line.Split('|');
                if (p.Length < 7) return null;
                return new Attendance
                {
                    Id = int.Parse(p[0]),
                    EmployeeId = int.Parse(p[1]),
                    Date = DateTime.Parse(p[2]),
                    CheckIn = TimeSpan.Parse(p[3]),
                    CheckOut = TimeSpan.Parse(p[4]),
                    IsPresent = bool.Parse(p[5]),
                    Notes = Unescape(p[6])
                };
            }
            catch { return null; }
        }

        private string Escape(string s) => s?.Replace("|", "¦") ?? "";
        private string Unescape(string s) => s?.Replace("¦", "|") ?? "";
    }
}
