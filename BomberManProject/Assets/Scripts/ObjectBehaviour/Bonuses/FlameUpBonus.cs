using Assets.Scripts.ObjectBehaviour.Bonuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ObjectBehaviour
{
    class FlameUpBonus:Bonus
    {
        public AudioClip audioClip;
        public GameObject gameObj;
        private void OnCollisionEnter(Collision hit)
        {
            var tag = hit.gameObject.tag;
            if (tag == "Player")
            {
                StartCoroutine(ShowBonus(gameObject.tag, audioClip, gameObj));
                StartCoroutine(AnimatePlayerPosition(gameObj, hit.gameObject));
                (gameObject.GetComponent(typeof(Collider)) as Collider).enabled = false;
                (gameObject.GetComponent(typeof(MeshRenderer)) as MeshRenderer).enabled = false;
                BombController.IncreasePower();
                Destroy(gameObject, 3);
            }
        }
    }
}
