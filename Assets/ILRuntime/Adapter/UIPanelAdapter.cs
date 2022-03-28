using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

namespace FastDev
{   
    public class UIPanelAdapter : CrossBindingAdaptor
    {
        public override Type BaseCLRType
        {
            get
            {
                return typeof(FastDev.UI.UIPanel);
            }
        }

        public override Type AdaptorType
        {
            get
            {
                return typeof(Adapter);
            }
        }

        public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            return new Adapter(appdomain, instance);
        }

        public class Adapter : FastDev.UI.UIPanel, CrossBindingAdaptorType
        {
            CrossBindingFunctionInfo<System.Int32> mget_index_0 = new CrossBindingFunctionInfo<System.Int32>("get_index");
            CrossBindingFunctionInfo<System.String> mget_panelName_1 = new CrossBindingFunctionInfo<System.String>("get_panelName");
            CrossBindingFunctionInfo<System.String> mget_assetPath_2 = new CrossBindingFunctionInfo<System.String>("get_assetPath");
            CrossBindingMethodInfo mOnClose_3 = new CrossBindingMethodInfo("OnClose");
            CrossBindingMethodInfo<System.String> mOnLoad_4 = new CrossBindingMethodInfo<System.String>("OnLoad");
            CrossBindingMethodInfo mOnOpen_5 = new CrossBindingMethodInfo("OnOpen");

            bool isInvokingToString;
            ILTypeInstance instance;
            ILRuntime.Runtime.Enviorment.AppDomain appdomain;

            public Adapter()
            {

            }

            public Adapter(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
            {
                this.appdomain = appdomain;
                this.instance = instance;
            }

            public void Init(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
            {
                this.appdomain = appdomain;
                this.instance = instance;
            }

            public ILTypeInstance ILInstance { get { return instance; } }


            public override void OnClose()
            {
                mOnClose_3.Invoke(this.instance);
            }

            public override void OnLoad(System.String assetPath)
            {
                mOnLoad_4.Invoke(this.instance, assetPath);
            }

            public override void OnOpen()
            {
                mOnOpen_5.Invoke(this.instance);
            }

            public override System.Int32 index
            {
            get
            {
                return mget_index_0.Invoke(this.instance);

            }
            }

            public override System.String panelName
            {
            get
            {
                return mget_panelName_1.Invoke(this.instance);

            }
            }

            public override System.String assetPath
            {
            get
            {
                return mget_assetPath_2.Invoke(this.instance);

            }
            }

            public override string ToString()
            {
                IMethod m = appdomain.ObjectType.GetMethod("ToString", 0);
                m = instance.Type.GetVirtualMethod(m);
                if (m == null || m is ILMethod)
                {
                    if (!isInvokingToString)
                    {
                        isInvokingToString = true;
                        string res = instance.ToString();
                        isInvokingToString = false;
                        return res;
                    }
                    else
                        return instance.Type.FullName;
                }
                else
                    return instance.Type.FullName;
            }
        }
    }
}

