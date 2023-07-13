using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTouch : MonoBehaviour
{
    public static FireTouch instance { get; private set; }

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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
        {
            fireTouch = true;
        }
    }
}
