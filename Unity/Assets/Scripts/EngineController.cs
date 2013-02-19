using UnityEngine;
using System.Collections;

public class EngineController : MonoBehaviour 
{

    private ParticleSystem[] particles;

    private void Start()
    {
		this.particles = this.gameObject.GetComponentsInChildren<ParticleSystem>();
    }

    public void play()
    {
        this.light.gameObject.SetActive(true);
		foreach(ParticleSystem ps in this.particles)
		{
			Debug.Log(ps.name);
			ps.Play();
		}
    }

    public void stop()
    {
        this.light.gameObject.SetActive(false);
        foreach(ParticleSystem ps in this.particles)
		{
			ps.Stop();
		}
    }
}
