using UnityEngine;

namespace CompanionAI.FSM
{
    [RequireComponent(typeof(SphereCollider))]
    public class ProjectileSensor : MonoBehaviour
    {
        public delegate void ProjectileEnterEvent(Transform projectile);
        public delegate void ProjectileExitEvent(Vector3 lastKnownPosition);

        public event ProjectileEnterEvent OnProjectileEnter;
        public event ProjectileExitEvent OnProjectileExit;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Projectile projectile))
            {
                OnProjectileEnter?.Invoke(projectile.transform);
                Debug.Log("Projectile Entered");
            }
        }

        private void OnTriggerExit(Collider other) 
        { 
            if (other.TryGetComponent(out Projectile projectile))
            {
                OnProjectileExit?.Invoke(other.transform.position);
                Debug.Log("Projectile Entered");
            }
        }
    }
}
