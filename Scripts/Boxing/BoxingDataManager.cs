using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingDataManager : MonoBehaviour
{
    public static BoxingDataManager instance { get; private set; }

    //public GameObject successParticle;  //성공 시 파티클
    public BoxingTimer playTime;  //플레이 타이머

    public int score;   //점수
    public int totalScroe;




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
    }

    private void Update()
    {
        if (BoxingGameManager.instance.gamePlay)
        {
            //ShipClickOn();
            // _score = ScoreAdd(ShipCtrl.instance.ShipMoveDistance());
            BoxingUIManager.instance.SetScore(score);
        }
    }

    public void SetScore(int _score)
    {
        score += _score;
    }

    public bool GameEndScoreState()
    {
        //Debug.Log("GameEndScoreState");
        return score > 0;//>= totalScroe;
    }

    // 게임 시작
    public IEnumerator GameStart()
    {
        playTime.StartTimer(); //TImer스크립트에 있는 플레이시간(30초)시작
        yield return BagMoveCtrl.instance._BagDirection();

        yield return null;
    }

    public IEnumerator GameEnd()
    {
        BagMoveCtrl.instance.StopBag();
        //Debug.Log(GameEndScoreState());
        if (GameEndScoreState())
            //successParticle.SetActive(true);   //성공 파티클 활성화

        yield return null;
    }
}
