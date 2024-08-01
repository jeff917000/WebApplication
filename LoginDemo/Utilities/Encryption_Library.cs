using System.Security.Cryptography;
using System.Text;

namespace LoginDemo.Utilities
{
    public class Encryption_Library
    {
        byte[] key = Encoding.UTF8.GetBytes("KSUhBxWEwTpaCgufq8tFP7Zpzz82eWWv");
        byte[] iv = Encoding.UTF8.GetBytes("ELPM46628PRVKFB4");
        public string AesEncrypt(string str)
        {
            byte[] encrypted;
            try
            {
                using (Aes strAes = Aes.Create())
                {
                    ICryptoTransform encryptor = strAes.CreateEncryptor(key, iv);
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(str);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                };
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
