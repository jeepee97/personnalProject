using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {
    public Map map_;
    public GameObject card_ = null;
    public SelectedCards selectedCards_;

    private void Start()
    {
        map_ = GetComponentInParent<Map>();
    }

    public void Task()
    {
        if (map_.selectedUnitProps_.selectedSpells_.Count < 2)
        {
            Debug.Log("add card");
            map_.selectedUnitProps_.selectedSpells_.Add(card_);
        }
        map_.gameController.SetSelectedCards();
        selectedCards_ = map_.selectedCard_;
        int count = map_.selectedUnitProps_.selectedSpells_.Count;
        if (count <= 2)
        {
            Debug.Log("est dans le if de task");
            if (map_.selectedCard_ == null)
            {
                Debug.Log("fking cards");
            }
            map_.selectedCard_.ModifierContenu(card_);
        }
        Debug.Log("task est appellee");
    }
}
