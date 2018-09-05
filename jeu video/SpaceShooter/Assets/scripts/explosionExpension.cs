using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionExpension : MonoBehaviour {

    public float maxSize;
    public float growFactor;

    void Start()
    {
        StartCoroutine(Scale());
    }

    IEnumerator Scale()
    {
        float timer = 0;

        while (maxSize > transform.localScale.x)
        {
            timer += Time.deltaTime;
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
            yield return null;
        }

    }
}
