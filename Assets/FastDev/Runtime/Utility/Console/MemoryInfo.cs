
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
            stringBuilder.AppendLine("MonoUsedSize：" + Utility.Converter.ByteConvert(Profiler.GetMonoUsedSizeLong()));
            stringBuilder.AppendLine("MonoHeapSize：" + Utility.Converter.ByteConvert(Profiler.GetMonoHeapSizeLong()));
            //only development or editor work
            stringBuilder.AppendLine("GraphicsDriverUsedSize：" + Utility.Converter.ByteConvert(Profiler.GetAllocatedMemoryForGraphicsDriver()));

            stringBuilder.AppendLine("TotalAllocatedMemory：" + Utility.Converter.ByteConvert(Profiler.GetTotalAllocatedMemoryLong()));
            stringBuilder.AppendLine("TotalUnusedReservedMemory：" + Utility.Converter.ByteConvert(Profiler.GetTotalUnusedReservedMemoryLong()));
            stringBuilder.AppendLine("TotalReservedMemory：" + Utility.Converter.ByteConvert(Profiler.GetTotalReservedMemoryLong()));

            return stringBuilder.ToString();
        }
    }
}
