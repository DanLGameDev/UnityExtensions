using UnityEngine;

namespace DGP.UnityExtensions.Behaviours
{
    [RequireComponent(typeof(MeshFilter))]
    public class ShowNormals : MonoBehaviour
    {
        [SerializeField] private float normalLength = 0.5f;
        [SerializeField] private MeshFilter meshFilter;

        private void Awake()
        {
            if (meshFilter == null)
                meshFilter = GetComponent<MeshFilter>();
        }

        #if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (meshFilter == null) return;
        
            var mesh = meshFilter.sharedMesh;
            if (mesh == null) {
                Debug.LogWarning("ShowNormals: No mesh found on MeshFilter.", this);
                return;
            }

            Gizmos.color = Color.yellow;
            var vertices = mesh.vertices;
            var normals = mesh.normals;
        
            for (var i = 0; i < vertices.Length; i++) {
                var worldPos = transform.TransformPoint(vertices[i]);
                var worldNormal = transform.TransformDirection(normals[i]);
                
                Gizmos.DrawRay(worldPos, worldNormal * normalLength);
            }
        }
        #endif
    }
}