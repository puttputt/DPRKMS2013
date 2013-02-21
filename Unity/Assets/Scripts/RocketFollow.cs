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
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		Vector3 RocketPosition = this.transform.parent.transform.position;
		RocketPosition.y += heightMod;
		float distanceFromOrigin = RocketPosition.magnitude;
		Debug.Log (distanceFromOrigin);


		if (distanceFromOrigin < distanceToSurface) {
			this.transform.localPosition = new Vector3 (
	            xpos, 
	            ypos, 
	            zpos
	            );
			this.transform.localRotation = Quaternion.Euler (xrotation, this.transform.localRotation.eulerAngles.y, this.transform.localRotation.eulerAngles.z);
		
		} else {
			StartCoroutine (Transition ());
			/*
			//if (distanceFromOrigin < (distanceToSurface + transitionTime*2.0f)) {
			Vector3 newPosition = new Vector3 (
	            xpos2, 
	            ypos2, 
	            zpos2 
	            );
			Vector3 cameraPanDelta = (this.transform.localPosition - newPosition) / transitionTime;
			this.transform.localPosition -= cameraPanDelta;
			
			float newRotation = ((this.transform.localRotation.x - xrotation2) / transitionTime);
			this.transform.localRotation = Quaternion.Euler (this.transform.localRotation.eulerAngles.x - newRotation, this.transform.localRotation.eulerAngles.y, this.transform.localRotation.eulerAngles.z);
			*/
			/*} else {
				this.transform.localPosition = new Vector3 (
	            xpos2, 
	            ypos2, 
	            zpos2 
	            );
				this.transform.localRotation = Quaternion.Euler (xrotation2, this.transform.localRotation.eulerAngles.y, this.transform.localRotation.eulerAngles.z);
				Debug.Log ("hay");
			}*/
		}
	}
	
	IEnumerator Transition ()
	{
		float t = 0.0f;
		Vector3 startingPos = transform.position;
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
