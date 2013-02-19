using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {
	
	const float rocketStrength=6;
	const float gravConst=-1;

    private Vector2 mousePosFromCenter;
	static bool begun=false;
	void Start () {
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.CenterMousePosition(); 
        }
        //Debug.Log("Mouse Pos, relative to center");
        //Debug.Log(this.mouseXCenter - Input.mousePosition.x);
        //Debug.Log(this.mouseYCenter - Input.mousePosition.y);
		
		
		
        if (Input.GetKey(KeyCode.W))
        {
			begun=true;
            Vector2 offset = this.MouseOffset();

			//Vector3 pos = new Vector3(0.0f,0.0f,0.0f);
			Vector3 dir = new Vector3(offset.x,1.0f,offset.y);
				
			rigidbody.AddForce(rocketStrength*dir, ForceMode.Impulse);
			
			//this.rigidbody.AddRelativeForce(offset.x,1,offset.y, ForceMode.Impulse);
            //this.rigidbody.AddForce(this.transform.up, ForceMode.Impulse);
        }
		
		if(begun){
			Vector3 rocket_pos = rigidbody.position;
			Vector3 grav_dir = gravConst*(rigidbody.mass)*(rocket_pos)/rocket_pos.magnitude;
			//Debug.Log("grav="+grav_dir);
			rigidbody.AddForce(grav_dir, ForceMode.Impulse);
		}

	}

    private void CenterMousePosition()
    {
        Screen.lockCursor = true;
        Screen.lockCursor = false;
    }

    private Vector2 MouseOffset()
    {
        //return mouse offset between 0 and 1w
        Vector2 mousepos = Input.mousePosition; 
        
        mousepos.x = (mousepos.x/( Screen.width/2)-1);
		if(mousepos.x > 1)
			mousepos.x=1;
		if(mousepos.x <- 1)
			mousepos.x=-1;
		
        mousepos.y = (mousepos.y/( Screen.height/2)-1);
		if(mousepos.y > 1)
			mousepos.y=1;
		if(mousepos.y <- 1)
			mousepos.y=-1;

        return mousepos;
    }
}
