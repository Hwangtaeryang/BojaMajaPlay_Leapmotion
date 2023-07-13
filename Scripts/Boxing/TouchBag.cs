using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchBag : MonoBehaviour
{

    public static TouchBag instance { get; private set; }


    public bool hitLeftState;
    public bool hitRightState;



    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("L_Hand"))
        {
            hitLeftState = true;
        }

        if(collision.gameObject.CompareTag("R_Hand"))
        {
            hitRightState = true;
        }
    }
}
