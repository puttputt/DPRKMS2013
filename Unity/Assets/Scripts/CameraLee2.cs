using UnityEngine;
using System.Collections;

public class CameraLee2 : MonoBehaviour {

    [SerializeField]
    private GameObject objectToFollow;
	
	[SerializeField]
    private GameObject target_to_hit;
	// Use this for initialization
	
	[SerializeField]
	float earth_radius = 500;
	
	[SerializeField]
	float camera_rocket_dist = 100;
	
	[SerializeField]
	float look_ahead = 50;
	
	[SerializeField]
	float start_height = -20;
	
	[SerializeField]
	float start_dist = 30;
	
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		// Get the transform of the camera
		Transform cameraTransform = Camera.main.transform; 
		
		if(this.objectToFollow.transform.position.magnitude<earth_radius){
			cameraTransform.position = this.objectToFollow.transform.position + (new Vector3(0,start_height,start_dist));
			cameraTransform.LookAt(this.objectToFollow.transform.position);
		}
		else{
			// Makes the camera follow this object by
			// making it a child of this transform.
			
			Vector3 rocket_pos_norm = this.objectToFollow.transform.position.normalized;			
			Vector3 beyond_rocket = rocket_pos_norm*(this.objectToFollow.transform.position.magnitude + camera_rocket_dist) ;
			
			// place camera behind further from earth than the rocket
			cameraTransform.position = beyond_rocket;			
			
			// vector between rocket and moon, normalized
			Vector3 rock_to_moon = (this.target_to_hit.transform.position- this.objectToFollow.transform.position).normalized;
			// make it point towards the object
			cameraTransform.LookAt(this.objectToFollow.transform.position + rock_to_moon*look_ahead);
		}
	}
	
}