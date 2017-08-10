using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.PathFinding
{
    class Grid:MonoBehaviour
    {
        public Node[,] grid;
        public int gridSizeX;
        public int gridSizeY;

        public Node NodeFromGrid(Vector3 position)
        {
            int x = Mathf.RoundToInt(position.x);
            int z = Mathf.RoundToInt(position.z);

            return grid[x, z];
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();
            for (int i=-1; i<=1; i++)
                for (int j =-1; j<=-1; j++)
                {
                    if ((i == 0 && j == 0) || (i != 0 && j != 0))
                        continue;

                    int checkX = node.gridX + i;
                    int checkY = node.gridY + j;

                    if (checkX < gridSizeX && checkY < gridSizeY && checkX >= 0 && checkY >= 0)
                        neighbours.Add(grid[checkX, checkY]);
                }
            return neighbours;
        }

    }
}
