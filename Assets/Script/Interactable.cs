
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public float radius1 = 5f;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius1);
    }

}
