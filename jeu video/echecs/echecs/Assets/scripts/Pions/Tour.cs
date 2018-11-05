using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tour : Piece {
    public override List<int[]> GetDeplacement()
    {
        List<int[]> ensemble = new List<int[]>();
        for (int i = 1; i <= 8; i++)
        {
            if (i != posX)
            {
                int[] pos = { i, posY };
                ensemble.Add(pos);
            }
        }
        for (int i = 1; i <= 8; i++)
        {
            if (i != posY)
            {
                int[] pos = { posX, i };
                ensemble.Add(pos);
            }
        }
        return ensemble;
    }

}
