using UnityEngine;
using System.Collections;

public class MoonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody.velocity = new Vector3(100.0f,0.0f,0.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Vector3 moon_pos = rigidbody.position;
		Vector3 accel = - rigidbody.velocity.sqrMagnitude/(moon_pos.sqrMagnitude)*moon_pos;
		
		rigidbody.AddForce(accel, ForceMode.Acceleration);
	
	}
}
