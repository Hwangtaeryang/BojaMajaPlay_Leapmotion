using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDataManager : MonoBehaviour
{
    public static WitchDataManager instance { get; private set; }

    public GameObject successParticle;  //성공 시 파티클
    public WitchTimer playTime;  //플레이 타이머

    public int score;   //점수
    public int totalScroe;


    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        //목표점수 설정
        //WitchUIManager.instance.SetTotalSocre(totalScroe);
        successParticle.SetActive(false);   //성공 파티클 비활성화
    }
    
    void Update()
    {
        //플레이 되고 있을 때만 게임 진행
        if(WitchGameManager.instance.gamePlay)
        {
            ClickCount();
        }
    }

    //클릭 시 점수 올리는 함수
    public void ClickCount()
    {
        if (Input.GetMouseButtonDown(0) || FireTouch.instance.fireTouch)//FanTouchLeft.instance.fireTouch || FanTouchRight.instance.fireTouch)
        {
            score += 100;
            WitchSoundManager.instance.StartSocreUpSound(); //점수올라가는 사운드 
            WitchUIManager.instance.SetScore(score);    //점수 뿌려준다
            WitchUIManager.instance.ScoreUpParticle();  //클릭시 파티클 뿌림
            WitchUIManager.instance.ElapsedTimeScore(score);    //각 점수마다 해당 파티클

            FireTouch.instance.fireTouch = false;

            //if (FanTouchLeft.instance.fireTouch)
            //    FanTouchLeft.instance.fireTouch = false;

            //if (FanTouchRight.instance.fireTouch)
            //    FanTouchRight.instance.fireTouch = false;
        }
    }



    void ReStartScore()
    {
        score = 0;
        successParticle.SetActive(false);   //성공 파티클 비활성화
    }

    public bool GameEndScoreState()
    {
        //Debug.Log("GameEndScoreState");
        return score >= totalScroe;
    }


    // 게임 시작
    public IEnumerator GameStart()
    {
        ReStartScore(); //스코어 0으로
        playTime.StartTimer(); //TImer스크립트에 있는 플레이시간(30초)시작
        
        yield return null;
    }

    public IEnumerator ParticleEnd()
    {
        successParticle.SetActive(false);   //성공 파티클 비활성화
        yield return null;
    }

    //게임 종료
    public IEnumerator GameEnd()
    {
        //Debug.Log(GameEndScoreState());
        if (GameEndScoreState())
            successParticle.SetActive(true);   //성공 파티클 활성화
        //Debug.Log("???");

        yield return null;
    }


    

}
