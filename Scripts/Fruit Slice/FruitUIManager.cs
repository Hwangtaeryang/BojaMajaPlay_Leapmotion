using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FruitUIManager : MonoBehaviour
{
    public static FruitUIManager Instance { get; private set; }
    

    public GameObject topText;

    [Header("[카운트다운]")]
    public Image startCountImg;    //3.2.1카운터 이미지
    public Image gameTitle; //시작 시 나오는 게임 타이틀

    [Header("[실패성공화면]")]
    public Image successImage;  //성공 시 레벨 타이틀
    public GameObject failureScrene;    //실패화면
    public GameObject successScene; //성공화면
    public GameObject fantasticPan; //판타스틱 백배경
    public GameObject[] starLevel;  //별등급(게임진행중)
    public GameObject[] finishLevel;    //게임 끝났을 때 별등급

    public FruitTimer playTimer; //리스타트 시 초기화 하기 위함

    [Header("[스코어]")]
    public Text score;
    public Text success_scroe;  //성공 시 점수


    public int startCountTime;  //3초 카운터
    public int endCountTime;    //5초 카운터
    int currStartCountTime; //현재 시작 카운터
    int currEndCountTime;   //현재 종료 카운터

    int scoreNum;
    int levelMax1 = 1000, levelMax2 = 2000, levelMax3 = 3000, levelMax4 = 4000;

    private readonly FullScreenMode fullscreen;

    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;

        

        currStartCountTime = startCountTime;    //시작카운터 초기화
        currEndCountTime = endCountTime;    //종료 카운터 초기화
        fantasticPan.SetActive(false);
    }

    private void Start()
    {
        // 캔버스 해상도 조절 > 16 : 9
        Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, fullscreen);
        //Debug.Log("Screen.width : " + Screen.width + ":::: (Screen.width*9) / 16 : " + (Screen.width * 9) / 16);
    }


    private void Update()
    {
        if (AppManager_FruitSlice.Instance.gamePlay)
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

    public void SetTotalScore(int _totel)
    {
        //totalScore.text = _totel.ToString();
    }

    public void SetScore(float points)
    {
        score.text = points.ToString();
    }

    //3.2.1카운터 코루틴
    public IEnumerator StartCount()
    {

        SetScore(0);    //재시작 시 점수 초기화
        currStartCountTime = startCountTime;    //시작카운터 초기화

        FruitSoundManager.Instance.OneTwoThreeSound();

        while (currStartCountTime > 0)
        {
            if (currStartCountTime > 3f)
                gameTitle.gameObject.SetActive(true);   //게임타이틀
            else
            {
                gameTitle.gameObject.SetActive(false);
                //startCountImg.gameObject.SetActive(true);   //3.2.1카운터
                startCountImg.sprite = Resources.Load<Sprite>("Textures/Count" + currStartCountTime);
            }

            currStartCountTime--;
            yield return new WaitForSecondsRealtime(0.8f);
        }

        startCountImg.gameObject.SetActive(false);
    }


    //5.4.3.2.1 종료 카운터 코루틴
    public IEnumerator EndCount()
    {
        //currEndCountTime = endCountTime;    //종료 카운터 초기화
        //endCountImg.gameObject.SetActive(true); //5.4.3.2.1 카운터

        //while (currEndCountTime > 0)
        //{
        //    //성공
        //    if (FruitDataManager.Instance.WonRound())
        //    {
        //        endCountImg.gameObject.transform.localPosition = new Vector3(184f, -418f, 0);
        //        //endCountImg.sprite = Resources.Load<Sprite>("Textures/Count" + currEndCountTime);
        //    }
        //    else //실패
        //    {
        //        endCountImg.gameObject.transform.localPosition = new Vector3(225f, -418f, 0);
        //        //endCountImg.sprite = Resources.Load<Sprite>("Textures/Count" + currEndCountTime);
        //    }

        //    endCountImg.text = (currEndCountTime--).ToString();
            yield return new WaitForSecondsRealtime(1f);
        //}

        //endCountImg.gameObject.SetActive(false);
    }
    

    public IEnumerator OnRoundStart()
    {
        // score.gameObject.SetActive(true);
        playTimer.ReStartTimer();   //타이머 초기화
        topText.SetActive(true);    //상단바 비활성화
        SetScore(0);

        // highscore.transform.parent.gameObject.SetActive(false);

        failureScrene.SetActive(false);
        successScene.SetActive(false);
        //StartCoroutine(StartCount());

        yield return null;
    }

    public IEnumerator OnRoundEnd()
    {
        if (FruitDataManager.Instance.WonRound())
        {
            topText.SetActive(false);   //상단바 비활성화
            FinishLevelShow();  //별 등급 보여주기
            FruitSoundManager.Instance.SuccessSound();  //성공 사운드
            successScene.SetActive(true);

            //시간 들고온다
            float timeNum = FruitTimer.copyTime;

            //Debug.Log(timeNum);
            //success_timer.text = (30f-timeNum).ToString("N2").Replace(".", ":");
            success_scroe.text = score.text;

            //PlayerPrefs.SetString("FruitTime", timeNum.ToString("N2").Replace(".", 
            PlayerPrefs.SetString("FruitState", "Success");  //게임 성공실패 여부
            PlayerPrefs.SetFloat("FruitTime", timeNum);
            PlayerPrefs.SetString("FruitScore", score.text);

            
            

            string playerLevel = "";

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


            PlayerPrefs.SetString("FruitLevel", playerLevel);

            StartCoroutine(EndCount());
            StartCoroutine(NextSceneChange());  //다음 게임으로
        }
        else
        {
            FinishLevelShow();  //별 등급 보여주기
            PlayerPrefs.SetString("FruitState", "Failure");  //게임 성공실패 여부

            FruitSoundManager.Instance.FailureSound();  //실패 사운드
            topText.SetActive(false);   //상단바 비활성화
            failureScrene.SetActive(true);
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

