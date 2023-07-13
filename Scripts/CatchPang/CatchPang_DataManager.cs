using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CatchPang_DataManager : MonoBehaviour
{
    [Header("Data Information")]
    public float projectileDamage;  // 공 데미지
    public int scoreCostOnGettingHit;   // 맞췄을때 얻는 점수
    public int score;   // 현재 스코어
    public int highscore;
    public CatchPang_Timer levelTimer;    // 타이머

    [Header("Lv Score Text")]
    //public Text SuccessTime;
    public Text SuccessScore;
    //public Text FailedTime;
    //public Text FailedScore;

    [Header("Particles")]
    public GameObject hitParticles;
    public GameObject missParticles;
    public GameObject clearParticles;

    public static CatchPang_DataManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }

    public void AddScore(float points)
    {
        score += (int)points;

        CatchPang_UIManager.Instance.SetScore(score);
    }
    public void SubtractScore()
    {
        score -= scoreCostOnGettingHit;

        if (score < 0) score = 0;

        CatchPang_UIManager.Instance.SetScore(score);
    }

    public bool WonRound()
    {
        return score > 0; //>= highscore;
    }

    private void ResetScore()
    {
        score = 0;
    }
    
    // 시작버튼 눌렀을 때
    public IEnumerator OnRoundStart()
    {
        // 게임클리어하면 생기는 파티클
        clearParticles.SetActive(false);
        // 타이머 시작
        levelTimer.StartTimer();

        ResetScore();

        yield return null;
    }

    // 라운드 끝나면
    public IEnumerator OnRoundEnd()
    {
        // 스코어 넘겼을 때
        if (WonRound())
        {
            //string secToString;
            // Text에 timer , Score 넣기
            //SuccessTime.text = CatchPang_Timer.Instance.timeLeft.ToString("N2");

            SuccessScore.text = score.ToString();
            // 클리어
            clearParticles.SetActive(true);
        }
        else
        {
            // 게임오버
            //FailedTime.text = "00'00\"";
            //FailedScore.text = score.ToString();
        }

        yield return null;
    }
}