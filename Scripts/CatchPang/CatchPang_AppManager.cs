using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CatchPang_AppManager : MonoBehaviour
{
    private EnemySpawn enemySpawner;
    private BallSpawn ballSpawner;
    public GameObject countdownPanel;
    private int startCountSetting = 4;
    private int roundEndCountSetting = 5;
    //public Image[] startCount;

    public bool gamePlay = false;

    public static CatchPang_AppManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
        countdownPanel.SetActive(true);
        CatchPang_SoundManager.Instance.PlaySE("CountDown");
        CatchPang_SoundManager.Instance.PlayRandomBGM();

        enemySpawner = FindObjectOfType<EnemySpawn>();
        ballSpawner = FindObjectOfType<BallSpawn>();

        OnRoundStart();  // 바로시작
    }
    
    // 활성화될때마다 호출 > Scene 오브젝트에 있음. 켜질때 한번
    void OnEnable()
    {
        CatchPang_Timer.RoundEnd += OnRoundEnd;
        //Debug.Log("Timer.RoundEnd OnEnable : " + CatchPang_Timer.RoundEnd);
    }

    // 비활성화될때마다 호출 > 꺼질때 한번
    void OnDisable()
    {
        CatchPang_Timer.RoundEnd -= OnRoundEnd;
        //Debug.Log("Timer.RoundEnd OnDisable : " + CatchPang_Timer.RoundEnd);
    }

    void OnRoundEnd()
    {
       StopAllCoroutines();
       StartCoroutine(_OnRoundEnd());
    }

    // 시작버튼
    public void OnRoundStart()
    {
        // 실행중인 코루틴 다 멈추고
        StopAllCoroutines();
        // 실행
        StartCoroutine(_OnRoundStart());
    }
    private IEnumerator _OnRoundStart()
    {
        yield return CatchPang_UIManager.Instance.OnRoundStart();

        yield return new WaitForSeconds(3.8f);
        //yield return GameStartCount(startCountSetting);

        yield return CatchPang_DataManager.Instance.OnRoundStart();
        gamePlay = true;

        //yield return GoAd.Instance.OnRoundStart();

        enemySpawner.StartSpawner();
        ballSpawner.StartSpawner();

    }

    private IEnumerator _OnRoundEnd()
    {
        gamePlay = false;

        yield return CatchPang_DataManager.Instance.OnRoundEnd();

        yield return CatchPang_UIManager.Instance.OnRoundEnd();

        enemySpawner.OnRoundEnd();

    }


    private IEnumerator GameStartCount(int countLeft)
    {
        WaitForSecondsRealtime ws = new WaitForSecondsRealtime(0.8f);

        CatchPang_SoundManager.Instance.PlaySE("CountDown");


        while (countLeft > 0)
        {
            countLeft -= 1;
            //startCount[countLeft].gameObject.SetActive(true);

            yield return ws;
            //startCount[countLeft].gameObject.SetActive(false);
        }
    }

    public void SceneLoad()
    {
        // 클리어 파티클 끝
        CatchPang_DataManager.Instance.clearParticles.SetActive(false);
        // 브금 끝
        CatchPang_SoundManager.Instance.StopBGM();

        //nextCount.gameObject.SetActive(false);

        SceneManager.LoadScene("Main");
    }
}