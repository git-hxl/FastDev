
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public interface IAgent
    {
        List<IAction> AIActions { get; }
        void AddAction(IAction action);
    }
}
