using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace FastDev
{
    public static class SecurityUtil
    {
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

        /// <summary>
        /// Rsa加密
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="xmlPublicKey"></param>
        /// <returns></returns>
        public static string RsaEncrypt(string txt, string xmlPublicKey)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(txt);
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                byte[] encryptData = rsa.Encrypt(data, false);
                return Convert.ToBase64String(encryptData);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);

                return "";
            }
        }

        /// <summary>
        /// Rsa解密
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="xmlPrivateKey"></param>
        /// <returns></returns>
        public static string RsaDecrypt(string txt, string xmlPrivateKey)
        {
            try
            {
                byte[] data = Convert.FromBase64String(txt);
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                byte[] decryptData = rsa.Decrypt(data, false);
                return Encoding.UTF8.GetString(decryptData);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);

                return "";
            }
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="txt">需签名的数据</param>
        /// <returns>签名后的值</returns>
        public static string SignatureFormatter(string xmlPrivateKey, string txt)
        {
            byte[] data = Encoding.UTF8.GetBytes(txt);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            byte[] singatureData = rsa.SignData(data, new SHA256CryptoServiceProvider());
            return Convert.ToBase64String(singatureData);
        }

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="txt">待验证的数据</param>
        /// <param name="singatureData">签名数据</param>
        /// <returns>签名是否符合</returns>
        public static bool SignatureDeformatter(string xmlPublicKey, string txt, string singatureData)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(txt);
                byte[] rgbSignature = Convert.FromBase64String(singatureData);
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                return rsa.VerifyData(data, new SHA256CryptoServiceProvider(), rgbSignature);
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="key">支持16，24，32长度，分别对应128，192，256位加密</param>
        /// <returns></returns>
        public static string AESEncrypt(string txt, string key)
        {
            try
            {
                key = key.PadRight(16, 'x').Substring(0, 16);
                string iv = key;
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                byte[] data = Encoding.UTF8.GetBytes(txt);
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(iv);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);

                return "";
            }
        }


        /// <summary>
        /// aes解密
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESDecrypt(string txt, string key)
        {
            try
            {
                key = key.PadRight(16, 'x').Substring(0, 16);
                string iv = key;
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                byte[] data = Convert.FromBase64String(txt);
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Encoding.UTF8.GetBytes(iv);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);

                return "";
            }
        }
    }
}