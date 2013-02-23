using UnityEngine;
using System.Collections;

public class MissileController2 : MonoBehaviour {
	
	const float torqueStrength=20.0f;
	const float thrustStrength=4.0f;
	const float gravConst=-0.5f;

    private Vector2 mousePosFromCenter;
	static bool begun=false;
	
	private ParticleSystem[] particles;
	
	private void Start () 
	{
		this.particles = this.gameObject.GetComponentsInChildren<ParticleSystem>();
		//rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
	}
	
	// Update is called once per frame
	private void FixedUpdate () 
	{
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
			
			if(Input.GetKey(KeyCode.LeftArrow)) torqueDir.x=1;		
			else if(Input.GetKey(KeyCode.RightArrow)) torqueDir.x=-1;
			else torqueDir.x=0;
			
			if(Input.GetKey(KeyCode.UpArrow)) torqueDir.z=-1;			
			else if(Input.GetKey(KeyCode.DownArrow)) torqueDir.z=1;			
			else torqueDir.z=0;
			
			rigidbody.AddRelativeForce(thrustStrength*thrustDir, ForceMode.Impulse);
			rigidbody.AddRelativeTorque(torqueStrength*torqueDir, ForceMode.Impulse);
						
			foreach(ParticleSystem ps in this.particles)
			{
				if(ps.name == "EngineFire")
				{
					ps.Play();
				}
				else if (ps.name == "Exhaust")
				{
					ps.Play();	
				}
			}
			
        }
		
		if(begun){
			Vector3 grav_dir = gravConst*(rigidbody.mass)*(rigidbody.position)/rigidbody.position.magnitude;
			rigidbody.AddForce(grav_dir, ForceMode.Impulse);
		}

	}

    private void CenterMousePosition()
    {
        Screen.lockCursor = true;
        Screen.lockCursor = false;
    }
	
	private void DoThingsOnDistance()
	{
		float distance = Vector3.Distance(this.transform.position, new Vector3(0,0,0));
		
		if(distance > 100)
		{
			
		}
		
	}
	void OnCollisionEnter(Collision collision)
	{
		foreach(ParticleSystem ps in this.particles)
		{
			if(ps.name == "Nuke")
			{
				ps.Play();
			}
		}
	}
	
	private void OnGUI(){
    	GUI.Button(new Rect(5,5,150,50),"Altitude = " + rigidbody.position.magnitude);
	}
}
