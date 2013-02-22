using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {
	
	const float torqueStrength=5.0f;
	const float thrustStrength=35.0f;
	const float gravConst=-10f;

    private Vector2 mousePosFromCenter;
	private Vector3 totalTorque;
	static bool begun=false;
	public float fuel = 120000f;
	public float gravity;
	
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
		
        if (Input.GetKey(KeyCode.W) && this.fuel > 0)
        {
			begun=true;
			
			this.rigidbody.AddForce(this.transform.up * thrustStrength, ForceMode.Acceleration);
			this.fuel -= 52.21f;
			if(this.fuel < 0)
			{
				this.fuel = 0;
			}
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
			if(rocket_pos.magnitude < 450)
				this.gravity = gravConst;
			else
				this.gravity = gravConst / (float)System.Math.Pow(rocket_pos.magnitude,0.75);
			
			Vector3 grav_dir = new Vector3(0.0f, 0.0f, 0.0f);
			if(rocket_pos.magnitude !=0)
				grav_dir = this.gravity*rocket_pos.normalized;

			rigidbody.AddForce(grav_dir, ForceMode.Acceleration);
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
	
	public float GetDistance()
	{
		return Vector3.Distance(this.transform.position, new Vector3(0,0,0));
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
