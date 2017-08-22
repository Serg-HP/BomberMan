using Assets.New_Folder.Basis;
using Assets.Scripts;
using Assets.Scripts.Basic;
using Assets.Scripts.ObjectBehaviour.Bonuses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombUpBonus : Bonus {

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
            PlayerController.IncreaseBombNumber();
            Destroy(gameObject,3);
        }
    }

}
