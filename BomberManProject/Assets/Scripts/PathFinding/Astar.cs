using Assets.Scripts.PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Basic
{
    class Astar
    {
        public static void FindPath(Grid grid,Vector3 startPos, Vector3 endPos)
        {
            Node startNode = grid.NodeFromGrid(startPos);
            Node endNode = grid.NodeFromGrid(endPos);

            List<Node> openSet = new List<Node>();
            List<Node> closedSet = new List<Node>();
            openSet.Add(startNode);

            while (openSet.Count> 0)
            {
                Node currentNode = openSet[0];
                for (int i=1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost< currentNode.fCost || 
                        openSet[i].fCost == currentNode.fCost && openSet[i].hCost<currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == endNode)
                {
                    RetracePath(grid,startNode, endNode);
                    return;
                }

                foreach(Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                        continue;
                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour<neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, endNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }

        static void RetracePath(Grid grid, Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            path.Reverse();
            grid.path = path;
        }

        static int GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = nodeB.gridX - nodeA.gridX;
            int dstY = nodeB.gridY - nodeA.gridY;
            return Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(dstX, 2) + Mathf.Pow(dstY, 2)));
        }

    }
}
