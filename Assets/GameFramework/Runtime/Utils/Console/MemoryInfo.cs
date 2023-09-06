
using System.Text;
using UnityEngine;
using UnityEngine.Profiling;

namespace GameFramework
{
    public class MemoryInfo
    {
        public new static string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("MonoUsedSize：" + UnitConvertUtil.ByteConvert(Profiler.GetMonoUsedSizeLong()));
            stringBuilder.AppendLine("MonoHeapSize：" + UnitConvertUtil.ByteConvert(Profiler.GetMonoHeapSizeLong()));
            //only development or editor work
            stringBuilder.AppendLine("GraphicsDriverUsedSize：" + UnitConvertUtil.ByteConvert(Profiler.GetAllocatedMemoryForGraphicsDriver()));

            stringBuilder.AppendLine("TotalAllocatedMemory：" + UnitConvertUtil.ByteConvert(Profiler.GetTotalAllocatedMemoryLong()));
            stringBuilder.AppendLine("TotalUnusedReservedMemory：" + UnitConvertUtil.ByteConvert(Profiler.GetTotalUnusedReservedMemoryLong()));
            stringBuilder.AppendLine("TotalReservedMemory：" + UnitConvertUtil.ByteConvert(Profiler.GetTotalReservedMemoryLong()));

            return stringBuilder.ToString();
        }
    }
}
