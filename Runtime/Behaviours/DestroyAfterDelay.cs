using UnityEngine;

namespace DGP.UnityExtensions.Behaviours
{
    /// <summary>
    /// Destroys the GameObject this component is attached to after a specified delay in seconds.
    /// </summary>
    public class DestroyAfterDelay : MonoBehaviour
    {
        [SerializeField] private float delaySeconds = 10f;
        
        private float _elapsedTime;

        private void Update()
        {
            _elapsedTime += Time.deltaTime;
        
            if (_elapsedTime >= delaySeconds)
                DestroySelf();
        }
    
        private void DestroySelf() => Destroy(gameObject);
    }
}