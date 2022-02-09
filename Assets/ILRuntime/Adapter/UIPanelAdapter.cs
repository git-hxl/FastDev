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
                return typeof(FastDev.UIPanel);
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

        public class Adapter : FastDev.UIPanel, CrossBindingAdaptorType
        {
            CrossBindingMethodInfo mOpenByAnima_0 = new CrossBindingMethodInfo("OpenByAnima");
            CrossBindingMethodInfo mCloseByAnima_1 = new CrossBindingMethodInfo("CloseByAnima");
            CrossBindingMethodInfo mOpen_2 = new CrossBindingMethodInfo("Open");
            CrossBindingMethodInfo mClose_3 = new CrossBindingMethodInfo("Close");
            CrossBindingMethodInfo mStart = new CrossBindingMethodInfo("Start");
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

            private void Start()
            {
                mStart.Invoke(this.instance);
            }

            public ILTypeInstance ILInstance { get { return instance; } }

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
                if (mOpen_2.CheckShouldInvokeBase(this.instance))
                    base.Open();
                else
                    mOpen_2.Invoke(this.instance);
            }

            public override void Close()
            {
                if (mClose_3.CheckShouldInvokeBase(this.instance))
                    base.Close();
                else
                    mClose_3.Invoke(this.instance);
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

