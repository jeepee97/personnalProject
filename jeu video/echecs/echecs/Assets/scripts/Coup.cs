using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coup {
    Piece pieceChoisi;
    int posX, posY;

    Coup(Piece pieceChoisi_, int posX_, int posY_)
    {
        pieceChoisi = pieceChoisi_;
        posX = posX_;
        posY = posY_;
    }
}
