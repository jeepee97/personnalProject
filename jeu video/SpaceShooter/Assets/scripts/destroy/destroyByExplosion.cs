using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyByExplosion : MonoBehaviour {

    public GameObject asteroidExplosion;
    public int asteroidScore;
    public int giantAsteroidScore;

    private gameController GameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            GameController = gameControllerObject.GetComponent<gameController>();
        }
        if (gameControllerObject == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "asteroid" || other.tag == "giantAsteroid" || other.tag == "smallAsteroid")
        {
            Destroy(other.gameObject);
            Instantiate(asteroidExplosion, other.transform.position, other.transform.rotation);
            if (other.tag == "giantAsteroid")
            {
                GameController.AddScore(giantAsteroidScore);
            }
            else
            {
                GameController.AddScore(asteroidScore);
            }
        }
    }
}
