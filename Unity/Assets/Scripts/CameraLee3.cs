using UnityEngine;
using System.Collections;

public class CameraLee3 : MonoBehaviour {

    [SerializeField]
    private GameObject objectToFollow;
	
	[SerializeField]
    private GameObject target_to_hit;
	// Use this for initialization
	
	[SerializeField]
	float earth_radius = 500;
	
	[SerializeField]
	float camera_rocket_dist = 200.0f;
	
	[SerializeField]
	float look_ahead = 100;
	
	[SerializeField]
	float start_height = -20;
	
	[SerializeField]
	float start_dist = 30;
	
	[SerializeField]
	float smooth = 1500;
	
	void Start () {
			
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Get the transform of the camera
		Transform cameraTransform = Camera.main.transform; 
		Vector3 rocket_pos = this.objectToFollow.transform.position;
		
		if(rocket_pos.magnitude<earth_radius){
			cameraTransform.position = rocket_pos + (new Vector3(0,start_height,start_dist));
			cameraTransform.LookAt(rocket_pos);
		}
		else{
			// Makes the camera follow this object by
			// making it a child of this transform.
			
			Vector3 cam_beyond_rocket_pos = rocket_pos.normalized*(rocket_pos.magnitude + camera_rocket_dist) ;
			// place camera behind further from earth than the rocket
			cameraTransform.position = Vector3.Lerp(cameraTransform.position, cam_beyond_rocket_pos,Time.deltaTime * smooth);			
			
			// vector between rocket and moon, normalized
			Vector3 rock_to_moon = (this.target_to_hit.transform.position- rocket_pos).normalized;
			// make it point towards the object
			cameraTransform.LookAt(rocket_pos + rock_to_moon*look_ahead);
		}
	}
	
}