using UnityEngine;
using System.Collections;

public class EngineController : MonoBehaviour {

    [SerializeField]
    private Light light;

    [SerializeField]
    private ParticleSystem fire;

    [SerializeField]
    private ParticleSystem exhaust;

    void Start()
    {
    }

    public void play()
    {
        this.light.gameObject.SetActive(true);
        this.exhaust.Play();
        this.fire.Play();
    }

    public void stop()
    {
        this.light.gameObject.SetActive(false);
        this.exhaust.Stop();
        this.fire.Stop();
    }
}
