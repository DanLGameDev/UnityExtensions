using DGP.UnityExtensions.Helpers;
using UnityEngine;

namespace DGP.UnityExtensions
{
    public static class Vector3Extensions
    {
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }
        
        public static Vector3 Add(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(vector.x + (x ?? 0f), vector.y + (y ?? 0f), vector.z + (z ?? 0f));
        }
        
        public static bool IsInRangeOf(this Vector3 vector, Vector3 target, float range)
        {
            return Vector3.SqrMagnitude(vector - target) <= range * range;
        }
        
        public static Vector3 WithRandomOffset(this Vector3 vector, float maxOffset, RandomStream randomStream = null)
        {
            return vector + new Vector3(
                randomStream?.Range(-maxOffset, maxOffset) ?? Random.Range(-maxOffset, maxOffset),
                randomStream?.Range(-maxOffset, maxOffset) ?? Random.Range(-maxOffset, maxOffset),
                randomStream?.Range(-maxOffset, maxOffset) ?? Random.Range(-maxOffset, maxOffset)
            );
        }
    }
}