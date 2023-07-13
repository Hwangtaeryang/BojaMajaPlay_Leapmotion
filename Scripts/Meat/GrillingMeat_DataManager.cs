using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrillingMeat_DataManager : MonoBehaviour
{
    [Header("Data Information")]
    public GrillingMeat_Timer timer;    // 타이머
    public int score;   // 현재 스코어
    public int highscore;

    [Header("Lv Score Text")]
    public Text SuccessScore;

    private Camera cam;
    private Animator camAnimator;

    public static GrillingMeat_DataManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        // 카메라 참조
        SetInitialReferences();
        // 카메라 줌인 애니메이션
        camAnimator.SetBool("ZoomInOut", true);
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void AddScore(float points)
    {
        score += (int)points;

        GrillingMeat_UIManager.Instance.SetScore(score);
    }

    public bool WonRound()
    {
        return score > 0;//>= highscore;
    }

    // 데이터 시작
    public IEnumerator _Data_Start()
    {
        // 게임클리어하면 생기는 파티클
        //fireworks.SetActive(false);
        // 타이머 시작
        timer.StartTimer();
        // 스코어 초기화
        ResetScore();

        yield return null;
    }

    public IEnumerator _Data_End()
    {
        // 스코어 넘겼을 때
        if (WonRound())
        {
            //string secToString;
            // Text에 timer , Score 넣기
            //SuccessTime.text = GrillingMeat_Timer.Instance.timeLeft.ToString("N2");

            SuccessScore.text = score.ToString();
            // 클리어
            //fireworks.SetActive(true);
        }
        else
        {
            // 게임오버
            //FailedTime.text = "00'00\"";
            //FailedScore.text = score.ToString();
        }

        yield return null;
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
