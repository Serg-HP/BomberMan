using Assets.Scripts.Basic;
using Assets.Scripts.PathFinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ObjectBehaviour
{
    class SmartEnemyController: EnemyController
    {
        private GameObject player;
        private List<Vector3> smartPath;
        private bool Moving = false;
        private bool isRandom;
        private int iterator = 0;
        private int width;
        private int length;
        private void Start()
        {
            Animator animatorEnemy = gameObject.GetComponent<Animator>();
            animatorEnemy.SetFloat("Walk", 1);
            source = gameObject.GetComponent<AudioSource>();
            speed = 0.01f;
            player = GameObject.FindGameObjectWithTag("Player");
            direction = 0;
            GetParameters();
        }
        private void Update()
        {
            GetPath();
            if (smartPath.Count != 0)
            {
                Moving = true;
                isRandom = false;
                if (Moving)
                    StartCoroutine("onCoroutine");
            }
            else
            {
                isRandom = true;
                MakeAction();
            }
        }

        private void GetParameters()
        {
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            MapCreator creator = camera.GetComponent<MapCreator>();
            width = creator.width;
            length = creator.length;
        }
        private void GetPath()
        {
            if (player != null && gameObject != null)
            {
                Grid grid = new Grid(width, length);
                Astar.FindPath(grid, transform.position, player.transform.position);
                smartPath = ConvertToPosition(grid.path);
            }
            else
            {
                smartPath.Clear();
            }
        }

        private List<Vector3> ConvertToPosition(List<Node> path)
        {
            List<Vector3> smartPath = new List<Vector3>();
            foreach (Node node in path)
                smartPath.Add(new Vector3(node.gridX, 0, node.gridY));
            return smartPath;
        }

        IEnumerator onCoroutine()
        {
            while (iterator < smartPath.Count)
            {
                Vector3 endPos = new Vector3(smartPath[iterator].x, 0, smartPath[iterator].z);
                MakeMove(transform.position, endPos);
                if (transform.position == endPos)
                    iterator++;
                yield return new WaitForSeconds(0.5f);
            }
            if (iterator >= smartPath.Count)
                Moving = false;
        }

        void OnCollisionEnter(Collision hit)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));
            var tag = hit.gameObject.tag;
            if (isRandom)
            {
                if (tag == "BreakWall" || tag == "ConcreteWall" || tag == "Bomb" || tag == "Enemy")
                    direction = Random.Range(0, 4);
            }
            if (tag == "Player")
            {
                KillPlayer(hit);
                StopCoroutine("onCoroutine");
            }
        }

        private void MakeMove(Vector3 currentPosition, Vector3 next_Position)
        {
            Vector3 substract = next_Position - currentPosition;
            substract.y = 0;
            if (substract.x<0)
            {
                MoveLeft();
            }
            if (substract.x>0)
            {
                MoveRight();
            }
            if (substract.z<0)
            {
                MoveDown();
            }
            if (substract.z>0)
            {
                MoveUp();
            }
        }

    }
}
