using UnityEngine;
using System.Collections;

public class CameraLee : MonoBehaviour {

    [SerializeField]
    private GameObject objectToFollow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		// Makes the camera follow this object by
		// making it a child of this transform.
		
		// Get the transform of the camera
		Transform cameraTransform = Camera.main.transform;     
		
		// make it a child of the current object
		cameraTransform.parent = this.objectToFollow.transform; 
		
		// place it behind the current object
		cameraTransform.localPosition = (Vector3.forward)*(-10.0f) + (Vector3.right)*(0.0f) + (Vector3.up)*(-60.0f);
		
		// make it point towards the object
		cameraTransform.LookAt(this.objectToFollow.transform.position);
	}
	
}