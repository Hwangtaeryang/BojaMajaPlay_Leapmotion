using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTiger_PlayerContoller : MonoBehaviour
{
    private Camera cam;
    private Vector3 rayPointPos;
    public LayerMask layerMask;
    //public float hitDist;

    public static IceTiger_PlayerContoller Instance { get; private set; }
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
        //GameObject hitParticle;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 15f, layerMask))
        {
            // Raycast 에 닿는 Layer 가 IceTigers 일 때
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("IceTigers"))
            {
                // Layer 가 IceTiger 이면서 마우스를 클릭시 태그가 IceTiger 이고 현재 백호의 상태가 Up 일때
                if (hit.collider.CompareTag("IceTiger"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.transform.gameObject.GetComponent<IceTiger>().OnDown();
                    }
                    //IceTiger_Action.Instance.keyValuePairs[hit.transform.gameObject.name] = false;
                    // 맞고있는지 ray ONGUI 에서 확인
                    //rayPointPos = ray.GetPoint(10f);

                    // hitParticle 생성 > 1.5초 뒤에 파괴
                    //hitParticle = Instantiate(IceTiger_DataManager.Instance.hitParticle[Random.Range(0, 3)], hit.transform.gameObject.transform);

                    // hitParticle 위치 지정
                    //hitParticle.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.4f, hit.transform.position.z);

                    // 백호한테 뎀지줌!
                    

                    //IceTiger_Action.Instance.keyValuePairs[hit.transform.gameObject.name] = false;
                }
            }
        }

    }
    //void OnGUI()
    //{
    //    //Debug.DrawRay(cam.transform.position, rayPointPos - cam.transform.position, Color.magenta, 0.2f);
    //}

    void SetInitialReferences()
    {
        cam = GetComponentInChildren<Camera>();
        if (!cam)
            cam = Camera.main;

        //Debug.Log("cam : " + cam);
    }
}
