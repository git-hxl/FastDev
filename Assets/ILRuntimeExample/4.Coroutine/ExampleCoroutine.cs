
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using UnityEngine;

public class ExampleCoroutine : MonoBehaviour
{
    void Start()
    {
        var adapter = gameObject.AddComponent<MonoBehaviourAdapter.Adapter>();
        IType itype = ILRuntimeManager.Instance.appdomain.GetType("Hotfix.Coroutine");
        ILTypeInstance iLTypeInstance = new ILTypeInstance(itype as ILType, false);
        iLTypeInstance.CLRInstance = adapter;
        adapter.ILInstance = (iLTypeInstance);
        adapter.AppDomain = (ILRuntimeManager.Instance.appdomain);
    }
}
