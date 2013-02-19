using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private GameObject objectToFollow;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.LookAt(this.objectToFollow.transform);
        this.transform.position = new Vector3(
            this.objectToFollow.transform.position.x, 
            this.objectToFollow.transform.position.y + 8, 
            this.objectToFollow.transform.position.z - 15
            );
	}
	
}
