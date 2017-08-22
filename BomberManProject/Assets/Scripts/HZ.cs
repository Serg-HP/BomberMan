using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HZ : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            Animator anime = gameObject.GetComponent<Animator>();
            anime.SetTrigger("SetBomb");
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Animator anime = gameObject.GetComponent<Animator>();
            anime.SetFloat("Walk",1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Animator anime = gameObject.GetComponent<Animator>();
            anime.SetTrigger("IsDead");
        }
    }
}
