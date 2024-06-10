using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SearchUniversity.DataContext.Models
{
    public class PasswordManager
    {
        public static string HashPassword(string password, string salt)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(salt)))
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = hmac.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static string GenerateSalt()
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzAnhHieuDepTrai442350123456789";
        }
    }
}
