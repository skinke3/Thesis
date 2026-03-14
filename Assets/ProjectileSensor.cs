using UnityEngine;
using Unity.Behavior;

public class ProjectileSensor : MonoBehaviour
{
    [SerializeField]
    private GameObject companion;
    [SerializeField]
    private GameObject dodgeLocation;
    [SerializeField]
    private BehaviorGraphAgent behaviorAgent;
    [SerializeField]
    private float distance = 5f;

    private void Awake()
    {
        behaviorAgent.SetVariableValue("DodgeLocation", dodgeLocation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Vector3 direction = other.transform.position - companion.transform.position;
            dodgeLocation.transform.position = companion.transform.position;
            dodgeLocation.transform.rotation = companion.transform.rotation;
            dodgeLocation.transform.Translate(Vector3.forward *  distance);
            behaviorAgent.SetVariableValue("DodgeLocation", dodgeLocation);
        }
    }

}
