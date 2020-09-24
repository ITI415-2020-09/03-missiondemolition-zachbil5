using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // Start is called before the first frame update
    static public FollowCam S; // a FollowCam Singleton
                               // fields set in the Unity Inspector pane
    public bool _____________________________;
    // fields set dynamically
    public GameObject poi; // The point of interest
    public float camZ; // The desired Z pos of the camera
    public float easing = 0.05f;
    public Vector2 minXY;
    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if there's only one line following an if, it doesn't need braces
        // return if there is no poi
        //                         // Get the position of the poi
        //Vector3 destination = poi.transform.position;
        //destination.x = Mathf.Max(minXY.x, destination.x);
        //destination.y = Mathf.Max(minXY.y, destination.y);
        //destination = Vector3.Lerp(transform.position, destination, easing);
        //// Retain a destination.z of camZ
        //destination.z = camZ;
        //// Set the camera to the destination
        //transform.position = destination;
        Vector3 destination;
        // If there is no poi, return to P:[0,0,0]
        if (poi == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            // Get the position of the poi
            destination = poi.transform.position;
            // If poi is a Projectile, check to see if it's at rest
            if (poi.tag == "Projectile")
            {
                // if it is sleeping (that is, not moving)
                if (poi.GetComponent<Rigidbody>().IsSleeping())
                {
                    // return to default view
                    poi = null;
                    // in the next update
                    return;
                }
            }
        }
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        // Retain a destination.z of camZ
        destination.z = camZ;
        // Set the camera to the destination
        transform.position = destination;
        this.GetComponent<Camera>().orthographicSize = destination.y + 10;
    }
}
