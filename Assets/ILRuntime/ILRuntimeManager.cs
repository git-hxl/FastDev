using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Bigger;
public class ILRuntimeManager : MonoBehaviour
{
    public static AppDomain appdomain;

    System.IO.MemoryStream fs;
    System.IO.MemoryStream p;

    private void Start()
    {
        StartCoroutine(LoadHotFixAssembly());
    }

    private IEnumerator LoadHotFixAssembly()
    {
        //首先实例化ILRuntime的AppDomain，AppDomain是一个应用程序域，每个AppDomain都是一个独立的沙盒
        appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
        //正常项目中应该是自行从其他地方下载dll，或者打包在AssetBundle中读取，平时开发以及为了演示方便直接从StreammingAssets中读取，
        //正式发布的时候需要大家自行从其他地方读取dll

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //这个DLL文件是直接编译HotFix_Project.sln生成的，已经在项目中设置好输出目录为StreamingAssets，在VS里直接编译即可生成到对应目录，无需手动拷贝
        //工程目录在Assets\Samples\ILRuntime\1.6\Demo\HotFix_Project~
        //以下加载写法只为演示，并没有处理在编辑器切换到Android平台的读取，需要自行修改
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(Application.streamingAssetsPath + "/HotFixProject.dll");
        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.isHttpError || unityWebRequest.isNetworkError)
        {
            Debug.LogError(unityWebRequest.error);
            yield break;
        }
        byte[] dll = unityWebRequest.downloadHandler.data;
        unityWebRequest.Dispose();

        //PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
        unityWebRequest = UnityWebRequest.Get(Application.streamingAssetsPath + "/HotFixProject.pdb");
        yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.isHttpError || unityWebRequest.isNetworkError)
        {
            Debug.LogError(unityWebRequest.error);
            yield break;
        }
        byte[] pdb = unityWebRequest.downloadHandler.data;
        fs = new MemoryStream(dll);
        p = new MemoryStream(pdb);
        appdomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());

        InitializeILRuntime();
        EventManager.Instance.Dispatch(MsgID.OnHotFixInitSuccess, null);
    }

    void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //这里做一些ILRuntime的注册
        appdomain.DelegateManager.RegisterMethodDelegate<string>();

        appdomain.DelegateManager.RegisterDelegateConvertor<TestDelegate1>((action) =>
        {
            //转换器的目的是把Action或者Func转换成正确的类型，这里则是把Action<int>转换成TestDelegateMethod
            return new TestDelegate1((a) =>
            {
                //调用委托实例
                ((System.Action<string>)action)(a);
            });
        });
    }
}
