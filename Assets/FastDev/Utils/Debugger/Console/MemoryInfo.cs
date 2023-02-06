
using System.Text;
using UnityEngine;
using UnityEngine.Profiling;

namespace FastDev
{
    public class MemoryInfo
    {
        public new static string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("MonoUsedSize£º" + UnitConvertUtil.ByteConvert(Profiler.GetMonoUsedSizeLong()));
            stringBuilder.AppendLine("MonoHeapSize£º" + UnitConvertUtil.ByteConvert(Profiler.GetMonoHeapSizeLong()));
            //only development or editor work
            stringBuilder.AppendLine("GraphicsDriverUsedSize£º" + UnitConvertUtil.ByteConvert(Profiler.GetAllocatedMemoryForGraphicsDriver()));

            stringBuilder.AppendLine("TotalAllocatedMemory£º" + UnitConvertUtil.ByteConvert(Profiler.GetTotalAllocatedMemoryLong()));
            stringBuilder.AppendLine("TotalUnusedReservedMemory£º" + UnitConvertUtil.ByteConvert(Profiler.GetTotalUnusedReservedMemoryLong()));
            stringBuilder.AppendLine("TotalReservedMemory£º" + UnitConvertUtil.ByteConvert(Profiler.GetTotalReservedMemoryLong()));

            return stringBuilder.ToString();
        }
    }
}
