using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillingMeat_PlayerHandContoller : MonoBehaviour
{
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.gameObject.CompareTag("Meat"))
    //    {
    //        collision.collider.gameObject.GetComponent<Meat>().TurnUp();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meat"))
        {
            other.gameObject.GetComponent<Meat>().TurnUp();
        }
    }
}
