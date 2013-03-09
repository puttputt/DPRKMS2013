using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour
{
	
	const float torqueStrength = 5.0f;
	const float thrustStrength = 40.0f;
	const float gravConst = -20f;
	private Vector2 mousePosFromCenter;
	private Vector3 totalTorque;
	static bool begun = false;
	public float fuel = 380000f;
	public float gravity;
	private bool fixtureMove = false;
	private float engineStartupTime = 2.5f;
	private bool isExploded;
	private ParticleSystem[] particles;
	public int finOneDetachHeight = 25;
	public int finTwoDetachHeight = 100;
	public int finThreeDetachHeight = 150;
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
	[SerializeField]
	private AudioSource rocketSound;
	[SerializeField]
	private AudioSource rocketInSpaceSound;
	[SerializeField]
	private AudioSource finFallSound;
	[SerializeField]
	private AudioSource FixtureReleaseSound;
	private int finsLaunched = 0;
	private FixedJoint[] joints;
	public Light rocketLight;

	private void Start ()
	{
		this.particles = this.gameObject.GetComponentsInChildren<ParticleSystem> ();
		this.joints = GetComponents<FixedJoint> ();
		isExploded = false;
		engineStartupTime = 2.5f;
		begun = false;
		//rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
	}
	
	private void FixedUpdate ()
	{
     	
		if (Input.GetKey (KeyCode.W) && this.fuel > 0) {
			if (this.engineStartupTime > 0)
				this.engineStartupTime -= Time.deltaTime;
			
			
			if (this.fuel < 0) {
				this.fuel = 0;
			}
			this.EnginePlay ();
			
			if (this.transform.position.magnitude > 100) {
				this.siloExhaust.Stop ();	
			} else {
				this.siloExhaust.Play ();
			}
			if (this.engineStartupTime <= 0) {
				if (this.fixtureMove == false) {
					this.fixtureMove = true;
					this.openFixtures ();
				}
				begun = true;
				GameObject.Find ("Main Camera").GetComponent<RocketFollow> ().shakey = true;
				this.rigidbody.AddForce (this.transform.up * thrustStrength, ForceMode.Acceleration);
				this.fuel -= 52.21f;
			}
			
			
		} else if (this.fuel <= 0 && !isExploded) {
			isExploded = true;
			this.BroadcastMessage ("outOfFuel");
		} else {
			GameObject.Find ("Main Camera").GetComponent<RocketFollow> ().shakey = false;
			this.EngineStop ();	
			this.siloExhaust.Stop ();
		}
		
		if (this.engineStartupTime < 0) {
			Vector3 torqueDir = new Vector3 ();
			if (Input.GetKey (KeyCode.LeftArrow)) {
				torqueDir.x = 1;		
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				torqueDir.x = -1;
			} else {
				torqueDir.x = 0;
			}
			
			if (Input.GetKey (KeyCode.UpArrow) && this.transform.position.magnitude > 450) {
				torqueDir.z = -1;			
			} else if (Input.GetKey (KeyCode.DownArrow)&& this.transform.position.magnitude > 450) {
				torqueDir.z = 1;			
			} else {
				torqueDir.z = 0;
			}
				
			if (this.finsLaunched == 1) {
				torqueDir.x -= 0.2f;
			} else if (this.finsLaunched == 3) {
				torqueDir.z -= 0.2f;	
			}
			
			this.totalTorque += torqueDir;

		}
		
		this.AccelerateTorque ();
		
		if (begun) {
			Vector3 rocket_pos = this.transform.position;
			if (rocket_pos.magnitude < 450){
				this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, 0);
				this.gravity = gravConst;
			}
			else if (rocket_pos.magnitude > 450 && rocket_pos.magnitude < 2950)				
				this.gravity = gravConst * (2950 - rocket_pos.magnitude) / 2950;
			else
				this.gravity = 0;
			
			Vector3 grav_dir = new Vector3 (0.0f, 0.0f, 0.0f);
			if (rocket_pos.magnitude != 0)
				grav_dir = this.gravity * rocket_pos.normalized;

			rigidbody.AddForce (grav_dir, ForceMode.Acceleration);
		}
		
		DoThingsOnDistance ();
	}
	
	private void openFixtures ()
	{
		foreach (Animation fixture in this.fixtures) {
			fixture.animation.Play ();
		}
		this.FixtureReleaseSound.Play ();
	}
	
	private void AccelerateTorque ()
	{
		this.rigidbody.AddRelativeTorque (this.totalTorque * torqueStrength, ForceMode.Force);
	}

	private void CenterMousePosition ()
	{
		Screen.lockCursor = true;
		Screen.lockCursor = false;
	}
	
	private void DoThingsOnDistance ()
	{
		float distance = this.transform.position.magnitude;
		//Debug.Log(distance);
		if (distance > finOneDetachHeight && this.finsLaunched == 0) {
			this.LaunchFin (0, this.finOne.GetComponent<FinLauncher> ());
			this.finsLaunched = 1;
		} else if (distance > finTwoDetachHeight && this.finsLaunched == 1) {
			this.LaunchFin (2, this.finThree.GetComponent<FinLauncher> ());
			this.finsLaunched = 2;
		} else if (distance > finThreeDetachHeight && this.finsLaunched == 2) {
			this.LaunchFin (1, this.finTwo.GetComponent<FinLauncher> ());
			this.finsLaunched = 3;
		}
		
	}
	
	public float GetDistance ()
	{
		return Vector3.Distance (this.transform.position, new Vector3 (0, 0, 0));
	}
		
	public void EnginePlay ()
	{
		foreach (ParticleSystem ps in this.particles) {
			if (ps.name == "EngineFire") {
				ps.Play ();
			} else if (ps.name == "Exhaust") {
				ps.Play ();	
			}
		}
		if (this.transform.position.magnitude < 450) {
			
			if (!this.rocketSound.isPlaying)
				this.rocketSound.Play ();
		} else {
			this.rocketSound.volume = 0.2f;
			if (!this.rocketSound.isPlaying)
				this.rocketSound.Play ();			
		}
		rocketLight.enabled = true;
	}
	
	public void EngineStop ()
	{
		foreach (ParticleSystem ps in this.particles) {
			if (ps.name == "EngineFire") {
				ps.Stop ();
			} else if (ps.name == "Exhaust") {
				ps.Stop ();	
			}
		}
		this.rocketInSpaceSound.Stop ();
		this.rocketSound.Stop ();
		rocketLight.enabled = false;
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
	
	void LaunchFin (int id, FinLauncher fin)
	{
		this.finFallSound.Play ();
		Destroy (this.joints [id]);
		fin.launch (this.rigidbody);
	}
	
}
