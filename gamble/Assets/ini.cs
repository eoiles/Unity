using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ini : MonoBehaviour
{
    public GameObject col;
    public int colnumber;


    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < colnumber; i++)
        {
            var a = Instantiate(col, this.transform, false);
            a.transform.localPosition = new Vector3(112.5f * i-112.5f*2,0, 0);
        }
        
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
