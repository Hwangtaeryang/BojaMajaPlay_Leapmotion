using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoUIManager : MonoBehaviour
{
    public static GoUIManager instance { get; private set; }

    [Header("[카운트다운]")]
    public Image startCountImg;    //3.2.1카운터 이미지
    public Image gameTitle; //시작 시 나오는 게임 타이틀
    //public Text endCountImg;   //5.4.3.2.1 카운터 이미지

    [Header("[실패성공화면]")]
    public Image successImage;  //성공 시 레벨 타이틀
    public GameObject Failure;  //실패화면
    public GameObject Success;  //성공화면
    public GameObject fantasticPan; //판타스틱 백배경
    public GameObject[] starLevel;  //별등급(게임진행중)
    public GameObject[] finishLevel;    //게임 끝났을 때 별등급
    public GoTimer playTimer; //리스타트 시 초기화 하기 위함
    public GameObject topTextGroup;
    public GameObject goPan;    //바둑판

    [Header("[스코어]")]
    public Text score;
    public Text success_scroe;  //성공 시 점수

    public int startCountTime;  //3초 카운터
    public int endCountTime;    //5초 카운터
    int currStartCountTime; //현재 시작 카운터
    int currEndCountTime;   //현재 종료 카운터

    int scoreNum;
    int levelMax1 = 2000, levelMax2 = 4000, levelMax3 = 6000, levelMax4 = 8000;

    private float f_totalScore;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        currStartCountTime = startCountTime;    //시작카운터 초기화
        currEndCountTime = endCountTime;    //종료 카운터 초기화
    }

    // Start is called before the first frame update
    void Start()
    {
        f_totalScore = 0f;
    }

    
    private void Update()
    {
        if (GoGameManager.instance.gamePlay)
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

    //3.2.1카운터 코루틴
    public IEnumerator StartCount()
    {
        currStartCountTime = startCountTime;    //시작카운터 초기화

        while (currStartCountTime > 0)
        {
            if (currStartCountTime > 3f)
                gameTitle.gameObject.SetActive(true);   //게임타이틀
            else
            {
                gameTitle.gameObject.SetActive(false);
                startCountImg.gameObject.SetActive(true);   //3.2.1카운터
                startCountImg.sprite = Resources.Load<Sprite>("Count" + currStartCountTime);
            }
            currStartCountTime--;
            yield return new WaitForSecondsRealtime(1f);
        }

        startCountImg.gameObject.SetActive(false);
    }

    //5.4.3.2.1 종료 카운터 코루틴
    public IEnumerator EndCount()
    {
        currEndCountTime = endCountTime;    //종료 카운터 초기화
        yield return new WaitForSecondsRealtime(1f);
    }

    public IEnumerator GameStart()
    {
        //playTimer.ReStartTimer();   //타이머 초기화
        //topText.SetActive(true);    //상단바 비활성화
        topTextGroup.SetActive(true);
        Success.SetActive(false);
        Failure.SetActive(false);
        //StartCoroutine(StartCount());

        yield return null;
    }

    public void SetScore(float points)
    {
        score.text = points.ToString(); // 스코어 0 초기화
        f_totalScore = points;
    }

    public IEnumerator GameEnd()
    {
        topTextGroup.SetActive(false);
        goPan.SetActive(false);

        if (GoDataManager.instance.GameEndScoreState())
        {
            FinishLevelShow();  //별 등급 보여주기
            Success.SetActive(true);

            //시간 들고온다
            float timeNum = GoTimer.copyTime;

            success_scroe.text = score.text;

            PlayerPrefs.SetString("GoState", "Success");  //게임 성공실패 여부
            PlayerPrefs.SetFloat("GoTime", timeNum);
            PlayerPrefs.SetString("GoScore", score.text);


            string playerLevel = "";

            //성공한 시간에 따라 성공 타이틀 변경
            if (scoreNum > 0 && scoreNum <= levelMax1)
            {
                successImage.sprite = Resources.Load<Sprite>("Textures/Level/Good");
                playerLevel = "Good";
            }
            else if (scoreNum > levelMax1 && scoreNum <= levelMax2)
            {
                successImage.sprite = Resources.Load<Sprite>("Textures/Level/Great");
                playerLevel = "Great";
            }
            else if (scoreNum > levelMax2 && scoreNum <= levelMax3)
            {
                successImage.sprite = Resources.Load<Sprite>("Textures/Level/Amazing");
                playerLevel = "Amazing";
            }
            else if (scoreNum > levelMax3 && scoreNum <= levelMax4)
            {
                successImage.sprite = Resources.Load<Sprite>("Textures/Level/Excellent");
                playerLevel = "Excellent";
            }
            else if (scoreNum > levelMax4)
            {
                fantasticPan.SetActive(true);
                successImage.sprite = Resources.Load<Sprite>("Textures/Level/Fantastic");
                playerLevel = "Fantastic";
            }

            PlayerPrefs.SetString("GoLevel", playerLevel);
            GoSoundManager.Instance.bgmAfterGameEnd("Success Screen");
            StartCoroutine(EndCount());
            StartCoroutine(NextSceneChange());  //다음 게임으로
        }
        else
        {
            PlayerPrefs.SetString("GoState", "Failure");  //게임 성공실패 여부

            
            //topText.SetActive(false);   //상단바 비활성화
            Failure.SetActive(true);

            GoSoundManager.Instance.bgmAfterGameEnd("Fail Screen");
            StartCoroutine(EndCount());
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

    IEnumerator NextSceneChange()
    {
        yield return new WaitForSeconds(5f);
        GameManager.instance.gamePlayNum += 1;

        //마지막 게임이 끝나기 전까지 
        if (GameManager.instance.gamePlayNum < GameManager.instance.gameTotalSu)
            GameManager.instance.SceneMove(GameManager.instance.gamePlayNum);
        else if (GameManager.instance.gamePlayNum == GameManager.instance.gameTotalSu)
            SceneManager.LoadScene("EndScene");
    }
}
