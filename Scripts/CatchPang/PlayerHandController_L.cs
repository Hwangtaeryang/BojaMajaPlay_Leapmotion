using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandController_L : MonoBehaviour
{
    //public GameObject setPos;
    //public GameObject PalmPose;
    //public LayerMask projectileLayer;
    //public float strength;

    //private SphereCollider sphereCollider;
    //private float distance = 10f;
    private Ball currentBall;
    private Camera cam;
    private bool is_ColCheck = false;

    [Header("Sfx")]
    public string[] flyingSfx;

    [Header("Balls")]
    public GameObject[] balls;
    // Start is called before the first frame update
    private void Start()
    {
        //is_ColCheck = true;
        //SetInitialReferences();
        //sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (is_ColCheck)
        //{
        //    Debug.Log("2여기동작은 하니?");
        //    if (collision.gameObject.CompareTag("Ball"))
        //    {
        //        foreach (var ball in balls)
        //        {
        //            // 저장해놓은 볼중에 레이캐스트로 클릭했을 때 재질과 같으면 그때 currentBall 변수에 Prefab을 생성
        //            if (ball.GetComponent<MeshRenderer>().sharedMaterial == collision.gameObject.transform.GetComponent<MeshRenderer>().sharedMaterial)
        //            {
        //                currentBall = Instantiate(ball).GetComponent<Ball>();
        //                Debug.Log("currentBall : " + currentBall);
        //            }
        //        }

        //        currentBall.transform.localScale = collision.gameObject.transform.lossyScale;
        //        currentBall.transform.position = collision.gameObject.transform.position;

        //        Debug.Log("currentBall transform.position : " + currentBall.transform.position);

        //        //sphereCollider = GetComponent<SphereCollider>();
        //        //sphereCollider.enabled = false;
        //        //currentBall.transform.position = setPos.transform.position;
        //        //currentBall.transform.SetParent(setPos.transform);
        //        //currentBall.GetComponent<Rigidbody>().isKinematic = true;
        //    }
        //}

    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ball"))
    //    {
    //        is_ColCheck = false;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ball"))
    //    {
    //        StopAllCoroutines();
    //        StartCoroutine(_IsColCheckFunc());
    //    }
    //}

    //IEnumerator _IsColCheckFunc()
    //{
    //    WaitForSeconds ws = new WaitForSeconds(0.3f);

    //    yield return ws;

    //    is_ColCheck = true;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("1여기동작은 하니?");
    //    if (other.gameObject.CompareTag("Ball"))
    //    {
    //        foreach (var ball in balls)
    //        {
    //            // 저장해놓은 볼중에 레이캐스트로 클릭했을 때 재질과 같으면 그때 currentBall 변수에 Prefab을 생성
    //            if (ball.GetComponent<MeshRenderer>().sharedMaterial == other.gameObject.transform.GetComponent<MeshRenderer>().sharedMaterial)
    //            {
    //                currentBall = Instantiate(ball).GetComponent<Ball>();
    //            }
    //        }

    //        currentBall.transform.localScale = other.gameObject.transform.lossyScale;

    //        //sphereCollider = GetComponent<SphereCollider>();
    //        //sphereCollider.enabled = false;
    //        //currentBall.transform.position = setPos.transform.position;
    //        //currentBall.transform.SetParent(setPos.transform);
    //        //currentBall.GetComponent<Rigidbody>().isKinematic = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Ball"))
    //    {
    //        CatchPang_SoundManager.Instance.PlaySE(flyingSfx[Random.Range(0, 2)]);
    //    }
    //}

    //void SetInitialReferences()
    //{
    //    cam = GetComponentInChildren<Camera>();
    //    if (!cam)
    //        cam = Camera.main;
    //}
}
