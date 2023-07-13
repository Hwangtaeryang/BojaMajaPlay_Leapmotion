using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandController_R : MonoBehaviour
{
    //public GameObject setPos;
    //public GameObject PalmPose;
    //public LayerMask projectileLayer;
    //public float strength;

    //private SphereCollider sphereCollider;
    //private float distance = 10f;
    private Ball currentBall;
    private Camera cam;

    [Header("Sfx")]
    public string[] flyingSfx;

    [Header("Balls")]
    public GameObject[] balls;
    // Start is called before the first frame update
    private void Start()
    {
        //SetInitialReferences();
        //sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("여기동작은 하니?");
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

            //sphereCollider = GetComponent<SphereCollider>();
            //sphereCollider.enabled = false;
            //currentBall.transform.position = setPos.transform.position;
            //currentBall.transform.SetParent(setPos.transform);
            //currentBall.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            CatchPang_SoundManager.Instance.PlaySE(flyingSfx[Random.Range(0, 2)]);
        }
    }

    //void SetInitialReferences()
    //{
    //    cam = GetComponentInChildren<Camera>();
    //    if (!cam)
    //        cam = Camera.main;
    //}
}
