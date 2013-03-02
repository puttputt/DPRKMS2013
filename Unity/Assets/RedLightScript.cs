using UnityEngine;
using System.Collections;

public class RedLightScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(this.light.intensity > 0){
			Vector3 spin = new Vector3(0, 5, 0);
			this.transform.Rotate(spin);
			
			GameObject rocket = GameObject.Find("Rocket");
		
			if(rocket.transform.position.y > 455)
			{
				this.light.intensity-=0.01f;
			}
		}
	}
}
