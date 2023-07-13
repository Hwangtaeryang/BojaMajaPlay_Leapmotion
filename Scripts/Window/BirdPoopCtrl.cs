using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPoopCtrl : MonoBehaviour
{
    public static BirdPoopCtrl instance { get; private set; }


    public Transform[] poopPos;

    public GameObject poop;
    GameObject poop_copy;
    string windowName;


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

    
    void Update()
    {
        
    }

    public void PoopMakeShow()
    {
        StartCoroutine(_PoopMakeShow());
    }

    IEnumerator _PoopMakeShow()
    {
        if(WindowGameManager.instance.gamePlay)
        {
            yield return new WaitForSeconds(3f);

            int posNum = Random.Range(0, poopPos.Length);

            poop_copy = Instantiate(poop);//, poopPos[posNum]);
            poop_copy.transform.position = poopPos[posNum].position;

            StartCoroutine(_PoopMakeShow());
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Window"))
    //    {
    //        windowName = other.gameObject.name;
    //        Debug.Log(windowName);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Window"))
        {
            //Debug.Log("-----");
            windowName = collision.gameObject.name;
            //Debug.Log(windowName);
        }
    }
}
