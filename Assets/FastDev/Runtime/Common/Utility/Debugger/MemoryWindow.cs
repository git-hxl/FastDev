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

            GUILayout.Label("MonoUsedSize：" + UnitConvert.ByteConvert(Profiler.GetMonoUsedSizeLong()));
            GUILayout.Label("MonoHeapSize：" + UnitConvert.ByteConvert(Profiler.GetMonoHeapSizeLong()));
            //only development or editor work
            GUILayout.Label("GraphicsDriverUsedSize：" + UnitConvert.ByteConvert(Profiler.GetAllocatedMemoryForGraphicsDriver()));

            GUILayout.Label("TotalAllocatedMemory：" + UnitConvert.ByteConvert(Profiler.GetTotalAllocatedMemoryLong()));
            GUILayout.Label("TotalUnusedReservedMemory：" + UnitConvert.ByteConvert(Profiler.GetTotalUnusedReservedMemoryLong()));
            GUILayout.Label("TotalReservedMemory：" + UnitConvert.ByteConvert(Profiler.GetTotalReservedMemoryLong()));
            GUILayout.EndScrollView();
        }
    }
}
