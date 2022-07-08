
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public interface IGoapAgent
    {
        List<IGoapAction> AIActions { get; }
        void AddAction(IGoapAction action);
    }
}
