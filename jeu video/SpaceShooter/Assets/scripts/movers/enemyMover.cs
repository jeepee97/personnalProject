using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMover : MonoBehaviour {

    public float speed;

    private float xSpeed;
    private Rigidbody rb;
    private Transform transform;
    private Vector3 trajectoire;

    private void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        trajectoire = Random.onUnitSphere;
        trajectoire.y = 0;
        if ((transform.position.x < 0 && trajectoire.x < 0) || (transform.position.x > 0 && trajectoire.x > 0))
        {
            trajectoire.x = -trajectoire.x;
        }
        if (trajectoire.z > 0)
        {
            trajectoire.z = -trajectoire.z;
        }
        
    }
    private void Update()
    {
        rb.velocity = trajectoire * speed;
    }
}
