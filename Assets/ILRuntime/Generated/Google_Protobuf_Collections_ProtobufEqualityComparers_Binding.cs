using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;

namespace ILRuntime.Runtime.Generated
{
    unsafe class Google_Protobuf_Collections_ProtobufEqualityComparers_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            MethodBase method;
            Type[] args;
            Type type = typeof(Google.Protobuf.Collections.ProtobufEqualityComparers);
            args = new Type[]{};
            method = type.GetMethod("get_BitwiseSingleEqualityComparer", flag, null, args, null);
            app.RegisterCLRMethodRedirection(method, get_BitwiseSingleEqualityComparer_0);


        }


        static StackObject* get_BitwiseSingleEqualityComparer_0(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* __ret = ILIntepreter.Minus(__esp, 0);


            var result_of_this_method = Google.Protobuf.Collections.ProtobufEqualityComparers.BitwiseSingleEqualityComparer;

            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }



    }
}
