using Assets.New_Folder.Basis;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : SimpleMoving
{
    public AudioClip pickUpSound;

    private int bombLimit;
    private float speedLimit = 0.25f;
    private bool wallPass;

    private Text stateText;
    private AudioSource source;
    private float volume = 1;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start ()
    {
        stateText = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
        bombLimit = 0;
        speed = 0.1f;
        wallPass = false;
        //rb = GetComponent<Rigidbody>();
    }

    private void MakeAction()
    {
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PutBomb();
        }
    }
    private void PutBomb()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Bomb");
        if (go.Length <= bombLimit)
        {
            Vector3 bombposition = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, Mathf.RoundToInt(transform.position.z));
            Instantiate(EnvironmentTools.GetBomb(), bombposition, Quaternion.identity);
        }
    }
	void Update ()
    {
        MakeAction();
    }
    public void IncreaseBombNumber()
    {
        bombLimit++;
    }
    public void IncreaseSpeed()
    {
        speed+=0.05f;
    }
    void OnCollisionEnter(Collision hit)
    {
        var tag = hit.gameObject.tag;
        switch (tag)
        {
            case "BombUp":
                IncreaseBombNumber();
                Destroy(hit.gameObject);
                StartCoroutine(ShowBonus(tag));
                break;
            case "FlameUp":
                BombController.IncreasePower();
                Destroy(hit.gameObject);
                StartCoroutine(ShowBonus(tag));
                break;
            case "SpeedUp":
                if (speed<speedLimit)
                    IncreaseSpeed();
                Destroy(hit.gameObject);
                StartCoroutine(ShowBonus(tag));
                break;
            case "WallPass":
                wallPass = true;
                Destroy(hit.gameObject);
                StartCoroutine(ShowBonus(tag));
                break;
            case "BreakWall":
                if (wallPass)
                    Physics.IgnoreCollision(hit.collider, GetComponent<SphereCollider>()); 
                break;
        }
    }

    IEnumerator ShowBonus(string tag)
    {
        source.PlayOneShot(pickUpSound, volume);
        stateText.enabled = true;
        stateText.text = "You got " + tag;
        yield return new WaitForSeconds(2);
        stateText.enabled = false;
        
    }

}
