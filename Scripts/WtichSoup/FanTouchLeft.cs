using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTouchLeft : MonoBehaviour
{
    public static FanTouchLeft instance { get; private set; }

    public bool fireTouch;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fire"))
        {
            fireTouch = true;
        }
    }
}
