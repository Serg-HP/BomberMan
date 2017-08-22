using Assets.Scripts.Basic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.PathFinding
{
    class PathFinder: MonoBehaviour
    {
        public int width;
        public int length;
        private GameObject player;
        private List<Vector3> smartPath;
        private float speed = 0.1f;
        private bool Moving = false;
        private int iterator = 0;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            FindPath();
        }

        private void FindPath()
        {
            Grid grid = new Grid(width,length);
            Astar.FindPath(grid,transform.position,player.transform.position);
            smartPath = ConvertToPosition(grid.path);
            Moving = true;
            if (Moving)
                StartCoroutine(onCoroutine());
        }

        private List<Vector3> ConvertToPosition(List<Node> path)
        {
            List<Vector3> smartPath = new List<Vector3>();
            foreach(Node node in path)
                smartPath.Add(new Vector3(node.gridX, 0, node.gridY));
            return smartPath;
        }

        IEnumerator onCoroutine()
        {
            while (iterator < smartPath.Count)
            {
                Vector3 endPos = new Vector3(smartPath[iterator].x, 0, smartPath[iterator].z);
                transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
                if (transform.position == endPos)
                    iterator++;
                yield return new WaitForSeconds(0.5f);
            }
            if (iterator >= smartPath.Count)
                Moving = false;
        }
    }
}
