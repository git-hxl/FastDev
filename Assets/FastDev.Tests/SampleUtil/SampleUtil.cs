using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleUtil : MonoBehaviour
{
    public string text;

    public GameObject TestObj;

    private Vector3 targetPos;
    private Quaternion targetDir;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
        string md5txt = Utility.Encryption.MD5Encrypt(text);
        Debug.Log("MD5：" + md5txt);

        string privateKey;
        string publicKey;
        Utility.Encryption.GenerateRsaKey(out privateKey, out publicKey);

        string encryptData = Utility.Encryption.RsaEncrypt(text, publicKey);

        Debug.Log("RSA加密数据：" + encryptData);

        string decryptData = Utility.Encryption.RsaDecrypt(encryptData, privateKey);

        Debug.Log("RSA解密数据：" + decryptData);

        string signText = Utility.Encryption.SignatureFormatter(privateKey, text);

        Debug.Log("RSA签名数据：" + signText);

        bool value = Utility.Encryption.SignatureDeformatter(publicKey, text, signText);

        Debug.Log("RSA验签结果：" + value);

        string aesEncryptTxt = Utility.Encryption.AESEncrypt(text, "123456789xxxxxxxxxxxxxxxxx");

        Debug.Log("Aes加密数据：" + aesEncryptTxt);

        string aesTxt = Utility.Encryption.AESDecrypt(aesEncryptTxt, "123456789");

        Debug.Log("Aes解密数据：" + aesTxt);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPos != Vector3.zero)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, 3f * Time.deltaTime);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, targetDir, 3f * Time.deltaTime);

            if (Camera.main.transform.position == targetPos)
            {
                targetPos = Vector3.zero;
            }
        }
    }

    [ContextMenu("TestCamerLerp")]
    public void TestCameraLerp()
    {
        Camera camera = Camera.main;

        targetPos = TestObj.transform.position;
        targetDir = TestObj.transform.rotation;
    }
}