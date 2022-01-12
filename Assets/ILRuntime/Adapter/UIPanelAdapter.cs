using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

namespace Bigger
{
    public class UIPanelAdapter : CrossBindingAdaptor
    {
        public override Type BaseCLRType
        {
            get
            {
                return typeof(Bigger.UIPanel);
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

        public class Adapter : Bigger.UIPanel, CrossBindingAdaptorType
        {
            CrossBindingMethodInfo mOpenByAnima_0 = new CrossBindingMethodInfo("OpenByAnima");
            CrossBindingMethodInfo mCloseByAnima_1 = new CrossBindingMethodInfo("CloseByAnima");
            CrossBindingMethodInfo mStart_2 = new CrossBindingMethodInfo("Start");
            CrossBindingMethodInfo mOpen_3 = new CrossBindingMethodInfo("Open");
            CrossBindingMethodInfo mClose_4 = new CrossBindingMethodInfo("Close");

            bool isInvokingToString;
            private ILTypeInstance instance;
            private ILRuntime.Runtime.Enviorment.AppDomain appdomain;

            public Adapter()
            {

            }

            public Adapter(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
            {
                this.appdomain = appdomain;
                this.instance = instance;
            }

            public ILTypeInstance ILInstance { get { return instance; } set { instance = value; } }

            public ILRuntime.Runtime.Enviorment.AppDomain AppDomain { get { return appdomain; } set { appdomain = value; } }

            public void Awake()
            {
                if (this.instance != null)
                    mStart_2.Invoke(this.instance);
            }

            public override void OpenByAnima()
            {
                if (mOpenByAnima_0.CheckShouldInvokeBase(this.instance))
                    base.OpenByAnima();
                else
                    mOpenByAnima_0.Invoke(this.instance);
            }

            public override void CloseByAnima()
            {
                if (mCloseByAnima_1.CheckShouldInvokeBase(this.instance))
                    base.CloseByAnima();
                else
                    mCloseByAnima_1.Invoke(this.instance);
            }

            public override void Open()
            {
                if (mOpen_3.CheckShouldInvokeBase(this.instance))
                    base.Open();
                else
                    mOpen_3.Invoke(this.instance);
            }

            public override void Close()
            {
                if (mClose_4.CheckShouldInvokeBase(this.instance))
                    base.Close();
                else
                    mClose_4.Invoke(this.instance);
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

