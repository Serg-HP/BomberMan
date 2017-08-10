using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.ObjectBehaviour.Bonuses
{
    class WallPassBonus:MonoBehaviour
    {
        public AudioClip audioClip;
        private void OnCollisionEnter(Collision hit)
        {
            var tag = hit.gameObject.tag;
            if (tag == "Player")
            {
                PlayerController.SetWallPassActive();
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
                Destroy(gameObject);
            }
        }
    }
}
