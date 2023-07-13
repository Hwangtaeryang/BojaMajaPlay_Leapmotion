using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceTiger_DataManager : MonoBehaviour
{
    [Header("Data Information")]
    public float hammerDamage;  // 공 데미지
    //public int scoreCostOnGettingHit;   // 맞췄을때 얻는 점수
    public IceTiger_Timer timer;    // 타이머
    public int score;   // 현재 스코어
    public int highscore;

    [Header("Lv Score Text")]
    public Text SuccessScore;

    [Header("Particles")]
    public GameObject fireworks;
    // 내일 파티클 여러개 설정
    public GameObject[] hitParticle;

    public static IceTiger_DataManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }
    
    public void ResetScore()
    {
        score = 0;
    }

    public void AddScore(float points)
    {
        score += (int)points;

        IceTiger_UIManager.Instance.SetScore(score);
    }

    public bool WonRound()
    {
        return score > 0;//>= highscore;
    }

    // 데이터 시작
    public IEnumerator _Data_Start()
    {
        // 게임클리어하면 생기는 파티클
        fireworks.SetActive(false);
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

            SuccessScore.text = score.ToString();
            // 클리어
            fireworks.SetActive(true);
        }
        else
        {
            // 게임오버
            //FailedTime.text = "00'00\"";
        }

        yield return null;
    }
}
