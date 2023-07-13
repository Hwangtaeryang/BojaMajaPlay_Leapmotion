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

        OnRoundStart();  // �ٷν���
    }
    
    // Ȱ��ȭ�ɶ����� ȣ�� > Scene ������Ʈ�� ����. ������ �ѹ�
    void OnEnable()
    {
        CatchPang_Timer.RoundEnd += OnRoundEnd;
        //Debug.Log("Timer.RoundEnd OnEnable : " + CatchPang_Timer.RoundEnd);
    }

    // ��Ȱ��ȭ�ɶ����� ȣ�� > ������ �ѹ�
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

    // ���۹�ư
    public void OnRoundStart()
    {
        // �������� �ڷ�ƾ �� ���߰�
        StopAllCoroutines();
        // ����
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
        // Ŭ���� ��ƼŬ ��
        CatchPang_DataManager.Instance.clearParticles.SetActive(false);
        // ��� ��
        CatchPang_SoundManager.Instance.StopBGM();

        //nextCount.gameObject.SetActive(false);

        SceneManager.LoadScene("Main");
    }
}