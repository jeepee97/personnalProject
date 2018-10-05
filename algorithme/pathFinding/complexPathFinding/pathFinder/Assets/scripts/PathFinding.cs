using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFinding : MonoBehaviour {
    //pas besoin avec PathRequestManager
    //public Transform seeker, target;

    PathRequestManager requestManager;
    Gride grid;

    private void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Gride>();
    }

    /* plus necessaire si on utilise le pathRequestManagemer
    private void Update()
    {
        FindPath(seeker.position, target.position);
    }
    */
    public void startFindPath(Vector3 start, Vector3 target)
    {
        StartCoroutine(FindPath(start, target));
    }
    
    // changer le void pour IEnumerator si on utilise PathRequestManager
    IEnumerator FindPath(Vector3 startPosition, Vector3 endPosition)
    {
        Node startNode = grid.NodeFromWorldPoint(startPosition);
        Node endNode = grid.NodeFromWorldPoint(endPosition);

        Vector3[] wayPoints = new Vector3[0];
        bool pathSuccess = false;

        Heap<Node> openSet = new Heap<Node>(grid.maxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.removeFirst();
            /* version lente
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || 
                    (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            */
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                pathSuccess = true;
                break;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable_ || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour) + neighbour.movementPenalty;
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = getDistance(neighbour, endNode);
                    neighbour.parent = currentNode;
                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                    else
                    {
                        openSet.updateItem(neighbour);
                    }
                }

            }
        }
        yield return null;
        if (pathSuccess)
        {
            wayPoints = RetracePath(startNode, endNode);
        }
        requestManager.finishedProcessingPath(wayPoints, pathSuccess);
    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifiedPath(path);
        Array.Reverse(waypoints);
        return waypoints;
        
    }

    Vector3[] SimplifiedPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX_ - path[i].gridX_,
                                               path[i - 1].gridY_ - path[i].gridY_);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPosition_);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int getDistance(Node a, Node b)
    {
        int distX = Mathf.Abs(a.gridX_ - b.gridX_);
        int distY = Mathf.Abs(a.gridY_ - b.gridY_);
        
        if (distX > distY)
        {
            return (14 * distY + 10 * (distX - distY));
        }
        return (14 * distX + 10 * (distY - distX));
    }
}
