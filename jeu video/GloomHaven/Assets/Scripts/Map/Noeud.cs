using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noeud {
    //set dans le constructeur
    public bool walkable_;
    public Vector3 worldPosition_;
    public Collider[] colliders_;
    public Map map_;
    public int x_, y_;

    public GameObject player_;
    public Unit playerUnit_;
    public GameObject tiles_;
    public ClickableTile clickableTile;
    public GameObject trapGO_;
    public Trap trap_;

    //gCost = ce que ca a couter jusqu'a present
    public int gCost;
    //hCost = estimation de ce quil reste a payer
    public int hCost;
    //valeur de la position a laquelle on se trouve
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    public Noeud parent_;



    public Noeud(bool walkable, Vector3 worldPosition, Collider[] colliders, Map map, int x, int y)
    {
        walkable_ = walkable;
        worldPosition_ = worldPosition;
        colliders_ = colliders;
        map_ = map;
        x_ = x;
        y_ = y;

        SetComponents();
        SetClickableTiles();
    }

    private void SetComponents()
    {
        foreach (Collider collider in colliders_)
        {
            if (collider.gameObject.tag == "Tiles" || collider.gameObject.tag == "Door")
            {
                tiles_ = collider.gameObject;
            }
            if (collider.gameObject.tag == "Player")
            {
                player_ = collider.gameObject;
                playerUnit_ = player_.GetComponent<Unit>();
                map_.playerTile_ = this;
            }

            if (collider.gameObject.tag == "Trap")
            {
                trapGO_ = collider.gameObject;
                trap_ = trapGO_.GetComponent<Trap>();
            }
        }
    }

    private void SetClickableTiles()
    {
        if (tiles_ != null)
        {
            clickableTile = tiles_.GetComponent<ClickableTile>();
            clickableTile.tileX_ = x_;
            clickableTile.tileY_ = y_;
            clickableTile.posX_ = (int)worldPosition_.x;
            clickableTile.posY_ = (int)worldPosition_.z;
            clickableTile.map_ = map_;
            clickableTile.walkable_ = walkable_;
            clickableTile.inPlayerMovingRange = false;
            if (tiles_.tag == "Door")
            {
                clickableTile.isRevealed_ = true;
            }
        }
    }

    public void SetPlayerMovingTiles()
    {
        if (player_ != null)
        {
            List<Noeud> openSet1 = new List<Noeud>(50);
            List<Noeud> openSet2 = new List<Noeud>(50);
            List<Noeud> closedSet = new List<Noeud>(50);

            openSet1.Add(this);
            openSet2.Add(this);

            for (int m = 0; m < map_.selectedUnitProps_.movingRange_; m++)
            {
                foreach (Noeud n in openSet1)
                {
                    List<Noeud> neighbours = n.map_.GetNeighbours(n);
                    foreach (Noeud nb in neighbours)
                    {
                        if (nb.walkable_)
                        {
                            int otherTeam = 0;
                            if (nb.player_ != null)
                            {
                                Unit unit = nb.player_.GetComponent<Unit>();
                                otherTeam = unit.team_;
                                if (otherTeam == playerUnit_.team_ || !playerUnit_.isWalking_)
                                {
                                    openSet2.Add(nb);
                                }
                            }
                            else if (!openSet2.Contains(nb))
                            {
                                openSet2.Add(nb);
                            }
                        }
                    }

                    //if (n.player_ == null || n.player_ == player_)
                    if (n.player_ == null)
                    {
                        closedSet.Add(n);
                    }
                }

                foreach (Noeud n in openSet2)
                {
                    openSet1.Add(n);
                }
                foreach(Noeud n in closedSet)
                {
                    openSet1.Remove(n);
                }
            }

            foreach (Noeud n in openSet2)
            {
                if (!closedSet.Contains(n) && n.player_ == null)
                {
                    closedSet.Add(n);
                }
            }

            foreach (Noeud n in closedSet)
            {
                ClickableTile CT = n.tiles_.GetComponent<ClickableTile>();
                CT.inPlayerMovingRange = true;
            }

            map_.playerMovingTiles_ = closedSet;
            map_.AfficherGrilleTransparante();
        }
    }

}
