using UnityEngine;

namespace Helpers
{
    internal static class TransformHelper
    {
        public static void DestroyAllChildren(this Transform transform)
        {
            for (var i = 0; i < transform.childCount; i++) Object.Destroy(transform.GetChild(i).gameObject);
        }
    }
}