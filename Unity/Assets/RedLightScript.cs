using UnityEngine;
using System.Collections;

public class RedLightScript : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 spin = new Vector3(0, 5, 0);
		this.transform.Rotate(spin);
	}
}
