using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FruitSlice
{ 
    public class PlayerHandController_FruitSlice : MonoBehaviour
    {
        private Camera cam;

        //public static PlayerHandController_FruitSlice Instance { get; private set; }

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
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Fruit"))
            {
                collision.collider.gameObject.transform.GetComponent<Fruit>().Slice();
            }
        }

        void SetInitialReferences()
        {
            cam = GetComponentInChildren<Camera>();
            if (!cam)
                cam = Camera.main;
        }
    }
}