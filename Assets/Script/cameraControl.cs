using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{

    public GameObject player;
    // public GameObject targetObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // targetObject = GameObject.FindGameObjectWithTag("enemy");
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        // Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        // // Kiểm tra xem game object có nằm trong tầm nhìn của camera không
        // bool isVisible = GeometryUtility.TestPlanesAABB(planes, targetObject.GetComponent<Renderer>().bounds);
        // GameObject eHealBar = Instantiate(Resources.Load("eHealbarUI"), GameObject.Find("PUI").transform) as GameObject;
        // if (isVisible)
        // {

        //     eHealBar.transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + 1.5f, targetObject.transform.position.z);
        // }
        // else
        // {
        //     Destroy(eHealBar);
        // }
    }
}
