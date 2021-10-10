using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Bigger
{
    public static class SecurityUtil
    {
        public static string DesEncrypt(string txt, string key, string iv)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] data = Encoding.UTF8.GetBytes(txt);
                des.Key = Encoding.UTF8.GetBytes(key.PadRight(8, '0').Substring(0, 8));
                des.IV = Encoding.UTF8.GetBytes(iv.PadRight(8, '0').Substring(0, 8));
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return "";
            }
        }
        public static string DesDecrypt(string txt, string key, string iv)
        {
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] data = Convert.FromBase64String(txt);
                des.Key = Encoding.UTF8.GetBytes(key.PadRight(8, '0').Substring(0, 8));
                des.IV = Encoding.UTF8.GetBytes(iv.PadRight(8, '0').Substring(0, 8));
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return "";
            }
        }

        public static string MD5Encrypt(string txt)
        {
            byte[] data = Encoding.UTF8.GetBytes(txt);
            byte[] md5 = new MD5CryptoServiceProvider().ComputeHash(data);
            string byte2String = null;

            for (int i = 0; i < md5.Length; i++)
            {
                byte2String += md5[i].ToString("x2");
            }
            return byte2String;
        }

        /// <summary>
        /// RSA产生密钥
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="xmlPublicKey">公钥</param>
        public static void GenerateRsaKey(out string xmlPrivateKey, out string xmlPublicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            xmlPrivateKey = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }
        public static string RsaEncrypt(string txt, string xmlPublicKey)
        {
            byte[] data = Encoding.UTF8.GetBytes(txt);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            byte[] encryptData = rsa.Encrypt(data, false);
            return Convert.ToBase64String(encryptData);
        }

        public static string RsaDecrypt(string txt, string xmlPrivateKey)
        {
            byte[] data = Convert.FromBase64String(txt);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            byte[] decryptData = rsa.Decrypt(data, false);
            return Encoding.UTF8.GetString(decryptData);
        }
    }
}