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
	public float shakinessIn = 0.2f;
	public float shakinessOut = 0.0f;
	private Vector3 final_cam_pos;
	private Vector3 final_rocket_pos;
	public bool shakey;
	private bool hasExitedSilo;
	// Use this for initialization
	
	[SerializeField]
	private GameObject credits;
	[SerializeField]
	private AudioSource anthem;
	[SerializeField]
	public AudioSource theme;
	[SerializeField]
	private AudioSource nuke;

	void Start ()
	{
		shakey = false;
		hasExitedSilo = false;
	}

	void contact (bool winGame)
	{
		Debug.Log ("contact");
		final_rocket_pos = this.transform.parent.transform.position;
		final_cam_pos = Vector3.Cross (final_rocket_pos.normalized, Random.onUnitSphere) * 4000 + final_rocket_pos * 2;
		
		Debug.Log ("final rocket = " + final_rocket_pos);
		Debug.Log ("final cam = " + final_cam_pos);
		exploded = true;
		
		if (winGame) {
			Vector3 firewrkPos = final_rocket_pos.normalized * 500;
			GameObject.Find ("Fireworks").BroadcastMessage ("startFireworks", firewrkPos);		
			this.credits.SetActive (true);
			this.theme.Stop ();
			this.nuke.Play ();
			this.anthem.Play ();
		} else {
			GameObject.Find ("root").BroadcastMessage ("GameOver");			
		}
	}
			
	// Update is called once per frame
	void LateUpdate ()
	{
		Vector3 RocketPosition = this.transform.parent.transform.position;
		RocketPosition.y += heightMod;
		float distanceFromOrigin = RocketPosition.magnitude;
		//Debug.Log (distanceFromOrigin);
		
		if (exploded) {
			this.transform.position = final_cam_pos;
			this.transform.LookAt (final_rocket_pos / 2);
			//this.transform.rotation = new Quaternion(0,0,0,0);
		} else if (distanceFromOrigin < distanceToSurface && !hasExitedSilo) {
			this.transform.localPosition = new Vector3 (
	            xpos, 
	            ypos, 
	            zpos
	            );
			float t = 0.0f;
			while (t < 1.0f) {
				t += Time.deltaTime * (Time.timeScale / transitionTime);
				Quaternion rot = Quaternion.Euler (xrotation, 
									this.transform.localRotation.eulerAngles.y, 
									this.transform.localRotation.eulerAngles.z);			
				if (shakey)
					rot = Quaternion.Euler (xrotation + Random.Range (-shakinessIn, shakinessIn),
											90 + Random.Range (-shakinessIn, shakinessIn),
											Random.Range (-shakinessIn, shakinessIn));
				this.transform.localRotation = Quaternion.Lerp (this.transform.localRotation, rot, t);
			}
		
		} else {
			hasExitedSilo = true;
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
			
			Quaternion rot = Quaternion.Euler (xrotation2, 90, 0);			
			if (shakey)
				rot = Quaternion.Euler (xrotation2 + Random.Range (-shakinessOut, shakinessOut),
										90 + Random.Range (-shakinessOut, shakinessOut),
										0 + Random.Range (-shakinessOut, shakinessOut));
			

			
			this.transform.localRotation = Quaternion.Lerp (this.transform.localRotation, rot, t);
			yield return 0;
		}	
	}
	
	
}
