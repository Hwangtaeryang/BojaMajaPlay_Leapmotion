using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CatchPang_DataManager : MonoBehaviour
{
    [Header("Data Information")]
    public float projectileDamage;  // �� ������
    public int scoreCostOnGettingHit;   // �������� ��� ����
    public int score;   // ���� ���ھ�
    public int highscore;
    public CatchPang_Timer levelTimer;    // Ÿ�̸�

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
    
    // ���۹�ư ������ ��
    public IEnumerator OnRoundStart()
    {
        // ����Ŭ�����ϸ� ����� ��ƼŬ
        clearParticles.SetActive(false);
        // Ÿ�̸� ����
        levelTimer.StartTimer();

        ResetScore();

        yield return null;
    }

    // ���� ������
    public IEnumerator OnRoundEnd()
    {
        // ���ھ� �Ѱ��� ��
        if (WonRound())
        {
            //string secToString;
            // Text�� timer , Score �ֱ�
            //SuccessTime.text = CatchPang_Timer.Instance.timeLeft.ToString("N2");

            SuccessScore.text = score.ToString();
            // Ŭ����
            clearParticles.SetActive(true);
        }
        else
        {
            // ���ӿ���
            //FailedTime.text = "00'00\"";
            //FailedScore.text = score.ToString();
        }

        yield return null;
    }
}