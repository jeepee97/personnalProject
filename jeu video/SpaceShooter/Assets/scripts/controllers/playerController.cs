using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class playerController : MonoBehaviour {

    public float speed;
    public float tilt;
    public Boundary boundary;
    
    public GameObject shot;
    public GameObject shotL;
    public GameObject shotR;
    public GameObject explosion;
    public Transform shotSpawn;
    public GameObject playerExplosion;
    public float fireRate;

    private AudioSource audioSource;
    private float nextFire;
    private bool tripleShot = false;
    private GameObject bolt;
    private Rigidbody rb;
    private int redPower = 0, greenPower = 0;
    private int explosionCount = 0;
    private gameController GameController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bolt = shot;
        shot.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        audioSource = GetComponent<AudioSource>();

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

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bolt, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
            if (tripleShot)
            {
                Instantiate(shotL, shotSpawn.position, shotSpawn.rotation);
                Instantiate(shotR, shotSpawn.position, shotSpawn.rotation);
            }
        }
        if (Input.GetButtonDown("Fire1") && explosionCount > 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(playerExplosion, transform.position, transform.rotation);
            explosionCount -= 1;
            GameController.AddExplosionCount(-1);
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.velocity = movement * speed;
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
        rb.position = new Vector3
            (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "power")
        {
            BoltModificateur(other.gameObject);
            Destroy(other.gameObject);
        }
    }
    private void BoltModificateur(GameObject power)
    {
        if (power.name == "powerGreen(Clone)")
        {
            if (greenPower < 2)
            {
                fireRate -= 0.0025f;
                greenPower += 1;
            }
        }
        else if (power.name == "powerRed(Clone)")
        {
            if (redPower < 1)
            {
                shot.transform.localScale *= 2;
                redPower += 1;
            }
            else if (redPower >= 1)
            {
                tripleShot = true;
                redPower += 1;
            }
        }
        else if (power.name == "powerPurple(Clone)")
        {
            explosionCount += 1;
            GameController.AddExplosionCount(1);
        }
    }
}
