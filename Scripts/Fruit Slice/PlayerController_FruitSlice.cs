using UnityEngine;
using System.Collections;

namespace FruitSlice
{
    public class PlayerController_FruitSlice : MonoBehaviour
    {
        private Camera cam;
        private Vector3 rayPointPos;

        public float trailDist;
        public LayerMask layerMask;
        // public CanvasGroup playerHit;


        public static PlayerController_FruitSlice Instance { get; private set; }
        void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else Instance = this;
        }
        void Start()
        {
            SetInitialReferences();
        }

        void Update()
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 15f, layerMask))
            {
                //Debug.Log("Raycast hit: " + hit.transform.name);
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Fruit"))
                {
                    //Debug.Log("Sliced fruit!");
                    hit.transform.GetComponent<Fruit>().Slice();
                }
            }

            rayPointPos = ray.GetPoint(trailDist);
            transform.GetChild(0).position = rayPointPos;
        }
        void OnGUI()
        {
            Debug.DrawRay(cam.transform.position, rayPointPos - cam.transform.position, Color.magenta, 0.2f);
        }

        // public void GetHit()
        // {
        //     StopCoroutine("_GetHit");
        //     StartCoroutine("_GetHit");
        // }
        // IEnumerator _GetHit()
        // {
        //     DataManager.Instance.SubtractScore();

        //     float value = 1;
        //     playerHit.alpha = value;

        //     while (value > 0.1f)
        //     {
        //         value -= Time.deltaTime * 7f;
        //         playerHit.alpha = value;
        //         yield return null;
        //     }

        //     playerHit.alpha = 0f;
        // }

        void SetInitialReferences()
        {
            cam = GetComponentInChildren<Camera>();
            if (!cam)
                cam = Camera.main;
        }
    }
}