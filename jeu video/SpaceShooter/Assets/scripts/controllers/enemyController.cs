using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {

    public GameObject shots;
    public float fireRate;
    public Transform shotSpawn;

    private AudioSource audioSource;
    private float nextFire;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();        
    }
    private void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shots, shotSpawn.position, shotSpawn.rotation);
        }
    }
}
