using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class fruit : MonoBehaviour
{
    // Start is called before the first frame update

    public RawImage r;

    public bool scroll;

    void Start()
    {

        scroll = true;

        

        r = this.GetComponent<RawImage>();

        r.uvRect = new Rect(0, Random.Range(0, 453.5f), 1, 1);
    }

    // Update is called once per frame
    void Update()
    {


        if (!scroll && r.uvRect.y%0.2<=0.001)
        {
            
        }
        else
        {
            refresh();
        }
        

    }

    void refresh()
    {
        r.uvRect = new Rect(0, r.uvRect.y + Time.deltaTime, 1, 1);
    }


    public void setscroll()
    {
        Debug.Log(this.gameObject.GetComponent<fruit>().scroll);
        this.scroll = false;
    }
}
