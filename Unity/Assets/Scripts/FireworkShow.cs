using UnityEngine;
using System.Collections;

public class FireworkShow : MonoBehaviour {
	
	//[SerializeField]
	//public blue1 GameObject ;
	[SerializeField]
	public Transform[] firewrks;
	public bool playOnStart=false;
	public float maxAngle=45.0f;
	
	float nextTime;
	float lastTime;
	bool begun;
	int indx;

	
	Vector3 basePos;
	Quaternion baseRot;
	
	// Use this for initialization
	void Start () {
		begun = false;
		indx=0;
		if(playOnStart)
			startFireworks(new Vector3(0,500,0));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(begun){
			if(Time.time >= nextTime){
				Quaternion randRot = Quaternion.Euler(Random.Range(-maxAngle,maxAngle),Random.Range(-maxAngle,maxAngle),Random.Range(-maxAngle,maxAngle));
				Instantiate(firewrks[Random.Range(0,firewrks.Length)], basePos, baseRot*randRot);	
				nextTime = Time.time + Random.Range(0.02f,0.9f);
			
			}
		}
	}
	
	void startFireworks(Vector3 posn){
		nextTime = Time.time + Random.Range(0.02f,0.5f);
		basePos = posn;
		//basePos = new Vector3(0,0,500);
		baseRot.SetFromToRotation( new Vector3(0,500,0),basePos);
		begun=true;
	}
}
