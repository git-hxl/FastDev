using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public interface IMsgManager
    {
        void Enqueue(int msgID, params object[] parameters);
        void Register(int msgID, Action<object[]> action);
        void UnRegister(int msgID, Action<object[]> action);
        void Dispatch(int msgID, params object[] parameters);
    }
}
