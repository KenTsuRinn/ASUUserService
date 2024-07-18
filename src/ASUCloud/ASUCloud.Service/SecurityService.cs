using System.Security.Cryptography;
using System.Text;

namespace ASUCloud.Service
{
    public class SecurityService
    {
        private const int SALT_SIZE = 24;
        private const int PBKDF2_ITERATION = 10000;

        public string GeneratePasswordHashPBKDF2(string password)
        {
            return GeneratePasswordHashPBKDF2(password, GenerateSalt());
        }

        private string GenerateSalt()
        {
            return "SECERITY";
            //var buff = new byte[SALT_SIZE];
            //using (var rng = new RNGCryptoServiceProvider())
            //{
            //    rng.GetBytes(buff);
            //}
            //return Convert.ToBase64String(buff);
        }

        private string GeneratePasswordHashPBKDF2(string password, string salt)
        {
            var result = "";
            var encoder = new UTF8Encoding();
            var b = new Rfc2898DeriveBytes(password, encoder.GetBytes(salt), PBKDF2_ITERATION);
            var k = b.GetBytes(32);
            result = Convert.ToBase64String(k);
            return result;
        }



    }
}
