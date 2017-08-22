using Assets.New_Folder.Basis;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : SimpleMoving
{
    private AudioSource source;
    public AudioClip setBombSound;
    public AudioClip flySound;
    public AudioClip deathSound;
    private static int bombLimit;
    private static float speedLimit = 0.25f;
    private static bool wallPass;
    private static bool isAlive=true;


    void Start ()
    {
        bombLimit = 0;
        speed = 0.1f;
        wallPass = false;
        source = gameObject.GetComponent<AudioSource>();
    }
    private void OnDestroy()
    {
        var resultText = GameObject.Find("ResultText").GetComponent<Text>();
        resultText.text = "GameOver!!!";
        Application.Quit();
    }

    private void MakeAction()
    {
        Animator animatorPlayer = gameObject.GetComponent<Animator>();
        if (Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animatorPlayer.SetTrigger("SetBomb");
                PutBomb();
            }
            else
            {
                //animatorPlayer.SetTrigger("IsWalk");
                animatorPlayer.SetFloat("Walk", 1);
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    MoveLeft();
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    MoveRight();
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    MoveUp();
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    MoveDown();
                }
            }
        }
        else
        {
            animatorPlayer.SetFloat("Walk", 0);
        }
    }
    private void PutBomb()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Bomb");
        if (go.Length <= bombLimit)
        {
            Vector3 bombposition = new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z));
            if (wallPass == true)
            {
                Collider[] hitColliders = Physics.OverlapSphere(bombposition, 0.1f);
                if (hitColliders.Length > 1)
                    return;
            }
            Instantiate(EnvironmentTools.GetBomb(), bombposition, Quaternion.identity);
        }
    }
	void Update ()
    {
        if (isAlive)
            MakeAction();
    }
    public static void IncreaseBombNumber()
    {
        bombLimit++;
    }
    public void IncreaseSpeed()
    {
        if (speed < speedLimit)
            speed +=0.05f;
    }
    public static void SetWallPassActive()
    {
        wallPass = true;
    }

    public static void SetDead()
    {
        isAlive = false;
        
    }

    private void PlayFlySound()
    {
        source.PlayOneShot(flySound);
    }
    private void PlaySetBombSound()
    {
        source.PlayOneShot(setBombSound);
    }
    private void PlayDeathSound()
    {
        source.PlayOneShot(deathSound);
    }
    void OnCollisionEnter(Collision hit)
    {
        var tag = hit.gameObject.tag;
        if (tag== "BreakWall")
            if (wallPass)
                Physics.IgnoreCollision(hit.collider, gameObject.GetComponent<CapsuleCollider>());
    }

}
