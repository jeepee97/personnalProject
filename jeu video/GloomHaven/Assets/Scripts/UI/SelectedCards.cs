using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCards : MonoBehaviour {
    public Map map_;
    public Unit selectedUnit;
    public List<GameObject> cards_ = new List<GameObject>();
    public RectTransform ParentPanel;
    public bool listIsFull = false;
    public bool contenuAEteModifier = true;

    private void Start()
    {
        map_ = GetComponentInParent<Map>();
        selectedUnit = map_.selectedUnitProps_;
        cards_ = selectedUnit.selectedSpells_;
    }

    private void Update()
    {
        if (contenuAEteModifier)
        {
            RefreshList();
            foreach (GameObject go in cards_)
            {
                GameObject goButton = Instantiate(go);
                goButton.transform.SetParent(ParentPanel, false);
                goButton.transform.localScale = new Vector3(1, 1, 1);
            }
            contenuAEteModifier = false;
            listIsFull = true;
        }
    }

    public void ModifierContenu(GameObject card)
    {
        Debug.Log("est dans modifier contenu");
        cards_.Add(card);
        contenuAEteModifier = true;
    }

    public void RefreshList()
    {
        if (listIsFull)
        {
            Image[] childs = GetComponentsInChildren<Image>();
            Debug.Log("list is done");
            foreach (Image image in childs)
            {
                GameObject go = image.gameObject;
                Destroy(go);
            }
            listIsFull = false;
        }
    }
}
