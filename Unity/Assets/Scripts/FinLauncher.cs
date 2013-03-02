using UnityEngine;
using System.Collections;

public class FinLauncher : MonoBehaviour {

	private bool launched = false;
	const float gravConst=-0.5f;
	private Vector3 totalTorque;
	
	public void Start()
	{
		this.totalTorque = new Vector3(0f, 0f, 0f);	
	}
	
	public void FixedUpdate()
	{
		if(this.launched)
		{
			
			Vector3 rocket_pos = this.transform.position;
			Vector3 grav_dir = new Vector3(0.0f, 0.0f, 0.0f);
			if(rocket_pos.magnitude !=0)
				grav_dir = gravConst*(rigidbody.mass)*(rocket_pos)/rocket_pos.magnitude;
			rigidbody.AddForce(grav_dir, ForceMode.Impulse);
			
			this.AccelerateTorque();
		}
	}
	
	public void launch(Rigidbody rocketBody)
	{
		if(!this.launched)
		{
			this.totalTorque += this.transform.right * 10;
			this.rigidbody.AddForce(this.rigidbody.transform.right * 5, ForceMode.Impulse);
			this.launched = true;
		}
	}
	
	private void AccelerateTorque()
	{
		this.rigidbody.AddRelativeTorque(this.totalTorque * 25, ForceMode.Force);
	}
}
