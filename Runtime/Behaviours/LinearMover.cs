using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace DGP.UnityExtensions.Behaviours
{
    /// <summary>
    /// Moves a target GameObject in a specified direction at a specified speed.
    /// </summary>
    public class LinearMover : MonoBehaviour
    {
        
        [SerializeField] private Vector3 direction = Vector3.right;
        [SerializeField] private float speed = 1f;
        
        [SerializeField] private bool destroyAfterDelay = false;
        [SerializeField]
        #if ODIN_INSPECTOR
        [ShowIf(nameof(destroyAfterDelay))]
        #endif
        private float destroyDelaySeconds = 5f;
        
        private float _elapsedTime = 0f;
        
        private void Update()
        {
            transform.position += direction.normalized * (speed * Time.deltaTime);

            if (!destroyAfterDelay) 
                return;
            
            _elapsedTime += Time.deltaTime;
                
            if (_elapsedTime >= destroyDelaySeconds)
                Destroy(gameObject);
        }
    }
}