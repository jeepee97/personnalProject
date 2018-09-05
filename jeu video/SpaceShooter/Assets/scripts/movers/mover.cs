using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour {

    public float speed;

    private float powerSpeed;
    private Rigidbody rb;
    private gameController GameController;

	// Use this for initialization
	void Start () {

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            GameController = gameControllerObject.GetComponent<gameController>();
        }
        if (gameControllerObject == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        rb = GetComponent<Rigidbody>();
        Vector3 trajectoire = new Vector3(Random.Range(-0.9f, 0.9f), 0.0f, 1);
        Vector3 trajectoire2 = Random.onUnitSphere;
        trajectoire2.y = 0.0f;

        if (tag == "power")
        {
            rb.velocity = trajectoire2 * speed;
        }
        else if (tag == "smallAsteroid")
        {
            rb.velocity = trajectoire2 * speed;
        }
        else if (tag == "bolt")
        {
            rb.velocity = transform.forward * speed;
        }
        else if (GameController.level >= 3)
        {
            if (GameController.level == 4)
            {
                speed -= 1;
            }
            
            if (GameController.spawnWait < 0.5)
            {
                rb.velocity = transform.forward * speed;
            }
            else
            {
                rb.velocity = trajectoire * speed;
            }
        }
        else
        {
            rb.velocity = transform.forward * speed;
        }
	}
}
