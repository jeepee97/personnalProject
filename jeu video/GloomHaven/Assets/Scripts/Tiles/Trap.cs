using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

    public GameObject gameControllerObject;
    public GameController gameController;
    public int trapDamage_;

	// Use this for initialization
	void Start () {
        gameController = gameControllerObject.GetComponent<GameController>();
	}

}
