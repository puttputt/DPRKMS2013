using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private GameObject objectToFollow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.LookAt(this.objectToFollow.transform);
        this.transform.position = new Vector3(
            this.objectToFollow.transform.position.x, 
            this.objectToFollow.transform.position.y, 
            this.objectToFollow.transform.position.z - 50
            );
		
		/*this.transform.rotation = new Vector3(
            this.objectToFollow.transform.rotation.x, 
            this.objectToFollow.transform.rotation.y, 
            this.objectToFollow.transform.rotation.z
            );*/
//		
//		float smooth = 2.0f;
//		float tiltAngle = 30.0f;
//	    float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
//	    float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
		Quaternion cam_rot = this.objectToFollow.transform.rotation;
		cam_rot.x = 0;
		//cam_rot.z=0;
		// Dampen towards the target rotation
	    //this.transform.rotation = Quaternion.Euler (tiltAroundX, tiltAroundY, tiltAroundZ);
		this.transform.rotation = cam_rot;
	}
	
}
