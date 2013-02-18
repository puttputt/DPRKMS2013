using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

    private Vector2 mousePosFromCenter;

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.CenterMousePosition(); 
        }
        //Debug.Log("Mouse Pos, relative to center");
        //Debug.Log(this.mouseXCenter - Input.mousePosition.x);
        //Debug.Log(this.mouseYCenter - Input.mousePosition.y);

        if (Input.GetKey(KeyCode.W))
        {
            Vector2 offset = this.MouseOffset();
            Debug.Log(offset.x);
            Debug.Log(offset.y);

            this.rigidbody.AddForce(this.transform.up - 
                new Vector3(
                offset.x / 100,
                0.1f,
                offset.y / 100),
                ForceMode.Impulse);
        }

	}

    private void CenterMousePosition()
    {
        Screen.lockCursor = true;
        Screen.lockCursor = false;
    }

    private Vector2 MouseOffset()
    {
        
        Vector2 mousepos = Input.mousePosition;
        
        mousepos.x -= Screen.width / 2;
        mousepos.y -= Screen.height / 2;

        return mousepos;
    }
}
