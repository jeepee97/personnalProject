using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Powers
{
    public GameObject power1, power2, power3;
}

[System.Serializable]
public class SmallAsteroids
{
    public GameObject smallAsteroid1, smallAsteroid2, smallAsteroid3;
}
public class destroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    public GameObject enemyExplosion;

    private gameController GameController;
    private int health;
    private Rigidbody rb1;
    private Rigidbody rb2;
    private int powerChance;

    public SmallAsteroids smallAsteroids;
    public int nombreAsteroids;
    public Powers powers;
    private Rigidbody rb;
    private GameObject asteroid;

    private void Start()
    {
        health = 1;
        if(tag == "giantAsteroid")
        {
            health = 5;
        }
        if (tag == "enemy")
        {
            health = 8;
        }
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
        if (tag == "enemyShot")
        {
            if(other.tag == "Player")
            {
                Destroy(other.gameObject);
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                Destroy(gameObject);
            }
        }   
        if (other.tag == "bolt")
        {
            health -= 1;
            Destroy(other.gameObject);
            if (health == 0)
            {
                Destroy(gameObject);
                if (tag == "enemy")
                {
                    Instantiate(enemyExplosion, transform.position, transform.rotation);
                }
                else
                {
                    Instantiate(explosion, transform.position, transform.rotation);
                }
                GameController.AddScore(scoreValue);

                powerChance = (int)Random.Range(0.0f, 20.0f);
                if (powerChance == 1)
                {
                    if (tag != "enemyShot")
                    {
                        Instantiate(PowerGenerateur(powers.power1, powers.power2, powers.power3), transform.position, transform.rotation);
                    }
                }

                if (tag == "giantAsteroid")
                {
                    for (int i = 0; i < nombreAsteroids; i++)
                    {
                        asteroid = GameController.AsteroidGenerater(smallAsteroids.smallAsteroid1, smallAsteroids.smallAsteroid2, smallAsteroids.smallAsteroid3);

                        Vector3 spawnPosition = transform.position;
                        Quaternion spawnRotation = Quaternion.identity;
                        Instantiate(asteroid, spawnPosition, spawnRotation);

                    }
                }
            }
        }
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            GameController.GameOver();
        }
    }

    private GameObject PowerGenerateur(GameObject power1, GameObject power2, GameObject power3)
    {
        float chance = Random.Range(0.0f, 3.0f);
        if (chance < 1)
        {
            return power1;
        }
        else if (chance < 2)
        {
            return power2;
        }
        else
        {
            return power3;
        }
    }
}
