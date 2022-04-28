using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wpf45_vlc_UI.Constructer
{
    public class Security //class thực hiện mã hóa và giải mã
    {
        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";//đặt key để mã hóa dữ liệu

        public string Encrypt(string text)//mã hóa
        {
            using (var md5 = new MD5CryptoServiceProvider())//thực hiện mã hóa
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        public string Decrypt(string cipher)//giải mã
        {
            if (string.IsNullOrEmpty(cipher))//kiểm tra đầu vào là trông hay null không
            {
                return null;//ngắt câu lệnh nếu có giá trị null hoặc trống
            }
            using (var md5 = new MD5CryptoServiceProvider())//thực hiện giải mã
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        try
                        {
                            byte[] cipherBytes = Convert.FromBase64String(cipher);
                            byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                            return UTF8Encoding.UTF8.GetString(bytes);
                        }
                        catch (CryptographicException ex)
                        {
                            MessageBox.Show("Phiên Đăng nhập thất bại\n Exception Message: " + ex.Message);
                            return null;
                        }
                        
                    }
                }
            }
        }
    }
}
