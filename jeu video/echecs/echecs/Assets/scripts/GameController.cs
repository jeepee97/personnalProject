using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public bool playerTurn;
    public bool playerColor = true; //true = blanc, flase = noir
    public List<Piece> piecesBlanches = new List<Piece>();
    public List<Piece> piecesNoires = new List<Piece>();
    public List<GameObject> ensembleDesPieces;
    List<Piece> playerPieces = new List<Piece>();
    List<Piece> computerPieces = new List<Piece>();
    List<Coup> coupPossibles = new List<Coup>();

	// Use this for initialization
	void Start () {
        separerLesPieces();
        initialiserBoard();
        playerTurn = playerColor;
        if (playerColor)
        {
            playerPieces = piecesBlanches;
            computerPieces = piecesNoires;
        }
        else
        {
            playerPieces = piecesNoires;
            computerPieces = piecesBlanches;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void NextPlayer()
    {
        playerTurn = !playerTurn;
        if (playerTurn)
        {
            
        }
        else
        {
            ComputerTurn();
        }
    }

    void ComputerTurn()
    {
        // reinitialiser coupPossibles
        coupPossibles = null;
        coupPossibles = new List<Coup>();

        foreach(Piece p in computerPieces)
        {
            List<int[]> deplacement;
            deplacement = p.GetDeplacement();
            foreach(int[] d in deplacement)
            {

            }
        }
    }

    void separerLesPieces()
    {
        foreach(GameObject g in ensembleDesPieces)
        {
            // pion
            Pion gPion = g.GetComponent<Pion>();
            if (gPion != null)
            {
                if (gPion.blanche)
                {
                    piecesBlanches.Add(gPion);
                }
                else
                {
                    piecesNoires.Add(gPion);
                }
            }
            // tour
            Tour gTour = g.GetComponent<Tour>();
            if (gTour != null)
            {
                if (gTour.blanche)
                {
                    piecesBlanches.Add(gTour);
                }
                else
                {
                    piecesNoires.Add(gTour);
                }
            }
            // cavalier
            Cavalier gCavalier = g.GetComponent<Cavalier>();
            if (gCavalier != null)
            {
                if (gCavalier.blanche)
                {
                    piecesBlanches.Add(gCavalier);
                }
                else
                {
                    piecesNoires.Add(gCavalier);
                }
            }
            // fou
            Fou gFou = g.GetComponent<Fou>();
            if (gFou != null)
            {
                if (gFou.blanche)
                {
                    piecesBlanches.Add(gFou);
                }
                else
                {
                    piecesNoires.Add(gFou);
                }
            }
            // reine
            Reine gReine = g.GetComponent<Reine>();
            if (gReine != null)
            {
                if (gReine.blanche)
                {
                    piecesBlanches.Add(gReine);
                }
                else
                {
                    piecesNoires.Add(gReine);
                }
            }
            // roi
            Roi gRoi = g.GetComponent<Roi>();
            if (gRoi != null)
            {
                if (gRoi.blanche)
                {
                    piecesBlanches.Add(gRoi);
                }
                else
                {
                    piecesNoires.Add(gRoi);
                }
            }
        }
    }

    void initialiserBoard()
    {
        initialiserPions();
        initialiserTours();
        initialiserCavalier();
        initialiserFou();
        initialiserReine();
        initialiserRoi();
    }
    void initialiserPions()
    {
        int compteur = 1;
        foreach (Pion p in piecesBlanches)
        {
            p.posX = compteur;
            p.posY = 2;
            compteur++;
        }
        compteur = 1;
        foreach (Pion p in piecesNoires)
        {
            p.posX = compteur;
            p.posY = 7;
            compteur++;
        }
    }
    void initialiserTours()
    {
        int compteur = 1;
        foreach (Tour p in piecesBlanches)
        {
            if (compteur == 1)
            {
                p.posX = 1;
            }
            else
            {
                p.posX = 8;
            }
            p.posY = 1;
            compteur++;
        }
        compteur = 1;
        foreach (Tour p in piecesBlanches)
        {
            if (compteur == 1)
            {
                p.posX = 1;
            }
            else
            {
                p.posX = 8;
            }
            p.posY = 8;
            compteur++;
        }
    }
    void initialiserCavalier()
    {
        int compteur = 1;
        foreach(Cavalier p in piecesBlanches)
        {
            if (compteur == 1)
            {
                p.posX = 2;
            }
            else
            {
                p.posX = 7;
            }
            p.posY = 1;
            compteur++;
        }
        compteur = 1;
        foreach(Cavalier p in piecesNoires)
        {
            if (compteur == 1)
            {
                p.posX = 2;
            }
            else
            {
                p.posX = 7;
            }
            p.posY = 8;
            compteur++;
        }

    }
    void initialiserFou()
    {
        int compteur = 1;
        foreach(Fou p in piecesBlanches)
        {
            if (compteur == 1)
            {
                p.posX = 3;
            }
            else
            {
                p.posX = 6;
            }
            p.posY = 1;
            compteur++;
        }
        compteur = 1;
        foreach(Fou p in piecesNoires)
        {
            if (compteur == 1)
            {
                p.posX = 3;
            }
            else
            {
                p.posY = 6;
            }
            p.posY = 8;
            compteur++;
        }
    }
    void initialiserReine()
    {
        foreach(Reine p in piecesBlanches)
        {
            p.posX = 4;
            p.posY = 1;
        }
        foreach(Reine p in piecesNoires)
        {
            p.posX = 4;
            p.posY = 8;
        }
    }
    void initialiserRoi()
    {
        foreach(Roi p in piecesBlanches)
        {
            p.posX = 5;
            p.posY = 1;
        }
        foreach(Roi p in piecesNoires)
        {
            p.posX = 5;
            p.posY = 8;
        }
    }
}
