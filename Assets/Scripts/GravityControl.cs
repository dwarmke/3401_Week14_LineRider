using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.gravity = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.Space))
         {
            Physics2D.gravity = new Vector3(0,-10,0);
         }
    }
}
