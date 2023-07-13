using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GrillingMeat_AppManager : MonoBehaviour
{
    //private IceTiger_Action iceTiger_Action;
    private int startCountSetting = 4;
    private int roundEndCountSetting = 5;
    public GameObject countdownPanel;
    //public Image[] startCount;

    public bool gamePlay = false;

    //public static bool isPlaying;
    public static UnityAction RoundStart = null;
    public static GrillingMeat_AppManager Instance { get; private set; }
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
        GrillingMeat_SoundManager.Instance.PlaySE("CountDown");
        // 브금 스타트
        GrillingMeat_SoundManager.Instance.PlayGrillingBGM();

        GameStart();
    }

    private void OnEnable()
    {
        GrillingMeat_Timer.RoundEnd += GameEnd;
    }

    private void OnDisable()
    {
        GrillingMeat_Timer.RoundEnd -= GameEnd;
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
        yield return GrillingMeat_UIManager.Instance._UI_Start();

        yield return new WaitForSeconds(3.8f);
        GrillingMeat_SoundManager.Instance.PlayGrillingBGM();
        //yield return GameStartCount(startCountSetting);

        yield return GrillingMeat_DataManager.Instance._Data_Start();
        gamePlay = true;


        // Meat 구워지기 시작 : 고기 최대 갯수가 제한되어있어야함. 
        //Debug.Log("RoundStart : " + RoundStart);
        if (RoundStart != null)
        {
            /// Debug.Log("RoundStart Invoke");
            RoundStart.Invoke();
        }

        //AdManager.Instance.ToggleAd(true);
    }

    private IEnumerator _GameEnd()
    {
        //AdManager.Instance.ToggleAd(false);
        gamePlay = false;

        yield return GrillingMeat_DataManager.Instance._Data_End();

        yield return GrillingMeat_UIManager.Instance._UI_End();

        yield return _NextCount(roundEndCountSetting);
    }

    private IEnumerator _NextCount(int countLeft)
    {
        WaitForSecondsRealtime ws = new WaitForSecondsRealtime(1f);

        //nextCount.gameObject.SetActive(true);

        //if(GrillingMeat_DataManager.Instance.WonRound())
        //    nextCount.gameObject.transform.localPosition = new Vector3(189f, -448f, 0);
        //else
        //    nextCount.gameObject.transform.localPosition = new Vector3(226f, -443f, 0);

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

        // 3,2,1 소리
        GrillingMeat_SoundManager.Instance.PlaySE("CountDown");

        while (countLeft > 0)
        {
            countLeft -= 1;
            //startCount[countLeft].gameObject.SetActive(true);

            yield return ws;
            //startCount[countLeft].gameObject.SetActive(false);
        }

        // 카운터끝나고 고기굽기 소리 시작
        GrillingMeat_SoundManager.Instance.PlayGrillingBGM();
    }

    public void SceneLoad()
    {
        // 클리어 파티클 끝
        //GrillingMeat_DataManager.Instance.fireworks.SetActive(false);
        // 브금 끝
        GrillingMeat_SoundManager.Instance.StopGrillingBGM();
        GrillingMeat_SoundManager.Instance.StopSfx();

        //nextCount.gameObject.SetActive(false);

        SceneManager.LoadScene("Main");
    }
}
