using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.PathFinding
{
    class Node
    {
        public bool walkable;
        public int gridX;
        public int gridY;

        public int gCost;
        public int hCost;
        public Node parent;
        public Node(bool _walkable, int _gridX, int _gridY)
        {
            walkable = _walkable;
            gridX = _gridX;
            gridY = _gridY;
        }
        public int fCost
        {
            get { return gCost + hCost; }
        }
    }
}
