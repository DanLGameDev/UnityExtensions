using System.Collections.Generic;
using UnityEngine;

namespace DGP.UnityExtensions
{
    public static class TransformExtensions
    {
        
        public static void Reset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        
        public static IEnumerable<Transform> Children(this Transform parent)
        {
            foreach (Transform child in parent)
                yield return child;
        }

        public static void EnableChildren(this Transform parent)
        {
            parent.PerformActionOnChildren(child => child.gameObject.SetActive(true));
        }
        
        public static void DisableChildren(this Transform parent)
        {
            parent.PerformActionOnChildren(child => child.gameObject.SetActive(false));
        }
        

        private static void PerformActionOnChildren(this Transform parent, System.Action<Transform> action)
        {
            for (var i = parent.childCount - 1; i >= 0; i--)
                action(parent.GetChild(i));
        }
    }
}