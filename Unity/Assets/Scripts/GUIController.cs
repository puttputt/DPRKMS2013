using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	

	[SerializeField]
	private GameObject rocket;
	
	// Use this for initialization
	void Start () {
	
	}
	
	private void OnGUI(){
		GUI.Label(new Rect(5,5,500,50),"Altitude = " + (int)this.rocket.rigidbody.position.magnitude + " m");
		GUI.Label(new Rect(5,20,500,50),"Fuel = " + (int)this.rocket.GetComponent<MissileController>().fuel + " L");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//this.altitude.text = rocket.GetComponent<MissileController>().GetDistance() + " m";
		//this.h_velocity.text = new Vector3(rocket.rigidbody.velocity.x, 0, rocket.rigidbody.velocity.z).magnitude + " m/s";
		//this.v_velocity.text = new Vector3(0, rocket.rigidbody.velocity.y, 0).magnitude + " m/s";
	
	}
}
