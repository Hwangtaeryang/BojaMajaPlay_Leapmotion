using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDataManager : MonoBehaviour
{
    public static GoDataManager instance { get; private set; }

    //public GameObject successParticle;  //성공 시 파티클
    public GoTimer playTime;  //플레이 타이머

    public int score;   //점수
    public int totalScroe;

    private BlackGoStoneSpawn blackGoStoneSpawn;
    private WhiteGoStoneSpawn whiteGoStoneSpawn;


    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //successParticle.SetActive(false);   //성공 파티클 비활성화
        blackGoStoneSpawn = FindObjectOfType<BlackGoStoneSpawn>();
        whiteGoStoneSpawn = FindObjectOfType<WhiteGoStoneSpawn>();
    }

    public bool GameEndScoreState()
    {
        //Debug.Log("GameEndScoreState");
        return score > 0;
    }

    // 게임 시작
    public IEnumerator GameStart()
    {
        playTime.StartTimer(); //TImer스크립트에 있는 플레이시간(30초)시작
        blackGoStoneSpawn.StartSpawner();
        whiteGoStoneSpawn.StartSpawner();

        yield return null;
    }

    public IEnumerator GameEnd()
    {
        // 스폰 중지
        blackGoStoneSpawn.OnRoundEnd();
        whiteGoStoneSpawn.OnRoundEnd();
        //Debug.Log(GameEndScoreState());
        if (GameEndScoreState())
            //successParticle.SetActive(true);   //성공 파티클 활성화

        yield return null;
    }

    public void AddScore(float points)
    {
        score += (int)points;

        GoUIManager.instance.SetScore(score);
    }
}
