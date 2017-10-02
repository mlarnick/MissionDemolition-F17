using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {
    static public FollowCam S;  // a FollowCam Singleton

    // Fields set in the Unity Inspector Panel
    public float easing = .05f;
    public Vector2 minXY;
    public bool _________________________;

    // Fields set dynamically
    public GameObject poi;  // The point of interest
    public float camZ;      // The desired z pos of the camera

    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 destination;
        
        // I fthere is no poi, reutnr to P:[0, 0, 0]
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
                // If it is sleeping
                if (poi.GetComponent<Rigidbody>().IsSleeping())
                {
                    // return to default view
                    poi = null;
                   // MissionDemolition.SwitchView("Both");
                    // in the next update
                    return;
                }
            }
        }

        // Limit the X and Y to minimum value
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        // Interpolate from the current Camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);

        // Retain a destination.z of camZ
        destination.z = camZ;

        // Set the camera to the destination
        transform.position = destination;

        // Set the orthographicSize of the Camera to keep Ground in view
        this.GetComponent<Camera>().orthographicSize = destination.y + 10;
    }
}
