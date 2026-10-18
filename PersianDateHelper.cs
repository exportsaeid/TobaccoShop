using System;
using System.Globalization;

namespace TobaccoShop
{
    /// <summary>
    /// تبدیل و نمایش تاریخ شمسی (جلالی)
    /// </summary>
    public static class PersianDateHelper
    {
        private static readonly PersianCalendar Pc = new PersianCalendar();
        private static readonly CultureInfo Fa = new CultureInfo("fa-IR");

        public static string ToPersianDate(DateTime dt)
        {
            try
            {
                int y = Pc.GetYear(dt);
                int m = Pc.GetMonth(dt);
                int d = Pc.GetDayOfMonth(dt);
                return $"{y:0000}/{m:00}/{d:00}";
            }
            catch
            {
                return dt.ToString("yyyy/MM/dd");
            }
        }

        public static string ToPersianDateTime(DateTime dt)
        {
            try
            {
                return $"{ToPersianDate(dt)} {dt:HH:mm}";
            }
            catch
            {
                return dt.ToString("yyyy/MM/dd HH:mm");
            }
        }

        public static string ToPersianLong(DateTime dt)
        {
            try
            {
                int y = Pc.GetYear(dt);
                int m = Pc.GetMonth(dt);
                int d = Pc.GetDayOfMonth(dt);
                string monthName = m switch
                {
                    1 => "فروردین",
                    2 => "اردیبهشت",
                    3 => "خرداد",
                    4 => "تیر",
                    5 => "مرداد",
                    6 => "شهریور",
                    7 => "مهر",
                    8 => "آبان",
                    9 => "آذر",
                    10 => "دی",
                    11 => "بهمن",
                    12 => "اسفند",
                    _ => m.ToString()
                };
                return $"{d} {monthName} {y}";
            }
            catch
            {
                return ToPersianDate(dt);
            }
        }

        /// <summary>
        /// پارس رشته yyyy/MM/dd شمسی به DateTime میلادی (شروع روز)
        /// </summary>
        public static bool TryParsePersianDate(string text, out DateTime result)
        {
            result = DateTime.MinValue;
            if (string.IsNullOrWhiteSpace(text)) return false;
            var parts = text.Trim().Replace("-", "/").Split('/');
            if (parts.Length != 3) return false;
            if (!int.TryParse(parts[0], out int y)) return false;
            if (!int.TryParse(parts[1], out int m)) return false;
            if (!int.TryParse(parts[2], out int d)) return false;
            try
            {
                result = Pc.ToDateTime(y, m, d, 0, 0, 0, 0);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DateTime StartOfPersianMonth(DateTime dt)
        {
            int y = Pc.GetYear(dt);
            int m = Pc.GetMonth(dt);
            return Pc.ToDateTime(y, m, 1, 0, 0, 0, 0);
        }

        public static DateTime TodayStart => DateTime.Today;
    }
}
