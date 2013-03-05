using UnityEngine;
using System.Collections;

public class satController : MonoBehaviour {
	
	public float Earth_to_Moon = 1000;
	public float moon_speed = 100;
	
	// Use this for initialization
	void Start () {
		Vector3 moon_start = Random.onUnitSphere * Earth_to_Moon;
		rigidbody.position = moon_start;
		
		rigidbody.transform.localRotation = Quaternion.Euler(Random.Range(0,360),Random.Range(0,360),Random.Range(0,360));
		
		Vector3 moon_direction = Vector3.Cross(Random.onUnitSphere,moon_start.normalized);
		rigidbody.velocity=(moon_direction*moon_speed);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Vector3 moon_pos = rigidbody.position;
		Vector3 accel = - rigidbody.velocity.sqrMagnitude/(moon_pos.sqrMagnitude)*moon_pos;
		
		rigidbody.AddForce(accel, ForceMode.Acceleration);	
	}
	
	void OnCollisionEnter(){
		Destroy(gameObject);
	}
}
