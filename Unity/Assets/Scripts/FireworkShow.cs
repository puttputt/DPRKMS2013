using UnityEngine;
using System.Collections;

public class FireworkShow : MonoBehaviour {
	
	//[SerializeField]
	//public blue1 GameObject ;
	[SerializeField]
	public Transform[] firewrks;
	public float[] firewrkTime;
	
	float startTime;
	bool begun;
	int indx;

	
	Vector3 basePos;
	Quaternion baseRot;
	
	// Use this for initialization
	void Start () {
		begun = false;
		indx=0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(begun){
			if( indx < firewrks.Length){
				if(Time.time >= (startTime+firewrkTime[indx])){
					Instantiate(firewrks[indx], basePos, baseRot);	
					indx++;
				}
			}
			else{
				indx = 0;
				startTime=Time.time;
			}
		}
	}
	
	void startFireworks(Vector3 posn){
		startTime = Time.time;
		basePos = posn;
		//basePos = new Vector3(0,0,500);
		baseRot.SetFromToRotation( new Vector3(0,500,0),basePos);
		begun=true;
	}
}
