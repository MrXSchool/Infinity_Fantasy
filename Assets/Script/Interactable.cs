
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float detectedZone = 4f;
    public float near = 3f;
    public float far = 5f;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, near);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectedZone);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, far);
    }

}
