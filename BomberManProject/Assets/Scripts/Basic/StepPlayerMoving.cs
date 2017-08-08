using Assets.Scripts.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepPlayerMoving : BasicMoving {
    
    public Rigidbody rb;

    public override void Move(Vector3 direction, int rotation)
    {
        Vector3 endpoint = transform.position;
        transform.rotation = Quaternion.Euler(0, rotation, 0);

        if (transform.position == endpoint)
        {
            endpoint = new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z + direction.z);
        }
        StartCoroutine(onCorountine(endpoint));
    }

    IEnumerator onCorountine(Vector3 endpoint)
    {
        float t = 0;
        while(t!= 1)
        {
            t+=Time.deltaTime;
            rb.MovePosition(Vector3.Lerp(transform.position, endpoint, t));
            yield return new WaitForEndOfFrame();
        }
    }
}