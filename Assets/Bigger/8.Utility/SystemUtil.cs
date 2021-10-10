// using System.Collections;
// using System.Collections.Generic;
// using System.Runtime.InteropServices;
// using UnityEngine;

// public class SystemUtil
// {
//     /// <summary>
//     /// 保持屏幕常亮
//     /// </summary>
//     /// <param name="value"></param>
//     public static void KeepScreenOn(bool value)
//     {
//         if (value)
//         {
// #if UNITY_EDITOR || UNITY_IOS
//             Screen.sleepTimeout = SleepTimeout.NeverSleep;
// #else
//             SetAndroidKeepScreenOn(true);
// #endif
//             Debug.Log("打开屏幕常亮");
//         }
//         else
//         {
// #if UNITY_EDITOR || UNITY_IOS
//             Screen.sleepTimeout = SleepTimeout.SystemSetting;
// #else
//             SetAndroidKeepScreenOn(false);
// #endif
//             Debug.Log("关闭屏幕常亮");
//         }
//     }


//     private static void SetAndroidKeepScreenOn(bool keepScreenOn)
//     {
//         AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//         AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
//         if (keepScreenOn)
//         {
//             jo.Call("runOnUiThread", new AndroidJavaRunnable(RunnableAndroidKeepScreenOn));
//         }
//         else
//         {
//             jo.Call("runOnUiThread", new AndroidJavaRunnable(RunnableAndroidKeepScreenOff));
//         }

//     }

//     private static void RunnableAndroidKeepScreenOn()
//     {
//         AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//         AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
//         AndroidJavaObject jPlayer = jo.Get<AndroidJavaObject>("mUnityPlayer");
//         jPlayer.Call("setKeepScreenOn", true);
//     }

//     private static void RunnableAndroidKeepScreenOff()
//     {
//         AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//         AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
//         AndroidJavaObject jPlayer = jo.Get<AndroidJavaObject>("mUnityPlayer");
//         jPlayer.Call("setKeepScreenOn", false);
//     }

//     /// <summary>
//     /// 复制文本
//     /// </summary>
//     /// <param name="txt"></param>
//     public static void CopyText(string txt)
//     {
//         Debug.Log("Copy text：" + txt);
// #if UNITY_EDITOR
// #elif UNITY_ANDROID
//         AndroidCopy(txt);
// #elif UNITY_IOS
//         copyText(txt);
// #endif
//     }

// #if UNITY_IOS
//     [DllImport("__Internal")]
//     private static extern void copyText(string txt);
// #endif

//     private static void AndroidCopy(string txt)
//     {
//         AndroidJavaObject javaClipboardHelper = new AndroidJavaObject("com.unity3d.player.AndroidTool");
//         AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//         AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
//         if (jo != null)
//         {
//             javaClipboardHelper.Call("copyTextToClipboard", jo, txt);
//         }
//     }

//     /// <summary>
//     /// 安卓系统toast弹窗
//     /// </summary>
//     /// <param name="message"></param>
//     public static void ShowAndroidToastMessage(string message)
//     {
//         AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//         AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
//         if (unityActivity != null)
//         {
//             AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
//             unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
//             {
//                 AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, toastClass.GetStatic<int>("LENGTH_SHORT"));
//                 toastObject.Call("show");
//             }));
//         }
//     }

//     /// <summary>
//     /// 打开状态栏
//     /// </summary>
//     public static void openAndroidStatusBar()
//     {
//         AndroidJavaObject unityActivity = GetCurrentActivity();
//         unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
//         {
//             Call("com.unity3d.player.AndroidTool", "openStatusBar", unityActivity);
//         }));
        
//     }
//     /// <summary>
//     /// 关闭状态栏
//     /// </summary>
//     public static void closeAndroidStatusBar()
//     {
//         AndroidJavaObject unityActivity = GetCurrentActivity();
//         unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
//         {
//             Call("com.unity3d.player.AndroidTool", "closeStatusBar", unityActivity);
//         }));
//     }

//     /// <summary>
//     /// 设置浅色状态栏（黑字）
//     /// </summary>
//     public static void setLightStatusBar()
//     {
//         AndroidJavaObject unityActivity = GetCurrentActivity();
//         unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
//         {
//             Call("com.unity3d.player.AndroidTool", "setLightStatusBar", unityActivity);
//         }));
//     }

//     /// <summary>
//     /// 设置深色状态栏（白字）
//     /// </summary>
//     public static void setDarkStatusBar()
//     {
//         AndroidJavaObject unityActivity = GetCurrentActivity();
//         unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
//         {
//             Call("com.unity3d.player.AndroidTool", "setDarkStatusBar", unityActivity);
//         }));
//     }

//     /// <summary>
//     /// 获取状态栏高度
//     /// </summary>
//     /// <returns></returns>
//     public static int getStatusBarHeight()
//     {
//         return Call<int>("com.unity3d.player.AndroidTool", "getStatusBarHeight", GetCurrentActivity());
//     }

//     private static void Call(string className, string method, params object[] objs)
//     {
//         AndroidJavaObject jo = new AndroidJavaObject(className);

//         jo.Call(method, objs);
//     }

//     private static T Call<T>(string className, string method, params object[] objs)
//     {
//         AndroidJavaObject jo = new AndroidJavaObject(className);

//         return jo.Call<T>(method, objs);
//     }


//     public static int RUNTIME_PERMISSIONS_MIN_SDK_LEVEL = 23;
//     public static string ANDROID_PERMISSION_RECORD_AUDIO = "android.permission.RECORD_AUDIO";
//     public static string ANDROID_PERMISSION_CAMERA = "android.permission.CAMERA";
//     public static string ANDROID_PERMISSION_WRITE_EXTERNAL_STORAGE = "android.permission.WRITE_EXTERNAL_STORAGE";

//     public static int GetSDKLevel()
//     {
//         var clazz = AndroidJNI.FindClass("android/os/Build$VERSION");
//         var fieldID = AndroidJNI.GetStaticFieldID(clazz, "SDK_INT", "I");
//         var sdkLevel = AndroidJNI.GetStaticIntField(clazz, fieldID);
//         return sdkLevel;
//     }

//     public static AndroidJavaObject GetCurrentActivity()
//     {
//         return new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
//     }

//     public static bool RuntimePermissionGranted(string permission)
//     {
//         if (GetSDKLevel() < RUNTIME_PERMISSIONS_MIN_SDK_LEVEL)
//         {
//             return true;
//         }

//         return GetCurrentActivity().Call<int>("checkSelfPermission", new object[] { permission }) == 0;
//     }

//     public static bool RuntimeShouldShowRequestPermissionRationale(string permission)
//     {
//         if (GetSDKLevel() < RUNTIME_PERMISSIONS_MIN_SDK_LEVEL)
//         {
//             return false;
//         }

//         return GetCurrentActivity().Call<bool>("shouldShowRequestPermissionRationale", new object[] { permission });
//     }

//     public static void RequestPermissions(string permission) {
//         if (GetSDKLevel() < RUNTIME_PERMISSIONS_MIN_SDK_LEVEL)
//         {
//             return;
//         }

//         GetCurrentActivity().Call("requestPermissions", new string[] { permission }, 5263);
//     }



//     public static int GetAndroidTopInset()
//     {
//         if (GetSDKLevel() < 23)
//         {
//             return 40;
//         }

//         //mUnityPlayer.getRootWindowInsets().getStableInsetTop();


//         AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

//         AndroidJavaObject jo = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
//         AndroidJavaObject jPlayer = jo.Get<AndroidJavaObject>("mUnityPlayer");
//         AndroidJavaObject jWindowInsets = jPlayer.Call<AndroidJavaObject>("getRootWindowInsets");
//         return jWindowInsets.Call<int>("getStableInsetTop");
//     }

// #if UNITY_IOS
//     [DllImport("__Internal")]
//     private static extern bool isNotchScreen();
// #endif

//     public static bool IsNotchScreen()
//     {
// #if  UNITY_EDITOR
//         return false;
// #elif UNITY_IOS
//         return isNotchScreen();
// #elif UNITY_ANDROID
//         return Call<bool>("com.unity3d.player.AndroidTool", "isNotchScreen", GetCurrentActivity());
// #endif
//     }

//     #if UNITY_IOS
//      [DllImport("__Internal")]
//     public static extern bool isInstalledApp(string app);
//     #endif
// }