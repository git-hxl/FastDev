using UnityEngine;
using UnityEngine.Profiling;

namespace FastDev
{
    class MemoryWindow : IWindow
    {
        private Vector2 scrollPos;
        public void Draw()
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos, "box");

            GUILayout.Label("MonoUsedSize：" + UnitConvertUtil.ByteConvert(Profiler.GetMonoUsedSizeLong()));
            GUILayout.Label("MonoHeapSize：" + UnitConvertUtil.ByteConvert(Profiler.GetMonoHeapSizeLong()));
            //only development or editor work
            GUILayout.Label("GraphicsDriverUsedSize：" + UnitConvertUtil.ByteConvert(Profiler.GetAllocatedMemoryForGraphicsDriver()));

            GUILayout.Label("TotalAllocatedMemory：" + UnitConvertUtil.ByteConvert(Profiler.GetTotalAllocatedMemoryLong()));
            GUILayout.Label("TotalUnusedReservedMemory：" + UnitConvertUtil.ByteConvert(Profiler.GetTotalUnusedReservedMemoryLong()));
            GUILayout.Label("TotalReservedMemory：" + UnitConvertUtil.ByteConvert(Profiler.GetTotalReservedMemoryLong()));
            GUILayout.EndScrollView();
        }
    }
}
