using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit : MonoBehaviour {

    public Map map_;

    public int team_; //team1 behing player's, team2 enemies
    public bool isWalking_ = true; //if false -> isFlying or isJumping
    public int speed_;
    public int movingRange_;
    public int attackRange_;
    public int health_;
    public int experience_;
    public int gold_;
    public int level_;
    public List<GameObject> spells_;
    public List<GameObject> selectedSpells_;
    public bool displayingSpells_ = false;
    public GameObject[] defauseCards_;
    public GameObject[] lostCards_;
    public GameObject[] modifierCards_;
    public GameObject[] items_;

    private void OnMouseUp()
    {
        map_.ChangeSelectedUnit(gameObject);
    }

    public void SetLVL(int mapLVL)
    {
        
    }
}
