using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillingMeat_PlayerContoller : MonoBehaviour
{
    //[SerializeField] float m_zoomSpeed = 0f;
    //[SerializeField] float m_zoomMax = 0f;
    //[SerializeField] float m_zoomMin = 0f;
    //[SerializeField] float t_zoomDirection = 5f;

    private Camera cam;
    private Animator camAnimator;
    private Vector3 rayPointPos;
    public LayerMask layerMask;
    
    public static GrillingMeat_PlayerContoller Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }
    void Start()
    {
        // 카메라 참조
        SetInitialReferences();
        // 카메라 줌인 애니메이션
        camAnimator.SetBool("ZoomInOut", true);
    }

    void Update()
    {
        //GameObject hitParticle;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // 맞고있는지 ray ONGUI 에서 확인
        rayPointPos = ray.GetPoint(10f);
        
        if (Physics.Raycast(ray, out RaycastHit hit, 15f, layerMask))
        {
            // Raycast 에 닿는 Layer 가 IceTigers 일 때
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Meats"))
            {
                // Layer 가 IceTiger 이면서 마우스를 클릭시 태그가 IceTiger 이고 현재 백호의 상태가 Up 일때
                if (hit.collider.CompareTag("Meat"))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //Debug.Log("클릭!!!!!!!!!!");
                        // 뒤집어짐
                        hit.transform.gameObject.GetComponent<Meat>().TurnUp();
                    }
                    //IceTiger_Action.Instance.keyValuePairs[hit.transform.gameObject.name] = false;


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

    private void OnGUI()
    {
        Debug.DrawRay(cam.transform.position, rayPointPos - cam.transform.position, Color.magenta, 0.2f);
    }

    void SetInitialReferences()
    {
        cam = GetComponentInChildren<Camera>();

        if (!cam)
        {
            cam = Camera.main;
        }
        
        // 메인카메라 애니메이터
        camAnimator = GetComponentInChildren<Animator>();
    }
}
