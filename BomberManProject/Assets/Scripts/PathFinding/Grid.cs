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
        public List<Node> path;

        public Grid(int _gridSizeX, int _gridSizeY)
        {
            gridSizeX = _gridSizeX;
            gridSizeY = _gridSizeY;
            grid = CreateGrid();
            path = new List<Node>();
        }

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
                for (int j =-1; j<=1; j++)
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

       public Node[,] CreateGrid()
        {
            grid = new Node[gridSizeX, gridSizeY];

            for (int x=0; x<gridSizeX; x++)
                for (int y = 0; y < gridSizeY; y++)
                    grid[x, y] = new Node(true, x, y);
            MarkObjects("BreakWall");
            MarkObjects("ConcreteWall");
            MarkObjects("Bomb");

            return grid;
        }

        private void MarkObjects(string tag)
        {
            GameObject[] objectArray = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objectArray)
            {
                int x = Mathf.RoundToInt(obj.transform.position.x);
                int y = Mathf.RoundToInt(obj.transform.position.z);
                grid[x, y] = new Node(false, x, y);
            }
        }

    }
}
