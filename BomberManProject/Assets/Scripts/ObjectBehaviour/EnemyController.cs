using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : SimpleMoving
{
    private int direction;
    void Start ()
    {
        direction = 0;
        speed = 0.1f;
    }
    private void MakeAction()
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
        var tag = hit.gameObject.tag;
        if (tag == "BreakWall" || tag == "ConcreteWall" || tag == "Bomb")
           direction = Random.Range(0, 4);
        if (tag == "Player")
            Destroy(hit.gameObject);
    }
}
