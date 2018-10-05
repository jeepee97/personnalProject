using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : IHeapItem<Node>{

    public bool walkable_;
    public Vector3 worldPosition_;
    public int gridX_;
    public int gridY_;
    public int movementPenalty;

    public int gCost;
    public int hCost;
    public Node parent;
    int HeapIndex;

    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY, int _penalty)
    {
        movementPenalty = _penalty;
        walkable_ = walkable;
        worldPosition_ = worldPosition;
        gridX_ = gridX;
        gridY_ = gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int heapIndex
    {
        get
        {
            return HeapIndex;
        }
        set
        {
            HeapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
	
}
