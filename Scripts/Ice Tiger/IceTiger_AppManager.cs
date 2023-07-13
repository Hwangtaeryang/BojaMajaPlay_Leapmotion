using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class IceTiger_AppManager : MonoBehaviour
{
    //private IceTiger_Action iceTiger_Action;
    private int startCountSetting = 4;
    private int roundEndCountSetting = 5;
    public GameObject countdownPanel;
    public Image[] startCount;

    public bool gamePlay = false;

    //public static bool isPlaying;
    public static UnityAction RoundStart = null;
    public static IceTiger_AppManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
        countdownPanel.SetActive(true);
        IceTiger_SoundManager.Instance.PlaySE("CountDown");
        // 브금 스타트
        IceTiger_SoundManager.Instance.PlayRandomBGM();

        GameStart();
    }
    
    private void OnEnable()
    {
        IceTiger_Timer.RoundEnd += GameEnd;
    }

    private void OnDisable()
    {
        IceTiger_Timer.RoundEnd -= GameEnd;
    }

    private void GameEnd()
    {
        StopAllCoroutines();
        StartCoroutine(_GameEnd());
    }

    public void GameStart()
    {
        StopAllCoroutines();
        StartCoroutine(_GameStart());
    }

    // 시작 코루틴 : Data, UI, Spawn
    private IEnumerator _GameStart()
    {
        yield return IceTiger_UIManager.Instance._UI_Start();
        yield return new WaitForSeconds(3.8f);
        //yield return GameStartCount(startCountSetting);
        
        yield return IceTiger_DataManager.Instance._Data_Start();
        gamePlay = true;

        //Debug.Log("RoundStart : " + RoundStart);
        if (RoundStart != null)
        {
            RoundStart.Invoke();
        }

    }

    private IEnumerator _GameEnd()
    {
        gamePlay = false;

        yield return IceTiger_DataManager.Instance._Data_End();

        yield return IceTiger_UIManager.Instance._UI_End();

        yield return _NextCount(roundEndCountSetting);
    }

    private IEnumerator _NextCount(int countLeft)
    {
        WaitForSecondsRealtime ws = new WaitForSecondsRealtime(1f);

        //nextCount.gameObject.SetActive(true);

        //if(IceTiger_DataManager.Instance.WonRound())
        //    nextCount.gameObject.transform.localPosition = new Vector3(191f, -449f, 0);
        //else
        //    nextCount.gameObject.transform.localPosition = new Vector3(226f, -445f, 0);

        //while (countLeft > 0)
        //{
        //    nextCount.text = countLeft.ToString();
        //    //nextCount[countLeft].gameObject.SetActive(true);

            yield return ws;

        //    countLeft -= 1;
        //    //nextCount[countLeft].gameObject.SetActive(false);
        //}

        //nextCount.gameObject.SetActive(false);
    }

    private IEnumerator GameStartCount(int countLeft)
    {
        WaitForSecondsRealtime ws = new WaitForSecondsRealtime(0.8f);

        IceTiger_SoundManager.Instance.PlaySE("CountDown");

        while (countLeft > 0)
        {
            countLeft -= 1;
            startCount[countLeft].gameObject.SetActive(true);

            yield return ws;
            startCount[countLeft].gameObject.SetActive(false);
        }
    }

    public void SceneLoad()
    {
        // 클리어 파티클 끝
        IceTiger_DataManager.Instance.fireworks.SetActive(false);
        // 브금 끝
        IceTiger_SoundManager.Instance.StopBGM();

        //nextCount.gameObject.SetActive(false);

        SceneManager.LoadScene("Main");
    }
}
