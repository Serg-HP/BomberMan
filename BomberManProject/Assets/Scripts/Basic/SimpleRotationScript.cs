using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotationScript : MonoBehaviour {

    private int rotateSpeedY = 10;
	void Update ()
    {
        transform.Rotate(0, rotateSpeedY, 0, Space.World);
    }
}
