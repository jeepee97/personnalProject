using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameController : MonoBehaviour {

    public Map MAP_;
    public GameObject selectedCardDisplay_;
    public GameObject tampon;
    public RectTransform ParentPanel;
    public int mapLVL_;
    public int trapDamage_;
    private bool playerTurn_;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        switch (playerTurn_)
        {
            case true :

                break;
            case false:

                break;
        }
	}

    public void SetSelectedCards()
    {
        if (MAP_.selectedUnitProps_ != null &&
            MAP_.selectedUnitProps_.selectedSpells_.Count > 0 &&
            MAP_.selectedUnitProps_.displayingSpells_ == false)
        {
            Debug.Log("GameController");
            MAP_.selectedUnitProps_.displayingSpells_ = true;
            tampon = Instantiate(selectedCardDisplay_);
            MAP_.selectedCard_ = tampon.GetComponentInChildren<SelectedCards>();
            tampon.transform.SetParent(ParentPanel, false);
            tampon.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SetGame()
    {
        if (MAP_.map_ != null)
        {
            foreach(Noeud n in MAP_.map_)
            {
                if (n.trap_ != null)
                {
                    Trap trap = n.trap_.GetComponent<Trap>();
                    trap.trapDamage_ = trapDamage_;
                }
                if (n.playerUnit_ != null && n.playerUnit_.team_ != 1)
                {
                    n.playerUnit_.SetLVL(mapLVL_);
                }
            }
        }
    }
}
