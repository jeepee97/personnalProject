using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShotsMover : MonoBehaviour {

    public GameObject target;
    public float speed;

    private Rigidbody rb;
    private playerController player;

    private void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("Player");
        if (playerControllerObject != null)
        {
            player = playerControllerObject.GetComponent<playerController>();
        }
        if (playerControllerObject == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        rb = GetComponent<Rigidbody>();

        Vector3 targetPosition = player.transform.position;
        Vector3 startingPosition = transform.position;
        Vector3 direction = startingPosition - targetPosition;
        direction = Vector3.Normalize(direction);

        rb.velocity = direction * speed;
    }
}
