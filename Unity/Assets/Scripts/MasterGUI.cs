using UnityEngine;
using System.Collections;

public class MasterGUI : MonoBehaviour
{

		
	[SerializeField]
	private GUITexture menu1;
	[SerializeField]
	private GUITexture menu2;
	[SerializeField]
	private GUITexture retryMenu;
	[SerializeField]
	private Camera mainCamera;
	[SerializeField]
	private MissileController rocket;
	private RocketFollow rocketFollowScript;
	private GUIController guiControllerScript;
	private int state;
	[SerializeField]
	private int timerToShowYourFailure;
	
	void Awake ()
	{
		this.rocketFollowScript = this.mainCamera.GetComponent<RocketFollow> ();
		this.guiControllerScript = this.mainCamera.GetComponent<GUIController> ();
		
		this.state = 0;
		this.menu1.gameObject.SetActive (true);
		this.menu2.gameObject.SetActive (false);
		this.retryMenu.gameObject.SetActive (false);
		this.rocket.enabled = false;
		this.rocketFollowScript.theme.Play ();
		this.guiControllerScript.enabled = false;
		
		this.rocketFollowScript.theme.volume = 0.3f;
	}

	void Update ()
	{
		if (this.state == 666 & timerToShowYourFailure > 0) {
			this.timerToShowYourFailure--;
			if (timerToShowYourFailure == 0) {
				this.retryMenu.gameObject.SetActive (true);
			}
		}
		if (Input.GetMouseButtonDown (0) && this.state == 0) {
			this.menu1.gameObject.SetActive (false);
			this.menu2.gameObject.SetActive (true);
			this.state = 1;
		} else if (Input.GetMouseButtonDown (0) && this.state == 1) {
			this.menu2.gameObject.SetActive (false);
			this.rocket.enabled = true;
			this.guiControllerScript.enabled = true;
			this.state = 2;
		} else if (Input.GetMouseButtonDown (0) && this.state == 666) {
			Application.LoadLevel ("workingScene");
		}
	}
	
	void GameOver ()
	{
		this.state = 666;
		//state 666 means your dead.
	}
}

