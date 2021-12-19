using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = new Vector3(0, 0, -20);

        this.transform.position += Mathf.Sin(Time.time*2) / 100 * direction;

    }
}