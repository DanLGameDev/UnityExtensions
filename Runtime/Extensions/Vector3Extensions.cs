using UnityEngine;

namespace DGP.UnityExtensions
{
    public static class Vector3Extensions
    {
        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
        }
        
        public static Vector3 Add(this Vector3 vector, float x = default(float), float y = default(float), float z = default(float))
        {
            return new Vector3(vector.x + x, vector.y + y, vector.z + z);
        }
        
    }
}