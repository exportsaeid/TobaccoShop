using System;

namespace TobaccoShop.Models
{
    public class UserSession
    {
        public int UserId { get; set; }
        public string Username { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Role { get; set; } = "";
        public DateTime LoginTime { get; set; }

        public bool IsAuthenticated => UserId > 0 && !string.IsNullOrEmpty(Username);
        public bool IsAdmin => Role == "Admin";
        public bool IsCashier => Role == "Cashier" || Role == "Admin";
    }
}
