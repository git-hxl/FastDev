using FastDev;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using UnityEngine;
using FastDev.UI;
public class AddHotFixUIClass : MonoBehaviour
{
    public string fullClassName;
    private void Awake()
    {
        var adapter = gameObject.AddComponent<UIPanelAdapter.Adapter>();
        IType itype = ILRuntimeManager.Instance.appdomain.GetType(fullClassName);
        ILTypeInstance iLTypeInstance = new ILTypeInstance(itype as ILType, false);
        iLTypeInstance.CLRInstance = adapter;
        adapter.Init(ILRuntimeManager.Instance.appdomain, iLTypeInstance);
    }
}