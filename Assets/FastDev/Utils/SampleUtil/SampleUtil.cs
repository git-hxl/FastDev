using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class SampleUtil : MonoBehaviour
    {
        public string text;
        // Start is called before the first frame update
        void Start()
        {
            string md5txt = SecurityUtil.MD5Encrypt(text);
            Debug.Log("MD5：" + md5txt);

            string privateKey;
            string publicKey;
            SecurityUtil.GenerateRsaKey(out privateKey, out publicKey);

            string encryptData = SecurityUtil.RsaEncrypt(text, publicKey);

            Debug.Log("RSA加密数据：" + encryptData);

            string decryptData = SecurityUtil.RsaDecrypt(encryptData, privateKey);

            Debug.Log("RSA解密数据：" + decryptData);

            string signText = SecurityUtil.SignatureFormatter(privateKey, text);

            Debug.Log("RSA签名数据：" + signText);

            bool value = SecurityUtil.SignatureDeformatter(publicKey, text+"x", signText);

            Debug.Log("RSA验签结果：" + value);

            string aesEncryptTxt = SecurityUtil.AESEncrypt(text, "123456789xxxxxxxxxxxxxxxxx");

            Debug.Log("Aes加密数据：" + aesEncryptTxt);

            string aesTxt = SecurityUtil.AESDecrypt(aesEncryptTxt, "123456789");

            Debug.Log("Aes解密数据：" + aesTxt);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
