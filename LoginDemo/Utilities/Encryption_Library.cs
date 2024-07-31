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

            //雖然不需要，但還是加上判斷了。
            if (string.IsNullOrEmpty(str)) return null;

            using (Aes strAes = Aes.Create())
            {
                strAes.Key = key;
                strAes.IV = iv;
                ICryptoTransform encryptor = strAes.CreateEncryptor(strAes.Key, strAes.IV);
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
    }
}
