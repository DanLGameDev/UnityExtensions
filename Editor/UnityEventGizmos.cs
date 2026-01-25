using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace DGP.UnityExtensions.Editor
{
    public static class UnityEventGizmos
    {
        public static void RenderConnection(Vector3 origin, Vector3 target, Color color, int width = 2)
        {
            UnityEngine.Gizmos.color = color;
            
            
            Vector3 direction = (target - origin).normalized;
                    
            //keep only 2 largest components
            var absDirection = new Vector3(Mathf.Abs(direction.x), Mathf.Abs(direction.y), Mathf.Abs(direction.z));
            var maxComponent = Mathf.Max(absDirection.x, absDirection.y, absDirection.z);
            if (Mathf.Approximately(absDirection.x, maxComponent))
                direction = new Vector3(direction.x, 0, 0);
            else if (Mathf.Approximately(absDirection.y, maxComponent))
                direction = new Vector3(0, direction.y, 0);
            else if (Mathf.Approximately(absDirection.z, maxComponent))
                direction = new Vector3(0, 0, direction.z);
            
            var distanceAlongDirection = Vector3.Distance(origin, target) * 0.5f;
            var originOffset = origin + direction * distanceAlongDirection;
            var targetOffset = target + -direction * distanceAlongDirection;

            origin = origin + (direction * 0.125f);

            UnityEngine.Gizmos.color = color;
            UnityEngine.Gizmos.DrawCube(origin,Vector3.one * .04f);
            UnityEngine.Gizmos.DrawSphere(target, 0.04f);
            
#if UNITY_EDITOR
            Handles.DrawBezier(origin, target, originOffset, targetOffset, color, null, width);
#endif
        }
        
        public static void DrawConnectionTo<T>(Vector3 origin,  UnityEvent<T> unityEvent, Color color)
        {
            for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
            {
                var method = unityEvent.GetPersistentTarget(i);
                var targetGo = method as Component;

                if (targetGo != null)
                    RenderConnection(targetGo.transform.position, origin, color);
            }
        }
        
        public static void DrawConnectionTo(Vector3 origin, UnityEvent unityEvent, Color color)
        {
            for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
            {
                var method = unityEvent.GetPersistentTarget(i);
                var targetGo = method as Component;

                if (targetGo != null)
                    RenderConnection(targetGo.transform.position, origin, color);
            }
        }
    }
}