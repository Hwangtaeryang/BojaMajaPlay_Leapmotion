using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class CatchPang_UIManager : MonoBehaviour
{
    [Header("Top Score Img")]
    public GameObject topGroup;
    public GameObject desk;

    [Header("End Screen Obj")]
    public GameObject endScreen;
    public GameObject failScreen;
    public GameObject fantasticPan; //판타스틱 백배경
    public GameObject[] starLevel;  //별등급(게임진행중)
    public GameObject[] finishLevel;    //게임 끝났을 때 별등급
    public Text score;
    private string level;   //플레이어프리팹 저장네임
    //public GameObject HomeBtnCanvas;

    [Header("Lv Score Img")]
    public Image Level;
    private readonly FullScreenMode fullscreen;

    int scoreNum;
    int levelMax1 = 9000, levelMax2 = 25000, levelMax3 = 40000, levelMax4 = 55000;

    public static CatchPang_UIManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        // 캔버스 해상도 조절 > 16 : 9
        Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, fullscreen);
    }

    private void Update()
    {
        if (CatchPang_AppManager.Instance.gamePlay)
        {
            scoreNum = int.Parse(score.text);
            if (scoreNum > 0 && scoreNum <= levelMax1)
            {
                starLevel[0].SetActive(true);
            }
            else if (scoreNum > levelMax1 && scoreNum <= levelMax2)
            {
                starLevel[0].SetActive(true);
                starLevel[1].SetActive(true);
            }
            else if (scoreNum > levelMax2 && scoreNum <= levelMax3)
            {
                starLevel[0].SetActive(true);
                starLevel[1].SetActive(true);
                starLevel[2].SetActive(true);
            }
            else if (scoreNum > levelMax3 && scoreNum <= levelMax4)
            {
                starLevel[0].SetActive(true);
                starLevel[1].SetActive(true);
                starLevel[2].SetActive(true);
                starLevel[3].SetActive(true);
            }
            else if (scoreNum > levelMax4)
            {
                starLevel[0].SetActive(true);
                starLevel[1].SetActive(true);
                starLevel[2].SetActive(true);
                starLevel[3].SetActive(true);
                starLevel[4].SetActive(true);
            }

        }
    }

    public void SetScore(float points)
    {
        score.text = points.ToString(); // 스코어 0 초기화
    }
    
    // 시작버튼 눌렀을 때
    public IEnumerator OnRoundStart()
    {
        // 스코어 0점으로 리셋
        //score.gameObject.SetActive(true);
        SetScore(0);

        // 끝나고 Victory UI / Play Agin False
        endScreen.SetActive(false);
        fantasticPan.SetActive(false);
        // 실패 스크린
        failScreen.SetActive(false);

        //Debug.Log("UIManager Start");

        yield return null;
    }

    // 라운드 끝나면
    public IEnumerator OnRoundEnd()
    {
        // 점수 저장 특수문자 변경후
        //timeChage = timeChage.Replace(":", ".");
        //PlayerPrefs.SetFloat("CatchPangTime", float.Parse(timeChage));
        //PlayerPrefs.SetString("CatchPangTime", time.text);
        PlayerPrefs.SetString("CatchPangScore", score.text);


        if (CatchPang_DataManager.Instance.WonRound())
        {
            topGroup.SetActive(false);
            desk.SetActive(false);  //공떨어지는 책상 비활성화
            FinishLevelShow();  //별 등급 보여주기
            PlayerPrefs.SetString("CatchPangState", "Success");  //게임 성공실패 여부
            // 클리어
            endScreen.SetActive(true);
            //HomeBtnCanvas.SetActive(true);


            if (scoreNum > 0 && scoreNum <= levelMax1)
            {
                Level.sprite = Resources.Load<Sprite>("Textures/Level/Good");
                level = "Good";
            }
            else if (scoreNum > levelMax1 && scoreNum <= levelMax2)
            {
                Level.sprite = Resources.Load<Sprite>("Textures/Level/Great");
                level = "Great";
            }
            else if (scoreNum > levelMax2 && scoreNum <= levelMax3)
            {
                Level.sprite = Resources.Load<Sprite>("Textures/Level/Amazing");
                level = "Amazing";
            }
            else if (scoreNum > levelMax3 && scoreNum <= levelMax4)
            {
                Level.sprite = Resources.Load<Sprite>("Textures/Level/Excellent");
                level = "Excellent";
            }
            else if (scoreNum > levelMax4)
            {
                fantasticPan.SetActive(true);
                Level.sprite = Resources.Load<Sprite>("Textures/Level/Fantastic");
                level = "Fantastic";
            }

            PlayerPrefs.SetString("CatchPangLevel", level);

            StartCoroutine(NextSceneChange());  //다음 게임으로
        }
        else
        {
            topGroup.SetActive(false);
            desk.SetActive(false);  //공떨어지는 책상 비활성화
            FinishLevelShow();  //별 등급 보여주기
            PlayerPrefs.SetString("CatchPangState", "Failure");  //게임 성공실패 여부

            
            failScreen.SetActive(true);

            StartCoroutine(NextSceneChange());  //다음 게임으로
        }

        yield return null;
    }

    void FinishLevelShow()
    {
        if (scoreNum > 0 && scoreNum <= levelMax1)
        {
            finishLevel[0].SetActive(true);
        }
        else if (scoreNum > levelMax1 && scoreNum <= levelMax2)
        {
            finishLevel[0].SetActive(true);
            finishLevel[1].SetActive(true);
        }
        else if (scoreNum > levelMax2 && scoreNum <= levelMax3)
        {
            finishLevel[0].SetActive(true);
            finishLevel[1].SetActive(true);
            finishLevel[2].SetActive(true);
        }
        else if (scoreNum > levelMax3 && scoreNum <= levelMax4)
        {
            finishLevel[0].SetActive(true);
            finishLevel[1].SetActive(true);
            finishLevel[2].SetActive(true);
            finishLevel[3].SetActive(true);
        }
        else if (scoreNum > levelMax4)
        {
            finishLevel[0].SetActive(true);
            finishLevel[1].SetActive(true);
            finishLevel[2].SetActive(true);
            finishLevel[3].SetActive(true);
            finishLevel[4].SetActive(true);
        }
    }

    //게임끝나고 다음 게임으로 가는 함수
    IEnumerator NextSceneChange()
    {
        yield return new WaitForSeconds(5f);
        GameManager.instance.gamePlayNum += 1;

        //마지막 게임이 끝나기 전까지 
        if (GameManager.instance.gamePlayNum < GameManager.instance.gameTotalSu)
            GameManager.instance.SceneMove(GameManager.instance.gamePlayNum);
        else if (GameManager.instance.gamePlayNum == GameManager.instance.gameTotalSu)
        {
            SceneManager.LoadScene("EndScene");
        }
            
    }
}
