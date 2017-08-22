using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ObjectBehaviour
{
    class ExitController: MonoBehaviour
    {
        private Collider collider;
        private GameObject[] enemyCount;
        private Text resultText;
        private void Start()
        {
            resultText = GameObject.Find("ResultText").GetComponent<Text>();
            collider = gameObject.GetComponent<Collider>();
            collider.enabled = false;
        }
        private void Update()
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemyCount.Length == 0)
            {
                collider.enabled = true;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.tag == "Player")
            {
                resultText.text = "Victory!!!";
                Destroy(gameObject);
                Application.Quit();
            }
        }
    }
}
