using UnityEngine;
using System.Collections;

public class SatelliteCreator : MonoBehaviour {

	[SerializeField]
	public GameObject satellite;
	public int numSatellites = 1;
	public float minDist = 900.0f;
	public float maxDist = 1200.0f;
	public float minSpeed = 100.0f;
	public float maxSpeed = 200.0f;
	public float minSize = 20.0f;
	public float maxSize = 2.0f;
	
	// Use this for initialization
	void Start () {

		for(int i=0; i<numSatellites; i++){
			//satController cntrl = new satController();
			
			GameObject clone = Instantiate(satellite, new Vector3(), new Quaternion()) as GameObject ;
			satController cntrl = clone.AddComponent<satController>();
			cntrl.Earth_to_Moon=Random.Range(minDist,maxDist);
			cntrl.moon_speed = Random.Range(minSpeed,maxSpeed);
			
			clone.transform.localScale = new Vector3(Random.Range(minSize,maxSize),Random.Range(minSize,maxSize),Random.Range(minSize,maxSize));
		}		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

