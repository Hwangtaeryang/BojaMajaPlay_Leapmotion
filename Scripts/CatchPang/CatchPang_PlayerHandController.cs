using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPang_PlayerHandController : MonoBehaviour
{
    public GameObject setPos;
    public GameObject[] balls;
    //public GameObject PalmPose;
    public LayerMask projectileLayer;
    public float strength;

    private bool isThumbExtended;
    private bool isPinch;

    private SphereCollider sphereCollider;
    private float distance = 10f;
    private Ball currentBall;
    private Camera cam;


    //public static CatchPang_PlayerHandController Instance { get; private set; }

    //void Awake()
    //{
    //    if (Instance != null)
    //        Destroy(this);
    //    else Instance = this;
    //}

    private void Start()
    {
        SetInitialReferences();
        sphereCollider = GetComponent<SphereCollider>();
    }

    void OnGUI()
    {
        Debug.DrawRay(cam.transform.position, transform.position - cam.transform.position, Color.magenta, 0.2f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            foreach (var ball in balls)
            {
                // 저장해놓은 볼중에 레이캐스트로 클릭했을 때 재질과 같으면 그때 currentBall 변수에 Prefab을 생성
                if (ball.GetComponent<MeshRenderer>().sharedMaterial == other.gameObject.transform.GetComponent<MeshRenderer>().sharedMaterial)
                {
                    currentBall = Instantiate(ball).GetComponent<Ball>();
                }
            }

            currentBall.transform.localScale = other.gameObject.transform.lossyScale;

            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.enabled = false;
            currentBall.transform.position = setPos.transform.position;
            currentBall.transform.SetParent(setPos.transform);
            currentBall.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void middleFingerDirectionActivate()
    {
        //Debug.Log("middleFingerDirectionActivate");
        if (setPos.transform.childCount == 1 && !isPinch)
        {
            StartCoroutine("EnableSphereCollider");
         
            setPos.transform.DetachChildren();
            currentBall.Throw(transform.position - cam.transform.position, strength);
            currentBall.Throw(transform.position - cam.transform.position, strength);
            currentBall = null;
            isPinch = true;
            
        }
    }

    IEnumerator EnableSphereCollider()
    {
        yield return new WaitForSeconds(0.3f);
        sphereCollider.enabled = true;
    }

    public void ThumbDirectionActivate()
    {
        //Debug.Log("펴지나?");
        isThumbExtended = true;
    }

    public void ThumbDirectionDeactivate()
    {
        //Debug.Log("접히나?");
        isThumbExtended = false;
    }

    public void PinchActivate()
    {
        //Debug.Log("꼬집?");
        isPinch = true;
    }

    public void PinchDeactivate()
    {
        //Debug.Log("폈다?");
        isPinch = false;
    }

    void SetInitialReferences()
    {
        cam = GetComponentInChildren<Camera>();
        if (!cam)
            cam = Camera.main;
    }
}
