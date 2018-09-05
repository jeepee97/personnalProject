using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingBackground : MonoBehaviour {

    public float speed;
    public GameObject background;

    private Transform transform;


    private void Start()
    {
        transform = GetComponent<Transform>();

    }

    private void Update()
    {
        float positionZ = transform.position.z - speed;
        Vector3 nouvellePosition = transform.position;
        nouvellePosition.z = positionZ;
        transform.position = nouvellePosition;
 
        if (transform.position.z <= -30)
        {
            Vector3 difference = new Vector3(0.0f, 0.0f, 60.0f);
            Instantiate(gameObject, transform.position + difference, transform.rotation);
            Destroy(gameObject);

        }
    }
}
