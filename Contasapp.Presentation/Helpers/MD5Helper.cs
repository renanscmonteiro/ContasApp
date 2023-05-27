using System.Security.Cryptography;
using System.Text;

namespace ContasApp.Presentation.Helpers
{
    public class MD5Helper
    {
        public static string Encrypt(string value)
        {
            using (MD5 md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(value);
                var hashBytes = md5.ComputeHash(inputBytes); //criptografia!

                var sb = new StringBuilder();
                foreach (var item in hashBytes)
                    sb.Append(item.ToString("x2")); //hexadecimal

                return sb.ToString();
            }
        }
    }
}