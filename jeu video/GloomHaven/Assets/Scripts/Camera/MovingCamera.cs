using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour {

    public float speed;
    private Vector3 mouseOrigin;
    private Vector3 camOrigin;
    private bool isMoving;
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(2))
        {
            // right click was pressed
            camOrigin = gameObject.transform.position;
            mouseOrigin = Input.mousePosition;
            Debug.Log("mouse origin : [" + mouseOrigin.x + ", " + mouseOrigin.y + ", " + mouseOrigin.z + "]");
            isMoving = true;
        }
        if (!Input.GetMouseButton(2))
        {
            isMoving = false;
        }
		if (isMoving)
        {
            Vector3 move = Input.mousePosition;
            Debug.Log("mouse Move : [" + move.x + ", " + move.y + ", " + move.z + "]");

            Vector3 moving = new Vector3(mouseOrigin.x - move.x, 0, mouseOrigin.y - move.y);
            gameObject.transform.position = camOrigin + speed*moving;
        }


	}
}
