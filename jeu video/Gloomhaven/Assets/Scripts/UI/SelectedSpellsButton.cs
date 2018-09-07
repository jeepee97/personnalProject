using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedSpellsButton : MonoBehaviour {

    public bool afficher;
    public Text displayText;
    public GameObject gameControllerObject;
    public GameController gameController;
    public Map MAP_;

    private void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        afficher = MAP_.selectedUnitProps_.displayingSpells_;
    }

    // Update is called once per frame
    void Update () {

        if (afficher)
        {
            displayText.text = "Hide selected spells";
        }
        else
        {
            displayText.text = "See selected spells";
        }
	}

    public void ChangeDisplay()
    {
        afficher = !afficher;
        Debug.Log("test2");
        if (afficher)
        {
            Debug.Log("test");
            gameController.SetSelectedCards();
            MAP_.selectedUnitProps_.displayingSpells_ = true;
        }
        else
        {
            gameController.deleteSelectedCards();
            MAP_.selectedUnitProps_.displayingSpells_ = false;
        }
    }
}
