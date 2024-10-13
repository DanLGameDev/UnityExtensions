using UnityEngine;

namespace DGP.UnityExtensions
{
    public static class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.TryGetComponent<T>(out T component))
                return component;
            
            return gameObject.AddComponent<T>();
        }
        
        public static T OrNull<T>(this T obj) where T : Object => (bool)obj ? obj : null;

        public static void EnableChildren(this GameObject gameObject) => gameObject.transform.EnableChildren();
        public static void DisableChildren(this GameObject gameObject) => gameObject.transform.DisableChildren();
    }
}