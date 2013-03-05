using UnityEngine;
using System.Collections;

public class MasterGUI : MonoBehaviour {

		
	[SerializeField]
	private GUITexture menu1;
	
	[SerializeField]
	private GUITexture menu2;
	
	[SerializeField]
	private Camera mainCamera;
	
	[SerializeField]
	private MissileController rocket;
	
	private RocketFollow rocketFollowScript;
	private GUIController guiControllerScript;
	private int state;
	
	void Awake () 
	{
		this.rocketFollowScript = this.mainCamera.GetComponent<RocketFollow>();
		this.guiControllerScript = this.mainCamera.GetComponent<GUIController>();
		
		this.state = 0;
		this.menu1.gameObject.SetActive(true);
		this.menu2.gameObject.SetActive(false);
		this.rocket.enabled = false;
		this.rocketFollowScript.theme.Play();
		this.guiControllerScript.enabled = false;
	}
	

	void Update () 
	{
		if(Input.GetMouseButtonDown(0) && this.state == 0)
		{
			this.menu1.gameObject.SetActive(false);
			this.menu2.gameObject.SetActive(true);
			this.state = 1;
		}
		else if (Input.GetMouseButtonDown(0) && this.state == 1)
		{
			this.menu2.gameObject.SetActive(false);
			this.rocket.enabled = true;
			this.rocketFollowScript.theme.volume = 0.1f;
			this.guiControllerScript.enabled = true;
			this.state = 2;
		}
		
	
	}
}

