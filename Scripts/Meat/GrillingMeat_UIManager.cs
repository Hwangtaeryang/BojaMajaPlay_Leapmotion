using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GrillingMeat_UIManager : MonoBehaviour
{
    int gameSu = 4;

    [Header("Top Score Img")]
    public GameObject topGroup;

    [Header("End Screen Obj")]
    public GameObject endScreen;
    public GameObject failScreen;
    public GameObject fantasticPan; //판타스틱 백배경
    public GameObject[] starLevel;  //별등급(게임진행중)
    public GameObject[] finishLevel;    //게임 끝났을 때 별등급
    public Text score;
    private string level;
    string timeChage;   //시간 특수문자 빼고 변경하기 위한 변수
    //public GameObject HomeBtnCanvas;

    [Header("Lv Score Img")]
    //public Image Medal;
    public Image Level;
    private readonly FullScreenMode fullscreen;


    int scoreNum;
    int levelMax1 = 3000, levelMax2 = 9000, levelMax3 = 15000, levelMax4 = 20000;

    public static GrillingMeat_UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        // 캔버스 해상도 조절 > 16 : 9
        Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, fullscreen);

        //Debug.Log("Screen.width : " + Screen.width + ":::: (Screen.width*9) / 16 : " + (Screen.width * 9) / 16);
    }

    private void Update()
    {
        if (GrillingMeat_AppManager.Instance.gamePlay)
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


    public void SetScore(int points)
    {
        score.text = points.ToString(); // 스코어 제어
    }

    // UI 시작
    public IEnumerator _UI_Start()
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
    // UI 끝 : 재시작 ? 메뉴 
    public IEnumerator _UI_End()
    {
        //PlayerPrefs.SetString("GrillingMeatTime", time.text);
        //PlayerPrefs.SetFloat("GrillingMeatTime", float.Parse(timeChage));
        PlayerPrefs.SetString("GrillingMeatScore", score.text);
        

        if (GrillingMeat_DataManager.Instance.WonRound())
        {
            topGroup.SetActive(false);
            FinishLevelShow();  //별 등급 보여주기
            PlayerPrefs.SetString("GrillingMeatState", "Success");  //게임 성공실패 여부

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
            PlayerPrefs.SetString("GrillingMeatLevel", level);


            StartCoroutine(NextSceneChange());  //다음 게임으로
        }
        else
        {
            topGroup.SetActive(false);
            PlayerPrefs.SetString("GrillingMeatState", "Failure");  //게임 성공실패 여부
            
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
        {
            GameManager.instance.SceneMove(GameManager.instance.gamePlayNum);
        }
        else if (GameManager.instance.gamePlayNum == GameManager.instance.gameTotalSu)
        {
            SceneManager.LoadScene("EndScene");
        }
            
    }
}
