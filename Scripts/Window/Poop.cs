using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{


    public static string windowName;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Window"))
        {
            windowName = collision.gameObject.name;
            WindowSoundManager.instance.BridPoopSound();
            //Debug.Log(windowName);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Window"))
        {
            Destroy(this.gameObject);
        }
    }
}
