using UnityEngine;
using System.Collections;

public class EngineController : MonoBehaviour
{
	public ParticleSystem launchSmoke;
	public ParticleSystem flightSmoke;
	public ParticleSystem flightFlames;
	public GameObject Rocket;
	public int disableSmokeHeight;

	private void Start ()
	{
	}
	
	public void Update ()
	{
		if (this.Rocket.transform.position.y > this.disableSmokeHeight) {
			//launchSmoke.Stop ();
		}
	}

	public void play ()
	{
		this.light.gameObject.SetActive (true);
		flightSmoke.Play ();
		flightFlames.Play ();
		//if (this.Rocket.transform.position.y < this.disableSmokeHeight)
		launchSmoke.Play ();
	}

	public void stop ()
	{
		this.light.gameObject.SetActive (false);
		flightSmoke.Stop ();
		flightFlames.Stop ();
	}
}
