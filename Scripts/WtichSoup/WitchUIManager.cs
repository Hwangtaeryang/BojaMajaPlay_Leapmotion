using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WitchUIManager : MonoBehaviour
{
    public static WitchUIManager instance { get; private set; }
    
    public GameObject topText;

    [Header("[카운트다운]")]
    public Image startCountImg;    //3.2.1카운터 이미지
    public Image gameTitle; //시작 시 나오는 게임 타이틀
    //public Text endCountImg;   //5.4.3.2.1 카운터 이미지

    [Header("[실패성공화면]")]
    public Image successImage;  //성공 시 레벨 타이틀
    public GameObject Failure;  //실패화면
    public GameObject Success;  //성공화면
    public GameObject fantasticPan; //판타스틱 백배경
    public WitchTimer playTimer; //리스타트 시 초기화 하기 위함
    public GameObject[] starLevel;  //별등급(게임진행중)
    public GameObject[] finishLevel;    //게임 끝났을 때 별등급

    [Header("[스코어]")]
    public Text score;
    public Text success_scroe;  //성공 시 점수


    [Header("[파티클]")]
    public GameObject[] scoreUpParticle;    //클릭할때마다 파티클 생김
    public GameObject[] bubbleParticle; //솥안에 거품 파티클
    public GameObject[] smokeParticle;  //연기 파티클
    public GameObject fireParticle;
    public GameObject fireDeathParticleParent;

    [Header("[거미]")]
    public GameObject spiderRed;
    public GameObject spiderBlue;
    public GameObject spiderGray;
    

    public int startCountTime;  //3초 카운터
    public int endCountTime;    //5초 카운터
    int currStartCountTime; //현재 시작 카운터
    int currEndCountTime;   //현재 종료 카운터
    int scoreNum;

    int levelMax1 = 5000, levelMax2 = 15000, levelMax3 = 30000, levelMax4 = 40000;

    Vector3 startSpiderRed, startSpiderBlue, startSpiderGray;

    private readonly FullScreenMode fullscreen;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        currStartCountTime = startCountTime;    //시작카운터 초기화
        currEndCountTime = endCountTime;    //종료 카운터 초기화

        //거품 초기화
        for (int i = 0; i < bubbleParticle.Length; i++)
            bubbleParticle[i].SetActive(false);
        //연기 초기화
        for (int j = 0; j < smokeParticle.Length; j++)
            smokeParticle[j].SetActive(false);

        //거미모델링 처음 위치 저장
        startSpiderRed = spiderRed.transform.localPosition;
        startSpiderBlue = spiderBlue.transform.localPosition;
        startSpiderGray = spiderGray.transform.localPosition;



    }


    void Start()
    {
        // 캔버스 해상도 조절 > 16 : 9
        Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, fullscreen);
    }

    private void Update()
    {
        if (WitchGameManager.instance.gamePlay)
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



    public void SetScore(int _score)
    {
        score.text = _score.ToString();
    }


    //3.2.1카운터 코루틴
    public IEnumerator StartCount()
    {
        
        SetScore(0);    //재시작 시 점수 초기화
        fantasticPan.SetActive(false);
        currStartCountTime = startCountTime;    //시작카운터 초기화

        
        while (currStartCountTime > 0)
        {
            if (currStartCountTime > 3f)
                gameTitle.gameObject.SetActive(true);   //게임타이틀
            else
            {
                gameTitle.gameObject.SetActive(false);
                startCountImg.gameObject.SetActive(true);   //3.2.1카운터
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
        currEndCountTime = endCountTime;    //종료 카운터 초기화
        //endCountImg.gameObject.SetActive(true); //5.4.3.2.1 카운터

        //while (currEndCountTime > 0)
        //{
        //    //성공
        //    if (WitchDataManager.instance.GameEndScoreState())
        //    {
        //        endCountImg.gameObject.transform.localPosition = new Vector3(186f, -419f, 0);
        //        //endCountImg.sprite = Resources.Load<Sprite>("Textures/Count" + currEndCountTime);
        //    }
        //    else //실패
        //    {
        //        endCountImg.gameObject.transform.localPosition = new Vector3(228f, -419f, 0);
        //        //endCountImg.sprite = Resources.Load<Sprite>("Textures/Count" + currEndCountTime);
        //    }

        //    endCountImg.text = (currEndCountTime--).ToString();
            yield return new WaitForSecondsRealtime(1f);
        //}

        //endCountImg.gameObject.SetActive(false);
    }


    public IEnumerator GameStart()
    {
        playTimer.ReStartTimer();   //타이머 초기화
        topText.SetActive(true);    //상단바 비활성화
        fireParticle.transform.localScale = new Vector3(1f, 1f, 1f);    //불꽃 사이즈 초기화
        //버블 파티클 초기화
        for (int i = 0; i < bubbleParticle.Length; i++)
            bubbleParticle[i].SetActive(false);

        //연기 초기화
        for (int j = 0; j < smokeParticle.Length; j++)
            smokeParticle[j].SetActive(false);

        //거미모델링 처음 위치 저장
        startSpiderRed = spiderRed.transform.localPosition;
        startSpiderBlue = spiderBlue.transform.localPosition;
        startSpiderGray = spiderGray.transform.localPosition;

        Success.SetActive(false);
        Failure.SetActive(false);
        //StartCoroutine(StartCount());
        

        yield return null;
    }
    
    public IEnumerator GameEnd()
    {

        //성공했을떄
        if (WitchDataManager.instance.GameEndScoreState())
        {
            topText.SetActive(false);   //상단바 비활성화
            FinishLevelShow();  //별 등급 보여주기
            WitchSoundManager.instance.SuccessSound();

            Success.SetActive(true);

            //시간 들고온다
            float timeNum = WitchTimer.copyTime;

            //Debug.Log(timeNum);
            //success_timer.text = (30f - timeNum).ToString("N2").Replace(".", ":");// + "\"";
            success_scroe.text = score.text;

            topText.SetActive(false);   //상단바 비활성화

            //PlayerPrefs.SetString("WithcTime", timeNum.ToString("N2").Replace(".", ":"));
            PlayerPrefs.SetString("WithcState", "Success");  //게임 성공실패 여부
            PlayerPrefs.SetFloat("WithcTime", timeNum);
            PlayerPrefs.SetString("WithcScore", score.text);

            
            string playerLevel ="";

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

            PlayerPrefs.SetString("WitchLevel", playerLevel);

            StartCoroutine(EndCount());
            StartCoroutine(NextSceneChange());  //다음 게임으로
        }
        else
        {
            topText.SetActive(false);   //상단바 비활성화
            FinishLevelShow();  //별 등급 보여주기
            PlayerPrefs.SetString("WithcState", "Failure");  //게임 성공실패 여부

            WitchSoundManager.instance.FailureSound();
            
            Failure.SetActive(true);
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

    //파티클 뿌려준다.
    public void ScoreUpParticle()
    {
        StartCoroutine(_ScoreUpParticle());
    }

    //클릭할때마다 파티클 생성
    public IEnumerator _ScoreUpParticle()
    {
        int num = Random.Range(0, scoreUpParticle.Length);
        scoreUpParticle[num].SetActive(true);

        yield return new WaitForSeconds(1.5f);
        scoreUpParticle[num].SetActive(false);
    }

    //획득 점수에 따른 파티클 효과
    public void ElapsedTimeScore(int _score)
    {
        
        //점수마다 파티클 효과 다르게 줌
        if(_score >= 3000 && _score < 8000)
        {
            

            //불 파티클 사이즈 키우기
            fireParticle.transform.localScale = new Vector3(1f, 1f, 1f);

            //버블 파티클 크기 변경
            bubbleParticle[0].SetActive(true); bubbleParticle[1].SetActive(false); bubbleParticle[2].SetActive(false);
            

            if (_score == 3000)
            {
                StartCoroutine(SpiderRedMove());    // 빨간 거미 내려옴
                WitchSoundManager.instance.BublleSoundStart();   //보글보글 사운드 한번 적용하기 위해서(sound loop돌림)
            }
                
                
        }
        else if(_score >= 8000 && _score < 13000)
        {
            fireParticle.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            bubbleParticle[0].SetActive(false); bubbleParticle[1].SetActive(true); bubbleParticle[2].SetActive(false);
            

            if (_score == 8000) 
            {
                StartCoroutine(SpiderBlueMove());   //파란거미 내려옴
                StartCoroutine(ShowDeathParticle());//폭발 파티클 적용
            }
                
        }
        else if (_score >= 13000 && _score < 18000)
        {
            fireParticle.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            bubbleParticle[0].SetActive(false); bubbleParticle[1].SetActive(false); bubbleParticle[2].SetActive(true);

            if (_score == 13000)
            {
                StartCoroutine(SpiderRedMove());    // 빨간 거미 내려옴
                StartCoroutine(ShowDeathParticle());
            }

            if(_score == 15000)
                StartCoroutine(SpiderBlueMove());   //파란거미 내려옴

        }
        else if (_score >= 18000 && _score < 20000)
        {
            fireParticle.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
            bubbleParticle[0].SetActive(false); bubbleParticle[1].SetActive(false); bubbleParticle[2].SetActive(true);

            if (_score == 18000)
            {
                StartCoroutine(SpierGrayMove());
                StartCoroutine(ShowDeathParticle());
            }
                
        }

        if (_score >= 5000f && _score < 12000)
        {
            smokeParticle[0].SetActive(true); smokeParticle[1].SetActive(false);
        }
        else if(_score >= 12000f)
        {
            smokeParticle[0].SetActive(false); smokeParticle[1].SetActive(true);
        }

    }

    //폭발 파티클 생성 함수
    IEnumerator ShowDeathParticle()
    {
        Transform child;
        child = fireDeathParticleParent.transform.GetChild(1);
        child.gameObject.SetActive(true);
        WitchSoundManager.instance.DeathSound();

        yield return new WaitForSeconds(1f);

        child.gameObject.SetActive(false);
    }

    //빨간거미 움직임 함수
    IEnumerator SpiderRedMove()
    {
        while (spiderRed.transform.localPosition.y > -0.155f)
        {
            spiderRed.transform.localPosition -= new Vector3(0, 0.2f * Time.deltaTime, 0f);

            if (spiderRed.transform.localPosition.y <= -0.155f)
                spiderRed.transform.localPosition = new Vector3(spiderRed.transform.localPosition.x, -0.155f, spiderRed.transform.localPosition.z);

            yield return new WaitForSeconds(0.05f);
        }

        while(spiderRed.transform.localPosition.y < 0.15f)
        {
            spiderRed.transform.localPosition += new Vector3(0, 0.2f * Time.deltaTime, 0f);

            if (spiderRed.transform.localPosition.y >= 0.15f)
                spiderRed.transform.localPosition = new Vector3(spiderRed.transform.localPosition.x, 0.15f, spiderRed.transform.localPosition.z);

            yield return new WaitForSeconds(0.05f);
        }

        
    }

    //파란거미 움직임 함수
    IEnumerator SpiderBlueMove()
    {
        while (spiderBlue.transform.localPosition.y > -0.082f)
        {
            spiderBlue.transform.localPosition -= new Vector3(0, 0.2f * Time.deltaTime, 0f);

            if (spiderBlue.transform.localPosition.y <= -0.082f)
                spiderBlue.transform.localPosition = new Vector3(spiderBlue.transform.localPosition.x, -0.082f, spiderBlue.transform.localPosition.z);

            yield return new WaitForSeconds(0.05f);
        }

        while(spiderBlue.transform.localPosition.y < 0.16f)
        {
            spiderBlue.transform.localPosition += new Vector3(0, 0.2f * Time.deltaTime, 0f);

            if (spiderBlue.transform.localPosition.y >= 0.16f)
                spiderBlue.transform.localPosition = new Vector3(spiderBlue.transform.localPosition.x, 0.16f, spiderBlue.transform.localPosition.z);

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator SpierGrayMove()
    {
        while(spiderGray.transform.localPosition.y > -0.16f)
        {
            spiderGray.transform.localPosition -= new Vector3(0f, 0.2f * Time.deltaTime, 0f);

            if (spiderGray.transform.localPosition.y <= -0.16f)
                spiderGray.transform.localPosition = new Vector3(spiderGray.transform.localPosition.x, -0.16f, spiderGray.transform.localPosition.z);

            yield return new WaitForSeconds(0.05f);
        }

        while(spiderGray.transform.localPosition.y < 0.08f)
        {
            spiderGray.transform.localPosition += new Vector3(0, 0.2f * Time.deltaTime, 0f);

            if (spiderGray.transform.localPosition.y >= 0.08f)
                spiderGray.transform.localPosition = new Vector3(spiderGray.transform.localPosition.x, 0.08f, spiderGray.transform.localPosition.z);

            yield return new WaitForSeconds(0.05f);
        }
    }

}
