using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour {

    public bool isRevealed;
    public List<GameObject> tilesGameObject;
    public ClickableTile[] tiles_;

	// Use this for initialization
	void Start () {
        transform.position = transform.position + Vector3.up * 20;
    }

    public void CreateSections()
    {
        tiles_ = GetComponentsInChildren<ClickableTile>();
        tilesGameObject = new List<GameObject>();
        foreach (ClickableTile tile in tiles_)
        {
            tilesGameObject.Add(tile.gameObject);
        }
        if (isRevealed == true)
        {
            Reveal();
        }
    }

    public void Reveal()
    {
        isRevealed = true;
        transform.position = transform.position - Vector3.up*20;
        foreach (ClickableTile tile in tiles_)
        {
            if (tile != null)
            {
                tile.isRevealed_ = true;
            }
        }
    }
}
