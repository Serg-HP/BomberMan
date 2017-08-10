using Assets.Scripts;
using Assets.Scripts.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombUpBonus : MonoBehaviour {

    public AudioClip audioClip;
    private void OnCollisionEnter(Collision hit)
    {
        var tag = hit.gameObject.tag;
        if (tag == "Player")
        {
            PlayerController.IncreaseBombNumber();
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            Destroy(gameObject);
        }
    }

}
