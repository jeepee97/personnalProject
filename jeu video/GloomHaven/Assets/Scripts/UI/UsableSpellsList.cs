using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsableSpellsList : MonoBehaviour {

    public Map map_;
    public List<GameObject> spells_;
    public RectTransform ParentPanel;
    public int nbButton = 0;
    public bool listIsFull = false;

    // Use this for initialization
    void Start () {
        SetListSpells();
	}

    public void SetListSpells()
    {
        if (map_.selectedUnitProps_ != null)
        {
            spells_ = map_.selectedUnitProps_.spells_;
        }
        foreach (GameObject go in spells_)
        {
            GameObject goButton = Instantiate(go);
            goButton.transform.SetParent(ParentPanel, false);
            goButton.transform.localScale = new Vector3(1, 1, 1);
        }
        listIsFull = true;
    }

    public void ClearList()
    {
        if (listIsFull)
        {
            Button[] childs = GetComponentsInChildren<Button>();
            Debug.Log("list is done");
            foreach (Button button in childs)
            {
                GameObject go = button.gameObject;
                Destroy(go);
            }
            listIsFull = false;
            spells_ = null;
        }
    }

    void ButtonClicked(int buttonNo)
    {
        Debug.Log("Button clicked = " + buttonNo);
    }
}
