using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Map : MonoBehaviour {

    const int SideDistance = 18;
    const int DiagonalDistance = 31;

    public SelectedCards selectedCard_;

    public GameObject gameControllerObject;
    public GameController gameController;
    public GameObject selectedUnit_;
    public Unit selectedUnitProps_;
    public Vector3 selectedUnitPosition_;
    public UsableSpellsList usableSpellList_;

    // pour dessiner la map
    public bool lineAreHorizontal_;
    public bool afficherGrid_;
    public LayerMask walkableMask_;
    public LayerMask unwalkableMask_;
    public Vector2 mapSize_;
    public float rayonNoeud_;
    public List<GameObject> sectionsGameObject_;
    public Section[] sections_;
    public Noeud[,] map_;
    float diametreNoeud_;
    public int mapSizeX_, mapSizeY_;

    // grille transparante
    public GameObject vert, rouge, bleu, jaune;
    private GameObject[,] grille;

    public Noeud playerTile_;
    public List<Noeud> playerMovingTiles_ = new List<Noeud>(50);
    public List<Noeud> playerAttackTiles_ = new List<Noeud>(50);

    private void Start()
    {
        sections_ = GetComponentsInChildren<Section>();
        sectionsGameObject_ = new List<GameObject>();
        foreach (Section section in sections_)
        {
            sectionsGameObject_.Add(section.gameObject);
            if (section != null)
            {
                section.CreateSections();
            }
        }
        gameController = gameControllerObject.GetComponent<GameController>();
        CreerMap();
        gameController.SetGame();
    }

    private void CreerMap()
    {
        if (lineAreHorizontal_)
        {
            mapSizeX_ = (int)mapSize_.x / (2*SideDistance);
            mapSizeY_ = (int)mapSize_.y / DiagonalDistance;
            if (mapSizeX_ % 2 == 0)
            {
                mapSizeX_--;
            }
            if (mapSizeY_ % 2 == 0)
            {
                mapSizeY_--;
            }
            int distanceX;
            int distanceY;
            bool dispositionColonne = false;
            for (int k = 0; 4* k + 1 <= mapSizeY_; k++)
            {
                if (4 * k + 1 == mapSizeY_)
                {
                    dispositionColonne = true;
                }
            }

            if (dispositionColonne)
            {
                distanceX = (mapSizeX_ - 1) * SideDistance;
            }
            else
            {
                distanceX = (mapSizeX_ - 2) * SideDistance;
            }
            distanceY = (mapSizeY_ / 2) * DiagonalDistance;
            Vector3 mapBottomLeft = transform.position - Vector3.right*distanceX - Vector3.forward *distanceY;

            map_ = new Noeud[mapSizeX_, mapSizeY_];

            for (int x = 0; x < mapSizeX_; x++)
            {
                for (int y = 0; y < mapSizeY_; y++)
                {
                    Vector3 worldPoint;
                    if (y % 2 == 1)
                    {
                        worldPoint = mapBottomLeft + Vector3.right * ((x * 2 - 1) * SideDistance) + Vector3.up * 10 + Vector3.forward * (y * DiagonalDistance);
                    }
                    else
                    {
                        worldPoint = mapBottomLeft + Vector3.right * (x * 2 * SideDistance) + Vector3.up * 10 + Vector3.forward * (y * DiagonalDistance);
                    }
                    // verifie s'il y a une collision
                    Collider[] colliders = Physics.OverlapSphere(worldPoint, rayonNoeud_ - 4);
                    bool walkable = (Physics.CheckSphere(worldPoint, rayonNoeud_ - 4, walkableMask_));
                    if (walkable == true)
                    {
                        walkable = !(Physics.CheckSphere(worldPoint, rayonNoeud_ - 4, unwalkableMask_));
                    }
                    map_[x, y] = new Noeud(walkable, worldPoint, colliders, this, x, y);
                   
                }
            }
        }
        else
        {
            mapSizeY_ = (int)mapSize_.y / (2 * SideDistance);
            mapSizeX_ = (int)mapSize_.x / DiagonalDistance;
            if (mapSizeY_ % 2 == 0)
            {
                mapSizeY_--;
            }
            if (mapSizeX_ % 2 == 0)
            {
                mapSizeX_--;
            }
            int distanceX;
            int distanceY;
            bool dispositionColonne = false;
            for (int k = 0; 4 * k + 1 <= mapSizeX_; k++)
            {
                if (4 * k + 1 == mapSizeX_)
                {
                    dispositionColonne = true;
                }
            }

            if (dispositionColonne)
            {
                distanceY = (mapSizeY_ - 1) * SideDistance;
            }
            else
            {
                distanceY = (mapSizeY_ - 2) * SideDistance;
            }

            distanceX = (mapSizeX_ / 2) * DiagonalDistance;

            Vector3 mapBottomLeft = transform.position - Vector3.right * distanceX - Vector3.forward * distanceY;

            map_ = new Noeud[mapSizeX_, mapSizeY_];

            for (int y = 0; y < mapSizeY_; y++)
            {
                for (int x = 0; x < mapSizeX_; x++)
                {
                    Vector3 worldPoint;
                    if (x % 2 == 1)
                    {
                        worldPoint = mapBottomLeft + Vector3.right * (x * DiagonalDistance) + Vector3.up*10 + Vector3.forward * ((y * 2 - 1) * SideDistance);

                    }
                    else
                    {
                        worldPoint = mapBottomLeft + Vector3.right * (x * DiagonalDistance) + Vector3.up * 10 + Vector3.forward * (y * 2 * SideDistance);
                    }
                    // verifie s'il y a une collision
                    Collider[] colliders = Physics.OverlapSphere(worldPoint, rayonNoeud_ - 4);
                    bool walkable = (Physics.CheckSphere(worldPoint, rayonNoeud_ - 4, walkableMask_));
                    map_[x, y] = new Noeud(walkable, worldPoint, colliders, this, x, y);

                }
            }
        }
        if (selectedUnit_ != null)
        {
            SetPlayer();
        }
    }

    public void SetPlayer()
    {
        foreach (Noeud n in map_)
        {
            if (n.player_ == selectedUnit_)
            {
                selectedUnitPosition_ = PlayerPosition();
                selectedUnitProps_ = selectedUnit_.GetComponent<Unit>();
                n.SetPlayerMovingTiles();
            }
        }
    }

    public void ChangeSelectedUnit(GameObject unit)
    {
        if (selectedUnitProps_ != null)
        {
            playerMovingTiles_.Clear();
            CreerMap();
            selectedUnitProps_.displayingSpells_ = false;
        }
        usableSpellList_.ClearList();
        Destroy(gameController.tampon);
        selectedUnit_ = unit;
        SetPlayer();
        usableSpellList_.SetListSpells();
        gameController.SetSelectedCards();
    }

    public Vector3 PlayerPosition()
    {
        for (int x = 0; x < mapSizeX_; x++)
        {
            for (int y = 0; y < mapSizeY_; y++)
            {
                if (map_[x, y].player_ == selectedUnit_)
                {
                    return map_[x, y].worldPosition_;
                }
            }
        }
        Debug.Log("Selected player not found!");
        return new Vector3(0, 0, 0);
    }

    public void MoveSelectedUnitTo(int x, int y)
    {
        selectedUnit_.transform.position = new Vector3(map_[x, y].worldPosition_.x, 20, map_[x,y].worldPosition_.z);
        if (map_[x,y].clickableTile.gameObject.tag == "Door")
        {
            foreach (Section section in sections_)
            {
                foreach (ClickableTile ct in section.tiles_)
                {
                    if (ct.posX_ == map_[x, y].worldPosition_.x && ct.posY_ == map_[x,y].worldPosition_.z && !section.isRevealed)
                    {
                        section.Reveal();
                    }
                }
            }
        }
        if (map_[x,y].trap_ != null && selectedUnitProps_.isWalking_)
        {
            selectedUnitProps_.health_ -= map_[x, y].trap_.trapDamage_;
            TrapDestroy(x,y);
        }
        playerMovingTiles_.Clear();
        CreerMap();
        //playerTile_.SetPlayerMovingTiles();
    }

    public List<Noeud> GetNeighbours(Noeud noeud)
    {
        List<Noeud> neighbours = new List<Noeud>();

        if (lineAreHorizontal_)
        {
            for (int i = -1; i <= 1; i++)
            {
                if (noeud.y_ + i < mapSizeY_ && noeud.y_ + i >= 0)
                {
                    if (i != 0)
                    {
                        int valeurTest;
                        if (noeud.y_ + 1 < mapSizeY_)
                        {
                            valeurTest = noeud.y_ + 1;
                        }
                        else
                        {
                            valeurTest = noeud.y_ - 1;
                        }
                        if (noeud.worldPosition_.x <= map_[noeud.x_, valeurTest].worldPosition_.x)
                        {
                            for (int j = -1; j <= 0; j++)
                            {
                                if (noeud.x_ + j >= 0 && noeud.x_ + j < mapSizeX_)
                                {
                                    neighbours.Add(map_[noeud.x_ + j, noeud.y_ + i]);
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j <= 1; j++)
                            {
                                if (noeud.x_ + j >= 0 && noeud.x_ + j < mapSizeX_)
                                {
                                    neighbours.Add(map_[noeud.x_ + j, noeud.y_ + i]);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (noeud.x_ - 1 >= 0)
                        {
                            neighbours.Add(map_[noeud.x_ - 1, noeud.y_]);
                        }
                        if (noeud.x_ + 1 < mapSizeX_)
                        {
                            neighbours.Add(map_[noeud.x_ + 1, noeud.y_]);
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = -1; i <= 1; i++)
            {
                if (noeud.x_ + i < mapSizeX_ && noeud.x_ + i >= 0)
                {
                    if (i != 0)
                    {
                        int valeurTest;
                        if (noeud.x_ + 1 < mapSizeX_)
                        {
                            valeurTest = noeud.x_ + 1;
                        }
                        else
                        {
                            valeurTest = noeud.x_ - 1;
                        }
                        if (noeud.worldPosition_.z <= map_[valeurTest, noeud.y_].worldPosition_.z)
                        {
                            for (int j = -1; j <= 0; j++)
                            {
                                if (noeud.y_ + j >= 0 && noeud.y_ + j < mapSizeY_)
                                {
                                    neighbours.Add(map_[noeud.x_ + i, noeud.y_ + j]);
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j <= 1; j++)
                            {
                                if (noeud.y_ + j >= 0 && noeud.y_ + j < mapSizeY_)
                                {
                                    neighbours.Add(map_[noeud.x_ + i, noeud.y_ + j]);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (noeud.y_ - 1 >= 0)
                        {
                            neighbours.Add(map_[noeud.x_, noeud.y_ - 1]);
                        }
                        if (noeud.y_ + 1 < mapSizeY_)
                        { 
                            neighbours.Add(map_[noeud.x_, noeud.y_ + 1]);
                        }
                    }
                }
            }
        }

        return neighbours;
    }

    public void TrapDestroy(int x, int y)
    {
        Destroy(map_[x, y].trapGO_);
        map_[x, y].trap_ = null;
    }

    public void AfficherGrilleTransparante()
    {
        if (grille != null) {
            foreach (GameObject gm in grille)
            {
                Destroy(gm);
            }
        }
        grille = new GameObject[mapSizeX_, mapSizeY_];
        if (map_ != null)
        {
            foreach (Noeud n in map_)
            {
                if (playerMovingTiles_.Contains(n))
                {
                    if (lineAreHorizontal_)
                    {
                        Transform transform = this.GetComponent<Transform>();
                        grille[n.x_, n.y_] = Instantiate(bleu, new Vector3(n.worldPosition_.x, 0, n.worldPosition_.z), transform.rotation);
                    }
                    else
                    {
                        grille[n.x_, n.y_] = Instantiate(bleu, new Vector3(n.worldPosition_.x, 0, n.worldPosition_.z), Quaternion.identity);
                    }
                }
            }
        }

    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(mapSize_.x, 1, mapSize_.y));
        if (map_ != null && afficherGrid_)
        {
            foreach (Noeud n in map_)
            {
                Gizmos.color = Color.white;
                if (playerMovingTiles_.Contains(n))
                {
                    Gizmos.color = Color.yellow;
                }
                /*
                else if (!n.walkable_)
                {
                    Gizmos.color = Color.red;
                }
                */
                //Gizmos.DrawCube(n.worldPosition_, Vector3.one * (diametreNoeud_ - 4));
                if (!n.walkable_)
                {
                    Gizmos.color = Color.red;
                }
                
                Gizmos.DrawSphere(n.worldPosition_, rayonNoeud_);
            }
        }
    }

}

