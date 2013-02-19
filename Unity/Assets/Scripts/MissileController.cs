using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {
	
	const float torqueStrength=20.0f;
	const float thrustStrength=4.0f;
	const float gravConst=-0.5f;

    private Vector2 mousePosFromCenter;
	static bool begun=false;
	void Start () {
		//rigidbody.freezeRotation = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.CenterMousePosition(); 
        }
		
        if (Input.GetKey(KeyCode.W))
        {
			begun=true;
			
			Vector3 torqueDir = new Vector3();
			Vector3 thrustDir = new Vector3();
			thrustDir.y=1;
			
			
			
			if(Input.GetKey(KeyCode.LeftArrow)) torqueDir.z=1;		
			else if(Input.GetKey(KeyCode.RightArrow)) torqueDir.z=-1;
			else torqueDir.z=0;
			
			if(Input.GetKey(KeyCode.UpArrow)) torqueDir.x=1;			
			else if(Input.GetKey(KeyCode.DownArrow)) torqueDir.x=-1;			
			else torqueDir.x=0;
			
			Vector3 forcePos = new Vector3(0.0f,14.0f,0.0f);
			rigidbody.AddRelativeForce(thrustStrength*thrustDir, ForceMode.Impulse);
			rigidbody.AddRelativeTorque(torqueStrength*torqueDir, ForceMode.Impulse);
        }
		
		if(begun){
			Vector3 rocket_pos = rigidbody.position;
			Vector3 grav_dir = gravConst*(rigidbody.mass)*(rocket_pos)/rocket_pos.magnitude;
			rigidbody.AddForce(grav_dir, ForceMode.Impulse);
		}

	}

    private void CenterMousePosition()
    {
        Screen.lockCursor = true;
        Screen.lockCursor = false;
    }
}
