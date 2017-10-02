using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour {

    // Fields set in Unity Inspector plane
    public int numClouds = 40;          // The number of clouds to make
    public GameObject[] cloudPrefabs;   // The prefabs for the clouds
    public Vector3 cloudPosMin;        // Min position of each cloud
    public Vector3 cloudPosMax;         // Max position of each cloud
    public float cloudScaleMin = 1;     // Min scale of each cloud
    public float cloudScaleMax = 5;     // Max scale of each cloud
    public float cloudSpeedMult = .5f;  // Adjusts speed of clouds

    public bool ______________________;

    // Fields set dynamically
    public GameObject[] cloudInstances;

    void Awake()
    {
        // Make an array large enough to hald all the cloud instances
        cloudInstances = new GameObject[numClouds];

        // Find the cloudAnchor parent GameObject
        GameObject anchor = GameObject.Find("CloudAnchor");

        // Iterate through and make Cloud_s
        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            // Pick and int between 0 and CloudPrefabs - 1
            // Random.Range will not ever pick as high as top number
            int prefabNum = Random.Range(0, cloudPrefabs.Length);

            // Make an instance
            cloud = Instantiate(cloudPrefabs[prefabNum]) as GameObject;

            // Position cloud
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);

            // Scale cloud
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            // Smaller clouds (with smaller scaleU) should be nearer the ground
            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);

            // Smaller clouds should be farther away
            cPos.z = 100 - 90 * scaleU;

            // Apply these transforms to the cloud
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;

            // Make cloud a chile of the anchor
            cloud.transform.parent = anchor.transform;

            // Add the cloud to cloudInstances
            cloudInstances[i] = cloud;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Iterate over each cloud that was created
        foreach(GameObject cloud in cloudInstances)
        {
            // Get the cloud scale and position
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;

            // Move larger clouds faster
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;

            // If a cloud has moved to far to the left
            if (cPos.x <= cloudPosMin.x)
            {
                // Move it to the far right
                cPos.x = cloudPosMax.x;
            }

            // Apply the new postion to cloud
            cloud.transform.position = cPos;
        }
	}
}
