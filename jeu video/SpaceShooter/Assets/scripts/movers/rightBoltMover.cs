using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightBoltMover : MonoBehaviour {

    public float speed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 direction = new Vector3(0.0f, 0.0f, 1.0f);
        direction = Quaternion.Euler(0, 25, 0) * direction;
        transform.Rotate(0.0f, 25.0f, 0.0f, Space.Self);
        rb.velocity = direction * speed;

    }
}
