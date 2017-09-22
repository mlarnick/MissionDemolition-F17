using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    static public FollowCam S;  // a FollowCam Singleton

    // fields set in the Unity pane
    public bool ________________________;

    // fields set dynamically
    public GameObject poi;  // The point of interest
    public float camZ;      // The desired Z pos of the camera

    void Awake()
    {
        S = this;
        camZ = this.transform.position.z;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // if there's only one line following an if, it doesn't need braces
        if (poi == null) return;    // return if there is no poi;

        // Get the position of poi
        Vector3 destination = poi.transform.position;

        // Retain a destination.z of camZ
        destination.z = camZ;

        // Set the camera to the destination
        transform.position = destination;
	}
}
