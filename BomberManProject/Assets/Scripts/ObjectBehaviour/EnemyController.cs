using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : SimpleMoving
{
    protected int direction;
    protected bool delay = false;
    public AudioClip hitSound;
    public AudioClip deathSound;
    protected AudioSource source;
    void Start ()
    {
        direction = 0;
        speed = 0.05f;
        Animator animatorEnemy = gameObject.GetComponent<Animator>();
        animatorEnemy.SetFloat("Walk", 1);
        source = gameObject.GetComponent<AudioSource>();
    }
    protected void MakeAction()
    {
        switch (direction)
        {
            case 0:
                MoveLeft();
                break;
            case 1:
                MoveRight();
                break;
            case 2:
                MoveUp();
                break;
            case 3:
                MoveDown();
                break;

        }
    }
    void Update ()
    {
        MakeAction();
    }
    void OnCollisionEnter(Collision hit)
    {
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));
        var tag = hit.gameObject.tag;
        if (tag == "BreakWall" || tag == "ConcreteWall" || tag == "Bomb" || tag == "Enemy")
           direction = Random.Range(0, 4);
        if (tag == "Player")
        {
            KillPlayer(hit);
        }
    }
    protected void KillPlayer(Collision hit)
    {
        Animator animatorEnemy = gameObject.GetComponent<Animator>();
        Animator animatorPlayer = hit.gameObject.GetComponent<Animator>();
        PlayerController.SetDead();
        animatorEnemy.speed = 1.5f;
        animatorEnemy.SetTrigger("IsAttack");
        animatorPlayer.speed = 0.5f;
        animatorPlayer.SetTrigger("IsDead");
        StartCoroutine(Delay());
        Destroy(hit.gameObject, 1);
    }
    protected IEnumerator Delay()
    {
        enabled = false;
        yield return new WaitForSeconds(2);
        enabled = true;
    }
    private void PlayDeathSound()
    {
        source.PlayOneShot(deathSound);
    }
    private void PlayHitSound()
    {
        source.PlayOneShot(hitSound);
    }
}
