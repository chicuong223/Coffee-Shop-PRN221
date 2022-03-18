using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Utilities
{
    public static class PasswordUtility
    {
        public static string HashPassword(string raw)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert raw text to bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(raw));

                // Convert bytes to a string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();

            }
        }
    }
}
