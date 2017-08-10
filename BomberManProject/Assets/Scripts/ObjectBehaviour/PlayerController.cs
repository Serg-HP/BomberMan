using Assets.New_Folder.Basis;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : SimpleMoving
{
    private static int bombLimit;
    private static float speedLimit = 0.25f;
    private static bool wallPass;

    private Text stateText;

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
    void OnCollisionEnter(Collision hit)
    {
        var tag = hit.gameObject.tag;
        GameObject gameobj;
        var position = new Vector3(transform.position.x, 2, transform.position.z);
        switch (tag)
        {
            case "BombUp":
                StartCoroutine(ShowBonus(tag));
                gameobj = Instantiate(EnvironmentTools.GetBombUpEffect(), position, Quaternion.identity);
                Destroy(gameobj, 2);
                break;
            case "FlameUp":
                StartCoroutine(ShowBonus(tag));
                gameobj = Instantiate(EnvironmentTools.GetFlameUpEffect(), position, Quaternion.identity);
                Destroy(gameobj, 2);
                break;
            case "SpeedUp":
                StartCoroutine(ShowBonus(tag));
                gameobj = Instantiate(EnvironmentTools.GetSpeedUpEffect(), position, Quaternion.identity);
                Destroy(gameobj, 2);
                break;
            case "WallPass":
                StartCoroutine(ShowBonus(tag));
                gameobj = Instantiate(EnvironmentTools.GetWallPassEffect(), position, Quaternion.identity);
                Destroy(gameobj, 2);
                break;
            case "BreakWall":
                if (wallPass)
                    Physics.IgnoreCollision(hit.collider, gameObject.GetComponent<CapsuleCollider>());
                break;
        }
    }
    private IEnumerator ShowBonus(string tag)
    {
        stateText.enabled = true;
        stateText.text = "You got " + tag;
        yield return new WaitForSeconds(2);
        stateText.enabled = false;
    }

}
