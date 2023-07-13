using UnityEngine;
using System.Collections;

public class CatchPang_PlayerController : MonoBehaviour
{
    private Camera cam;
    private Ball currentBall;
    private float dist;
    private Vector3 rayPointPos;

    [Header("Player Info")]
    public GameObject hand;
    public LayerMask projectileLayer;
    public float strength;
    public CanvasGroup playerHit;
    public string[] flyingSfx;

    [Header("Balls")]
    public GameObject[] balls;

    public static CatchPang_PlayerController Instance { get; private set; }
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
        //Debug.DrawRay(cam.transform.position, Input.mousePosition, Color.magenta, 0.2f);
        // 현재 마우스위치에 있는 projectileLayer( > Balls 로 설정되어있음 )를 체크
        if (Physics.Raycast(ray, out hit, 5f, projectileLayer))
        {
            //Debug.Log("Raycast hit: " + hit.transform.name);
            if (hit.collider.CompareTag("Ball"))    // 태그가 Ball 이면
            {
                // 마우스 버튼을 누른다
                if (Input.GetMouseButtonDown(0))
                {
                    foreach (var ball in balls)
                    {   // 저장해놓은 볼중에 레이캐스트로 클릭했을 때 재질과 같으면 그때 currentBall 변수에 Prefab을 생성
                        if (ball.GetComponent<MeshRenderer>().sharedMaterial == hit.transform.GetComponent<MeshRenderer>().sharedMaterial)
                            currentBall = Instantiate(ball).GetComponent<Ball>();
                    }
                    
                    // 현재 저장해놓은 볼의 크기를 절대적인 크기로 같게하고, 위치를 카메라쪽으로 0.5만큼 이동시킨다.
                    currentBall.transform.localScale = hit.transform.lossyScale;
                    dist = Vector3.Distance(cam.transform.position, hit.transform.position);
                    dist -= 0.5f;
                }

            }
        }
        // currentBall 을 생성했다면( 클릭했다면 ! )
        if (currentBall != null)
        {
            // 레이가 닿고있는 위치로 잡고있는 Ball 을 움직인다.
            rayPointPos = ray.GetPoint(dist);
            currentBall.transform.position = rayPointPos;

            // 마우스 버튼을 놓는다
            if (Input.GetMouseButtonUp(0))
            {
                // 볼 던질 때 사운드
                CatchPang_SoundManager.Instance.PlaySE(flyingSfx[Random.Range(0,2)]);

                //Debug.Log("rayPointPos - cam.transform.position : " + (rayPointPos - cam.transform.position));
                // 볼을 던진다.
                currentBall.Throw(rayPointPos - cam.transform.position, strength);
                currentBall = null;
            }
        }
    }
    void OnGUI()
    {
        Debug.DrawRay(cam.transform.position, rayPointPos - cam.transform.position, Color.magenta, 0.2f);
    }

    public void GetHit()
    {
        StopCoroutine("_GetHit");
        StartCoroutine("_GetHit");
    }
    IEnumerator _GetHit()
    {
        CatchPang_DataManager.Instance.SubtractScore();
        
        float value = 1;
        playerHit.alpha = value;

        while (value > 0.1f)
        {
            value -= Time.deltaTime * 7f;
            playerHit.alpha = value;
            yield return null;
        }

        playerHit.alpha = 0f;
    }

    void SetInitialReferences()
    {
        cam = GetComponentInChildren<Camera>();
        if (!cam)
            cam = Camera.main;
    }
}