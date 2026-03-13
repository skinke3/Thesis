using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace CompanionAI.FSM
{
    [RequireComponent(typeof(SphereCollider))]
    public class TargetSensor : MonoBehaviour
    {
        public delegate void TargetEnterEvent(Transform target);
        public delegate void TargetExitEvent(Vector3 lastKnownPosition);

        public event TargetEnterEvent OnTargetEnter;
        public event TargetExitEvent OnTargetExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
            {
                OnTargetEnter?.Invoke(other.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Target"))
            {
                OnTargetExit?.Invoke(other.transform.position);
            }
        }


    }
}
