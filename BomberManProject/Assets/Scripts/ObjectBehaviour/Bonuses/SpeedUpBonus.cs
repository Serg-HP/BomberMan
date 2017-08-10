using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ObjectBehaviour
{
    class SpeedUpBonus: MonoBehaviour
    {
        public AudioClip audioClip;
        private void OnCollisionEnter(Collision hit)
        {
            var tag = hit.gameObject.tag;
            if (tag == "Player")
            {
                GameObject go = GameObject.FindGameObjectWithTag("Player");
                PlayerController other = (PlayerController)go.GetComponent(typeof(PlayerController));
                other.IncreaseSpeed();
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
                Destroy(gameObject);
            }
        }
    }
}
