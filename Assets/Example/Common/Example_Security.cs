using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastDev;
public class Example_Security : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string priKey = "";
        string pubKey = "";
        SecurityUtil.GenerateRsaKey(out priKey, out pubKey);

        //支持公钥加密私钥解密
        string message = "Hello World";
        Debug.Log("原文：" + message);
        string encryptMsg = SecurityUtil.RsaEncrypt(message, pubKey);
        Debug.Log("加密：" + encryptMsg);
        string decryptMsg = SecurityUtil.RsaDecrypt(encryptMsg, priKey);
        Debug.Log("解密：" + decryptMsg);

        //支持私钥加密私钥解密
        string encryptMsg2 = SecurityUtil.RsaEncrypt(message, priKey);
        Debug.Log("加密：" + encryptMsg2);
        string decryptMsg2 = SecurityUtil.RsaDecrypt(encryptMsg2, priKey);
        Debug.Log("解密：" + decryptMsg2);

        //不支持公钥解密
    }

    // Update is called once per frame
    void Update()
    {

    }
}
