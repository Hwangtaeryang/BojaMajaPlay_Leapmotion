using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSlashDataManager : MonoBehaviour
{
    public static TreeSlashDataManager instance { get; private set; }

    //public GameObject successParticle;  //성공 시 파티클
    public TreeSlashTimer playTime;  //플레이 타이머

    public int score;   //점수
    public int totalScore;

    [Header("Particle")]
    public GameObject hitParticle;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void AddScore(float points)
    {
        score += (int)points;

        TreeSlashUIManager.instance.SetScore(score);
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    //successParticle.SetActive(false);   //성공 파티클 비활성화
    //}

    public bool GameEndScoreState()
    {
        //Debug.Log("GameEndScoreState");
        return score > 0;//>= totalScore;
    }

    // 게임 시작
    public IEnumerator GameStart()
    {
        ResetScore();
        TreeSlashUIManager.instance.SetScore(score);
        playTime.StartTimer(); //TImer스크립트에 있는 플레이시간(30초)시작

        yield return null;
    }

    public IEnumerator GameEnd()
    {
        //Debug.Log(GameEndScoreState());
        if (GameEndScoreState())
            //successParticle.SetActive(true);   //성공 파티클 활성화

        yield return null;
    }
}
