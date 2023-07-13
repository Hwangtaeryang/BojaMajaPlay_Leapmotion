using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapMotionGameStartBtnOnClick : MonoBehaviour
{

    public bool mainBtnClick;
    public bool startBtnClick;



    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.name == "MainBtn")
        {
            if(other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
            {
                mainBtnClick = true;
            }
        }

        else if(gameObject.name == "StartBtn")
        {
            if (other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
            {
                startBtnClick = true;
            }
        }
    }
}
