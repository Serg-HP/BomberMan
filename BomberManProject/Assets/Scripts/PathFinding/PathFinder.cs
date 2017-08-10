using Assets.Scripts.PathFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Basic
{
    class PathFinder
    {
        Grid grid;
        void FindPath(Vector3 startPos, Vector3 endPos)
        {
            Node startNode = grid.NodeFromGrid(startPos);
            Node endNode = grid.NodeFromGrid(endPos);

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count> 0)
            {
                Node currentNode = openSet[0];
                for (int i=1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost< currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost<currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == endNode)
                {
                    return;
                }

                foreach(Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(currentNode))
                        continue;
                }
            }
        }

        void GetDistance(Node nodeA, Node nodeB)
        {
            int dstX = nodeB.gridX - nodeB.gridX;
        }

    }
}
