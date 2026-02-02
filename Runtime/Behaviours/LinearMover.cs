using UnityEngine;

namespace DGP.UnityExtensions.Behaviours
{
    /// <summary>
    /// Moves a target GameObject in a specified direction at a specified speed.
    /// </summary>
    public class LinearMover : MonoBehaviour
    {
        
        [SerializeField] private Vector3 direction = Vector3.right;
        [SerializeField] private float speed = 1f;
        [SerializeField] private float destroyDelaySeconds = 5f;
        
        private void Update()
        {
            transform.position += direction.normalized * (speed * Time.deltaTime);
        }
    }
}