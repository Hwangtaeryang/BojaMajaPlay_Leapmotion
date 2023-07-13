using System.Collections;
using UnityEngine;

public class FruitDataManager : MonoBehaviour
{
    public GameObject fireworks;
    public GameObject hitParticles;
    public GameObject missParticles;
    public float projectileDamage;
    public int scoreCostOnGettingHit;
    public int highscore;
    public int score;
    public FruitTimer levelTimer;

    public static FruitDataManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        FruitUIManager.Instance.SetTotalScore(highscore);    //목표 점수 초기화
    }
    

    public void AddScore(float points)
    {
        score += (int)points;
        FruitUIManager.Instance.SetScore(score);
    }

    public void SubtractScore()
    {
        score -= scoreCostOnGettingHit;

        if (score < 0) score = 0;

        FruitUIManager.Instance.SetScore(score);
    }

    private void ResetScore()
    {
        score = 0;
    }

    public bool WonRound()
    {
        return score > 0;//>= highscore;
    }

    public IEnumerator FireShowOff()
    {
        //불꽃 효과 끄기
        fireworks.SetActive(false);
        yield return null;
    }

    public IEnumerator OnRoundStart()
    {
        ResetScore();
        levelTimer.StartTimer();

        yield return null;
    }

    public IEnumerator OnRoundEnd()
    {
        if (WonRound()) fireworks.SetActive(true);

        yield return null;
    }
}
