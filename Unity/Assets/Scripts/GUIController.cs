using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	

	[SerializeField]
	private GameObject rocket;
	
	// Use this for initialization
	void Start () {
	
	}
	
	private void OnGUI(){
		GUI.Label(new Rect(5,5,500,50),"Altitude = " + this.rocket.rigidbody.position.magnitude + " m");
		GUI.Label(new Rect(5,20,500,50),"H-Velocity = " + new Vector3(rocket.rigidbody.velocity.x, 0, rocket.rigidbody.velocity.z).magnitude + " m/s");
		GUI.Label(new Rect(5,35,500,50),"V-Velocity = " + new Vector3(0, rocket.rigidbody.velocity.y, 0).magnitude + " m/s");
		GUI.Label(new Rect(5,50,500,50),"Fuel = " + this.rocket.GetComponent<MissileController>().fuel + " L");
		//GUI.Label(new Rect(5,65,500,50),"Gravity = " + this.rocket.GetComponent<MissileController>().gravity + " m/s^2");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//this.altitude.text = rocket.GetComponent<MissileController>().GetDistance() + " m";
		//this.h_velocity.text = new Vector3(rocket.rigidbody.velocity.x, 0, rocket.rigidbody.velocity.z).magnitude + " m/s";
		//this.v_velocity.text = new Vector3(0, rocket.rigidbody.velocity.y, 0).magnitude + " m/s";
	
	}
}
