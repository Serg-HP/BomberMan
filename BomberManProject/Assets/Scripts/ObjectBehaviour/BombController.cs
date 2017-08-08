﻿using Assets.New_Folder.Basis;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class BombController: MonoBehaviour
    {
        private static int explosionLength = 1;
        private float lifespan = 2;
        private SphereCollider collider;
        void Start()
        {
            collider = gameObject.GetComponent<SphereCollider>();
            Invoke("MakeExplosion", lifespan);
            Destroy(collider.gameObject,lifespan);
        }

        private void MakeExplosion()
        {
            List<Ray> rayList = CreateExplosionArea();
            RaycastHit hit;
            ShowExplosion(Vector3.zero,1);
            foreach (Ray ray in rayList)
            {
                if (Physics.Raycast(ray, out hit, explosionLength))
                {
                    if (IsDestroyable(hit))
                    {
                        Destroy(hit.collider.gameObject);
                        ShowExplosion(ray.direction, explosionLength);
                    }
                }
                else
                    ShowExplosion(ray.direction, explosionLength);

            }
        }

        private void ShowExplosion(Vector3 direction, float explosionLength)
        {
            for (int i = 1; i <= explosionLength; i++)
            {
                var position = new Vector3(transform.position.x + direction.x*i, transform.position.y, transform.position.z + direction.z*i);
                GameObject gameobj = Instantiate(EnvironmentTools.GetExplosion(), position, Quaternion.identity);
                Destroy(gameobj, lifespan);
            }
        }

        private List<Ray> CreateExplosionArea()
        {
            List<Ray> rayList = new List<Ray>();
            rayList.Add(new Ray(transform.position, Vector3.left));
            rayList.Add(new Ray(transform.position, Vector3.right));
            rayList.Add(new Ray(transform.position, Vector3.forward));
            rayList.Add(new Ray(transform.position, Vector3.back));
            return rayList;
        }

        private bool IsDestroyable(RaycastHit hit)
        {
            return hit.collider.tag == "BreakWall" || hit.collider.tag == "Enemy" || hit.collider.tag == "Player";
        }

        private void OnTriggerExit(Collider other)
        {
            collider.isTrigger = false;
        }
        
        public static void IncreasePower()
        {
            explosionLength++;
        }

    }
}