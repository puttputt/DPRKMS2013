using UnityEngine;
using System.Collections;

public class RocketFollow : MonoBehaviour
{
	
	public int distanceToSurface = 500;
	public float xpos;
	public float ypos;
	public float zpos;
	public float xrotation;
	public float heightMod;
	public float xpos2;
	public float ypos2;
	public float zpos2;
	public float xrotation2;
	public float transitionTime = 100.0f;
	private bool exploded = false;
	
	private Vector3 final_cam_pos;
	private Vector3 final_rocket_pos;
	// Use this for initialization
	

	void Start ()
	{
	
	}
	

	void contact(){
		final_rocket_pos = this.transform.parent.transform.position;
		final_cam_pos = Vector3.Cross(final_rocket_pos.normalized, Random.onUnitSphere)*4000+final_rocket_pos*2;
		
		Debug.Log("final rocket = " + final_rocket_pos);
		Debug.Log("final cam = " + final_cam_pos);
		exploded=true;
		
		Vector3 firewrkPos = final_rocket_pos.normalized * 500;
		GameObject.Find("Fireworks").BroadcastMessage("startFireworks",firewrkPos);
	}
			
	// Update is called once per frame
	void LateUpdate ()
	{
		Vector3 RocketPosition = this.transform.parent.transform.position;
		RocketPosition.y += heightMod;
		float distanceFromOrigin = RocketPosition.magnitude;
		//Debug.Log (distanceFromOrigin);
		
		if(exploded){
			this.transform.position = final_cam_pos;
			this.transform.LookAt(final_rocket_pos/2);
			//this.transform.rotation = new Quaternion(0,0,0,0);
		}
		
		else if(distanceFromOrigin < distanceToSurface) {
			this.transform.localPosition = new Vector3 (
	            xpos, 
	            ypos, 
	            zpos
	            );
			this.transform.localRotation = Quaternion.Euler (xrotation, this.transform.localRotation.eulerAngles.y, this.transform.localRotation.eulerAngles.z);
		
		} else {
			StartCoroutine (Transition ());
		}
	}
	
	IEnumerator Transition ()
	{
		float t = 0.0f;
		Vector3 newPosition = new Vector3 (
	            xpos2, 
	            ypos2, 
	            zpos2 
	            );
		while (t < 1.0f) {
			t += Time.deltaTime * (Time.timeScale / transitionTime);
			this.transform.localPosition = Vector3.Lerp (this.transform.localPosition, newPosition, t);
			this.transform.localRotation=Quaternion.Lerp(this.transform.localRotation, Quaternion.Euler(xrotation2,90,0), t);

			yield return 0;
		}	
	}

}
