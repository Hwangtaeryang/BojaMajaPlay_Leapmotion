using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("asdfasdf222 :::" + other.name);
        //TreeSlashSoundManager.Instance.PlaySE("swish" + Random.Range(1, 3));
        if (other.CompareTag("L_Hand"))
        {
            //Debug.Log("asdfasdf");
            TreeSlashSoundManager.Instance.PlaySE("swish" + Random.Range(1, 3));
        }

        if (other.CompareTag("R_Hand"))
        {
            //Debug.Log("asdfasdf11");
            TreeSlashSoundManager.Instance.PlaySE("swish" + Random.Range(1, 3));
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("asdfasdf22");
    //    if (collision.collider.gameObject.CompareTag("L_Hand"))
    //    {
    //        Debug.Log("asdfasdf");
    //        TreeSlashSoundManager.Instance.PlaySE("swish" + Random.Range(1, 3));
    //    }

    //    if (collision.collider.gameObject.CompareTag("R_Hand"))
    //    {
    //        Debug.Log("asdfasdf11");
    //        TreeSlashSoundManager.Instance.PlaySE("swish" + Random.Range(1, 3));
    //    }
    //}
}
