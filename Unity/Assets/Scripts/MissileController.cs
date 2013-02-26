using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {
	
	const float torqueStrength=5.0f;
	const float thrustStrength=35.0f;
	const float gravConst=-20f;

    private Vector2 mousePosFromCenter;
	private Vector3 totalTorque;
	static bool begun=false;
	public float fuel = 380000f;
	public float gravity;
	private bool fixtureMove = false;
	
	private ParticleSystem[] particles;
	
	[SerializeField]
	private GameObject finOne;
	[SerializeField]
	private GameObject finTwo;
	[SerializeField]
	private GameObject finThree;
	[SerializeField]
	private ParticleSystem siloExhaust;
	[SerializeField]
	private Animation[] fixtures;
	
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
			
			if(this.transform.position.magnitude > 100)
			{
				this.siloExhaust.Stop();	
			}
			else
			{
				this.siloExhaust.Play();
			}
			
			if(this.fixtureMove == false)
			{
				this.fixtureMove = true;
				this.openFixtures();
			}
			
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
			else if (rocket_pos.magnitude > 450 && rocket_pos.magnitude < 2500)				
				this.gravity = gravConst * (2950-rocket_pos.magnitude)/2950;
			else
				this.gravity = 0;
			
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
	
	private void openFixtures()
	{
		foreach(Animation fixture in this.fixtures)
		{
			fixture.animation.Play();
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
		float distance = this.transform.position.magnitude;
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
