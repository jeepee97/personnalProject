using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{

    public int tileX_;
    public int tileY_;
    public int posX_;
    public int posY_;
    public bool isRevealed_ = false;
    public bool inPlayerMovingRange;
    public bool walkable_;
    public Map map_;

    private void OnMouseUp()
    {
        if (walkable_ && inPlayerMovingRange && isRevealed_)
        {
            Debug.Log("ClickTile [" + tileX_ + ", " + tileY_ + "] [" + posX_ + ", " + posY_ + "]");
            map_.MoveSelectedUnitTo(tileX_, tileY_);
        }
    }
}