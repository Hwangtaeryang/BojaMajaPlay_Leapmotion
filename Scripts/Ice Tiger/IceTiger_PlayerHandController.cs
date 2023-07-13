using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTiger_PlayerHandController : MonoBehaviour
{
    private Camera cam;
    
    //public static IceTiger_PlayerHandController Instance { get; private set; }

    //private void Awake()
    //{
    //    if (Instance != null)
    //        Destroy(this);
    //    else Instance = this;
    //}

    private void Start()
    {
        SetInitialReferences();     // Ray 상태 보려고 캠넣음
    }

    void OnGUI()
    {
        Debug.DrawRay(cam.transform.position, transform.position - cam.transform.position, Color.magenta, 0.2f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("IceTigers"))
        {
            collision.collider.gameObject.transform.GetComponent<IceTiger>().OnDown();
        }
    }

    void SetInitialReferences()
    {
        cam = GetComponentInChildren<Camera>();
        if (!cam)
            cam = Camera.main;
    }
}
