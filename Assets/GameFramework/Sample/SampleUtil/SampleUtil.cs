using GameFramework;
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

        bool value = SecurityUtil.SignatureDeformatter(publicKey, text, signText);

        Debug.Log("RSA验签结果：" + value);

        string aesEncryptTxt = SecurityUtil.AESEncrypt(text, "123456789xxxxxxxxxxxxxxxxx");

        Debug.Log("Aes加密数据：" + aesEncryptTxt);

        string aesTxt = SecurityUtil.AESDecrypt(aesEncryptTxt, "123456789");

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