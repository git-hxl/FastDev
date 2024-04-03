using UnityEngine;

namespace FastDev
{
    public static partial class Utility
    {
        public static class Transform
        {
            /// <summary>
            /// 获取节点路径（root/gameobject）
            /// </summary>
            /// <param name="transform"></param>
            /// <param name="pattern"></param>
            /// <returns></returns>
            public static string GetRoute(UnityEngine.Transform transform, string pattern = "/")
            {
                var result = transform.name;
                var parent = transform.parent;
                while (parent != null)
                {
                    result = $"{parent.name}{pattern}{result}";
                    parent = parent.parent;
                }
                return result;
            }

            public static string GetRouteNoRoot(UnityEngine.Transform transform, string pattern = "/")
            {
                var result = transform.name;
                var parent = transform.parent;
                while (parent != null && parent.parent != null)
                {
                    result = $"{parent.name}{pattern}{result}";
                    parent = parent.parent;
                }
                return result;
            }
        }
    }

}
