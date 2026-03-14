using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    [SerializeField]
    Transform[] waypoints;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (Transform waypoint in waypoints)
        {
            Gizmos.DrawSphere(waypoint.position, 0.3f);
        }
    }
}
