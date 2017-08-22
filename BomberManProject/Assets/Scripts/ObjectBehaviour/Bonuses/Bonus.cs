using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ObjectBehaviour.Bonuses
{
    abstract public class Bonus:MonoBehaviour
    {
        private Vector3 bonusPosition;
        private Collider collider;
        private bool destroyed;
        void Start()
        {
            collider = gameObject.GetComponent(typeof(Collider)) as Collider;
            collider.enabled = false;
            destroyed = false;
            bonusPosition = new Vector3(transform.position.x, 0, transform.position.z);
        }
        void Update()
        {
            Collider[] hitColliders = Physics.OverlapSphere(bonusPosition, 0.1f);
            if (hitColliders.Length == 0 && !destroyed)
            {
                collider.enabled = true;
                destroyed = true;
            }  
        }

        public IEnumerator ShowBonus(string tag, AudioClip audioClip, GameObject gameObj)
        {
            float lifelong = 2;
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            Text stateText = GameObject.Find("StateText").GetComponent<Text>();
            stateText.enabled = true;
            stateText.text = "You got " + tag;
            yield return new WaitForSeconds(lifelong);
            stateText.enabled = false;
        }

        public IEnumerator AnimatePlayerPosition(GameObject gameObj, GameObject player)
        {
            while (true)
            {
                var position = new Vector3(player.transform.position.x, 2, player.transform.position.z);
                GameObject animation = Instantiate(gameObj, position, Quaternion.identity);
                Destroy(animation, 1);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
