using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounceByBoundary : MonoBehaviour {

    private Rigidbody rb;

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "bolt" || other.tag == "enemy")
        {
            
        }
        else
        {
            rb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 nouvelleVitesse = rb.velocity;
            nouvelleVitesse.x = -nouvelleVitesse.x;
            rb.velocity = nouvelleVitesse;
        }
    }
}
