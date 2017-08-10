using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ObjectBehaviour
{
    class FlameUpBonus:MonoBehaviour
    {
        public AudioClip audioClip;
        private void OnCollisionEnter(Collision hit)
        {
            var tag = hit.gameObject.tag;
            if (tag == "Player")
            {
                BombController.IncreasePower();
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
                Destroy(gameObject);
            }
        }
    }
}
