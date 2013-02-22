using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {
	
	const float torqueStrength=5.0f;
	const float thrustStrength=4.0f;
	const float gravConst=-0.5f;

    private Vector2 mousePosFromCenter;
	private Vector3 totalTorque;
	static bool begun=false;
	
	private ParticleSystem[] particles;
	
	[SerializeField]
	private GameObject finOne;
	[SerializeField]
	private GameObject finTwo;
	[SerializeField]
	private GameObject finThree;
	
	private FixedJoint[] joints;
	
	private void Start () 
	{
		this.particles = this.gameObject.GetComponentsInChildren<ParticleSystem>();
		this.joints = GetComponents<FixedJoint>();
		//rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
	}
	
	private void FixedUpdate () 
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.CenterMousePosition(); 
        }
		
        if (Input.GetKey(KeyCode.W))
        {
			begun=true;
			
			this.rigidbody.AddForce(this.transform.up * thrustStrength, ForceMode.Impulse);
			
			this.EnginePlay();
			
        }
		else
		{
			this.EngineStop();	
		}
		
		Vector3 torqueDir = new Vector3();
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			torqueDir.x=1;		
		}
		else if(Input.GetKey(KeyCode.RightArrow)) 
		{
			torqueDir.x=-1;
		}
		else 
		{
			torqueDir.x=0;
		}
			
		if(Input.GetKey(KeyCode.UpArrow)) 
		{
			torqueDir.z=-1;			
		}
		else if(Input.GetKey(KeyCode.DownArrow)) 
		{
			torqueDir.z=1;			
		}
		else
		{
			torqueDir.z=0;
		}
		
		this.totalTorque += torqueDir;
		this.AccelerateTorque();
		
		if(begun)
		{
			Vector3 rocket_pos = this.transform.position;
			Vector3 grav_dir = new Vector3(0.0f, 0.0f, 0.0f);
			if(rocket_pos.magnitude !=0)
				grav_dir = gravConst*(rigidbody.mass)*(rocket_pos)/rocket_pos.magnitude;
			rigidbody.AddForce(grav_dir, ForceMode.Impulse);
		}
		
		if(Input.GetKey(KeyCode.Z))
		{
			this.LaunchFin(0, this.finOne.GetComponent<FinLauncher>());
		}
		else if(Input.GetKey(KeyCode.X))
		{
			this.LaunchFin(1, this.finTwo.GetComponent<FinLauncher>());
		}
		else if(Input.GetKey(KeyCode.C))
		{
			this.LaunchFin(2, this.finThree.GetComponent<FinLauncher>());
		}
	}
	
	private void AccelerateTorque()
	{
		this.rigidbody.AddRelativeTorque(this.totalTorque * torqueStrength, ForceMode.Force);
	}

    private void CenterMousePosition()
    {
        Screen.lockCursor = true;
        Screen.lockCursor = false;
    }
	
	private void DoThingsOnDistance()
	{
		float distance = Vector3.Distance(this.transform.position, new Vector3(0,0,0));
		Debug.Log(distance);
		if(distance > 100)
		{
			
		}
		
	}
	
	public void EnginePlay()
	{
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
	
	public void EngineStop()
	{
		foreach(ParticleSystem ps in this.particles)
			{
				if(ps.name == "EngineFire")
				{
					ps.Stop();
				}
				else if (ps.name == "Exhaust")
				{
					ps.Stop();	
				}
			}	
	}
	
//	void OnCollisionEnter(Collision collision)
//	{
//		foreach(ParticleSystem ps in this.particles)
//		{
//			if(ps.name == "Nuke")
//			{
//				ps.Play();
//			}
//		}
//	}
	
	void LaunchFin(int id, FinLauncher fin)
	{
		Destroy(this.joints[id]);
		fin.launch(this.rigidbody);
	}
	
}
