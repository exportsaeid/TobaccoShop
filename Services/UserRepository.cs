using System;
using TobaccoShop.Models;

namespace TobaccoShop.Services
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(string filePath) : base(filePath) { }

        protected override int GetId(User entity) => entity.Id;
        protected override void SetId(User entity, int id) => entity.Id = id;

        protected override string Serialize(User u)
        {
            string lastLogin = u.LastLogin.HasValue ? u.LastLogin.Value.ToString("o") : "";
            return $"{u.Id}|{Escape(u.Username)}|{u.Password}|{Escape(u.FullName)}|{u.Role}|{u.IsActive}|{lastLogin}";
        }

        protected override User? Deserialize(string line)
        {
            try
            {
                var parts = line.Split('|');
                if (parts.Length < 6) return null;
                DateTime? last = null;
                if (parts.Length > 6 && !string.IsNullOrEmpty(parts[6]))
                    last = DateTime.Parse(parts[6]);

                return new User
                {
                    Id = int.Parse(parts[0]),
                    Username = Unescape(parts[1]),
                    Password = parts[2],
                    FullName = Unescape(parts[3]),
                    Role = parts[4],
                    IsActive = bool.Parse(parts[5]),
                    LastLogin = last
                };
            }
            catch { return null; }
        }

        public User? GetByUsername(string username)
        {
            return _items.Find(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        private string Escape(string s) => s?.Replace("|", "¦") ?? "";
        private string Unescape(string s) => s?.Replace("¦", "|") ?? "";
    }
}
