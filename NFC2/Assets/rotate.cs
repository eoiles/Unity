using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{


    public float speed = 15;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.RotateAround(Vector3.zero, Vector3.up, speed * Time.deltaTime);
	}
}
