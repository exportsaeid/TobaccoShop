using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace TobaccoShop.Services
{
    public static class NumberHelper
    {
        /// <summary>
        /// تبدیل رشته به عدد (با پشتیبانی از اعداد فارسی و حذف کاما)
        /// </summary>
        public static string NormalizeNumber(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "";
            var sb = new StringBuilder();
            foreach (char c in input.Trim())
            {
                if (c >= '\u06F0' && c <= '\u06F9') { sb.Append((char)('0' + (c - '\u06F0'))); continue; }
                if (c >= '\u0660' && c <= '\u0669') { sb.Append((char)('0' + (c - '\u0660'))); continue; }
                if (c == ',' || c == '٬' || c == '٫' || c == ' ' || c == '\u00A0' || c == '/') continue;
                if (c == '.') { sb.Append('.'); continue; }
                if (char.IsDigit(c) || c == '-' || c == '.') sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// فرمت کردن خودکار یک TextBox با جداکننده 3 رقم (مثل 1,500,000)
        /// </summary>
        public static void FormatTextBoxNumber(TextBox txt)
        {
            if (!txt.Focused) return;

            int cursorPosition = txt.SelectionStart;
            string rawText = NormalizeNumber(txt.Text);

            if (double.TryParse(rawText, out double value))
            {
                string formatted = value.ToString("N0", CultureInfo.InvariantCulture);
                if (txt.Text != formatted)
                {
                    txt.Text = formatted;
                    int diff = formatted.Length - rawText.Length;
                    txt.SelectionStart = Math.Max(0, cursorPosition + diff);
                }
            }
        }
    }
}