using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{
    public static EndManager instance { get; private set; }

    //지정된 창의 표시 상태 설정
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

    //활성화된 윈도우-함수를 호출한 쓰레드와 연동된 녀석의 핸들을 받는다.
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    //public Image[] levelBox;

    public Image[] gameImage;   //게임 제목
    public Image[] levelBackImage;  //레벨(시계) 백 그라운드 이미지
    public Image[] timeBack;    //시계 백배경
    public GameObject[] timeText; // TIME글자오브젝트
    public Text[] scoreText;    //점수

    public GameObject[] pos;    //시계 생성할 위치
    public GameObject[] levelPrefabs;   //등급별로 생성할 시계
    public GameObject[] copyObj;    //복사해서 쓸 오브젝트


    int gameSu = 10;
    int[] levelMax1, levelMax2, levelMax3, levelMax4;

    private readonly FullScreenMode fullscreen;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

        copyObj = new GameObject[gameSu];

        for (int i = 0; i < gameSu; i++)
        {
            //다시 들어왔을 때 일단 오브젝트 전부 살려놓기
            scoreText[i].gameObject.SetActive(true);
            timeText[i].SetActive(true);
        }
    }

    void Start()
    {
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
        // 캔버스 해상도 조절 > 16 : 9
        Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, fullscreen);

        //AdManager.Instance.ToggleAd(false);

        StartCoroutine(EndResult());



        levelMax1 = new int[10]; levelMax2 = new int[10]; levelMax3 = new int[10]; levelMax4 = new int[10];

        // 마녀, 과일, 차닦기, 고기, 물풍선, 호랑이, 바둑, 모기, 권투, 나무
        levelMax1[0] = 5000; levelMax1[1] = 1000; levelMax1[2] = 5000; levelMax1[3] = 3000; levelMax1[4] = 9000;
        levelMax1[5] = 6000; levelMax1[6] = 2000; levelMax1[7] = 1000; levelMax1[8] = 2000; levelMax1[9] = 1000;

        levelMax2[0] = 15000; levelMax2[1] = 2000; levelMax2[2] = 10000; levelMax2[3] = 9000; levelMax2[4] = 25000;
        levelMax2[5] = 18000; levelMax2[6] = 4000; levelMax2[7] = 2500; levelMax2[8] = 5000; levelMax2[9] = 2500;

        levelMax3[0] = 30000; levelMax3[1] = 3000; levelMax3[2] = 15000; levelMax3[3] = 15000; levelMax3[4] = 40000;
        levelMax3[5] = 28000; levelMax3[6] = 6000; levelMax3[7] = 4000; levelMax3[8] = 8000; levelMax3[9] = 4000;

        levelMax4[0] = 40000; levelMax4[1] = 4000; levelMax4[2] = 25000; levelMax4[3] = 20000; levelMax4[4] = 55000;
        levelMax4[5] = 40000; levelMax4[6] = 8000; levelMax4[7] = 5000; levelMax4[8] = 10000; levelMax4[9] = 5000;
    }

    private void Update()
    {
        
    }

    //게임 종료
    public void CloseWindow()
    {
        for (int i = 0; i < 10; i++)
            Destroy(copyObj[i]);

        LeftFingerTouch.instance.closeBtnOnClick = false;
        RightFingerTouch.instance.closeBtnOnClick = false;
        Application.Quit();
    }

    //윈도우창 작게
    public void MiniWindow()
    {
        ShowWindow(GetActiveWindow(), 2);
        LeftFingerTouch.instance.miniBtnOnClick = false;
        RightFingerTouch.instance.miniBtnOnClick = false;
    }

    //홈으로
    public void HomeButtonOnClick()
    {
        for (int i = 0; i < 10; i++)
            Destroy(copyObj[i]);

        LeftFingerTouch.instance.homeBtnOnClick = false;
        RightFingerTouch.instance.homeBtnOnClick = false;
        SceneManager.LoadScene("Main");
    }


    IEnumerator EndResult()
    {
        yield return new WaitForSeconds(0.1f);


        for (int i = 0; i < gameSu; i++)
        {
            gameImage[i].sprite = Resources.Load<Sprite>("Textures/Main/Title/" + GameManager.instance.randomNum[i]);


            if (GameManager.instance.randomNum[i] == 1)  //마녀스프
            {
                if (PlayerPrefs.GetString("WithcState") == "Failure")
                {
                    timeText[i].SetActive(false); scoreText[i].gameObject.SetActive(false); //시간, TIME텍스트 비활성화
                    timeBack[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/GameOver");
                }
                else
                    scoreText[i].text = PlayerPrefs.GetString("WithcScore");

                ScoreLevel(PlayerPrefs.GetString("WithcState"), int.Parse(PlayerPrefs.GetString("WithcScore")), i, 0);
                //ScoreLevelChoice(i, 0, int.Parse(PlayerPrefs.GetString("WithcScore")), PlayerPrefs.GetString("WithcState"));
            }
            else if (GameManager.instance.randomNum[i] == 2)  //과일베기
            {
                if (PlayerPrefs.GetString("FruitState") == "Failure")
                {
                    timeText[i].SetActive(false); scoreText[i].gameObject.SetActive(false); //시간, TIME텍스트 비활성화
                    timeBack[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/GameOver");
                }
                else
                    scoreText[i].text = PlayerPrefs.GetString("FruitScore");

                ScoreLevel(PlayerPrefs.GetString("FruitState"), int.Parse(PlayerPrefs.GetString("FruitScore")), i, 1);
                //ScoreLevelChoice(i, 1, int.Parse(PlayerPrefs.GetString("FruitScore")), PlayerPrefs.GetString("FruitState"));
            }
            else if (GameManager.instance.randomNum[i] == 3)  //차닦기
            {
                if (PlayerPrefs.GetString("WindowState") == "Failure")
                {
                    timeText[i].SetActive(false); scoreText[i].gameObject.SetActive(false); //시간, TIME텍스트 비활성화
                    timeBack[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/GameOver");
                }
                else
                    scoreText[i].text = PlayerPrefs.GetString("WindowScore");

                ScoreLevel(PlayerPrefs.GetString("WindowState"), int.Parse(PlayerPrefs.GetString("WindowScore")), i, 2);
                //ScoreLevelChoice(i, 2, int.Parse(PlayerPrefs.GetString("WindowScore")), PlayerPrefs.GetString("WindowState"));
            }
            else if (GameManager.instance.randomNum[i] == 4)  //삼겹살
            {
                if (PlayerPrefs.GetString("GrillingMeatState") == "Failure")
                {
                    timeText[i].SetActive(false); scoreText[i].gameObject.SetActive(false); //시간, TIME텍스트 비활성화
                    timeBack[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/GameOver");
                }
                else
                    scoreText[i].text = PlayerPrefs.GetString("GrillingMeatScore");

                ScoreLevel(PlayerPrefs.GetString("GrillingMeatState"), int.Parse(PlayerPrefs.GetString("GrillingMeatScore")), i, 3);
                //ScoreLevelChoice(i, 3, int.Parse(PlayerPrefs.GetString("GrillingMeatScore")), PlayerPrefs.GetString("GrillingMeatState"));
            }
            else if (GameManager.instance.randomNum[i] == 5)  //캐치팡
            {
                if (PlayerPrefs.GetString("CatchPangState") == "Failure")
                {
                    timeText[i].SetActive(false); scoreText[i].gameObject.SetActive(false); //시간, TIME텍스트 비활성화
                    timeBack[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/GameOver");
                }
                else
                    scoreText[i].text = PlayerPrefs.GetString("CatchPangScore");

                ScoreLevel(PlayerPrefs.GetString("CatchPangState"), int.Parse(PlayerPrefs.GetString("CatchPangScore")), i, 4);
                //ScoreLevelChoice(i, 4, int.Parse(PlayerPrefs.GetString("CatchPangScore")), PlayerPrefs.GetString("CatchPangState"));
            }
            else if (GameManager.instance.randomNum[i] == 6)  //백호
            {
                if (PlayerPrefs.GetString("IceTigerState") == "Failure")
                {
                    timeText[i].SetActive(false); scoreText[i].gameObject.SetActive(false); //시간, TIME텍스트 비활성화
                    timeBack[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/GameOver");
                }
                else
                    scoreText[i].text = PlayerPrefs.GetString("IceTigerScore");

                ScoreLevel(PlayerPrefs.GetString("IceTigerState"), int.Parse(PlayerPrefs.GetString("IceTigerScore")), i, 5);
                //ScoreLevelChoice(i, 5, int.Parse(PlayerPrefs.GetString("IceTigerScore")), PlayerPrefs.GetString("IceTigerState"));
            }
            else if (GameManager.instance.randomNum[i] == 7)  //바둑
            {
                //timerText[i].text = PlayerPrefs.GetString("BasketballTime");
                if (PlayerPrefs.GetString("GoState") == "Failure")
                {
                    timeText[i].SetActive(false); scoreText[i].gameObject.SetActive(false); //시간, TIME텍스트 비활성화
                    timeBack[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/GameOver");
                }
                else
                    scoreText[i].text = PlayerPrefs.GetString("GoScore");

                ScoreLevel(PlayerPrefs.GetString("GoState"), int.Parse(PlayerPrefs.GetString("GoScore")), i, 6);
                //ScoreLevelChoice(i, 6, int.Parse(PlayerPrefs.GetString("GoScore")), PlayerPrefs.GetString("GoState"));
            }
            else if (GameManager.instance.randomNum[i] == 8)  //모기잡기
            {
                //timerText[i].text = PlayerPrefs.GetString("MosquitoTime");
                if (PlayerPrefs.GetString("MosquitoState") == "Failure")
                {
                    timeText[i].SetActive(false); scoreText[i].gameObject.SetActive(false); //시간, TIME텍스트 비활성화
                    timeBack[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/GameOver");
                }
                else
                    scoreText[i].text = PlayerPrefs.GetString("MosquitoScore");

                ScoreLevel(PlayerPrefs.GetString("MosquitoState"), int.Parse(PlayerPrefs.GetString("MosquitoScore")), i, 7);
                //ScoreLevelChoice(i, 7, int.Parse(PlayerPrefs.GetString("MosquitoScore")), PlayerPrefs.GetString("MosquitoState"));
            }
            else if (GameManager.instance.randomNum[i] == 9)  //권투
            {
                //timerText[i].text = PlayerPrefs.GetString("CarRaceTime");
                if (PlayerPrefs.GetString("BoxingState") == "Failure")
                {
                    timeText[i].SetActive(false); scoreText[i].gameObject.SetActive(false); //시간, TIME텍스트 비활성화
                    timeBack[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/GameOver");
                }
                else
                    scoreText[i].text = PlayerPrefs.GetString("BoxingScore");

                ScoreLevel(PlayerPrefs.GetString("BoxingState"), int.Parse(PlayerPrefs.GetString("BoxingScore")), i, 8);
                //ScoreLevelChoice(i, 8, int.Parse(PlayerPrefs.GetString("BoxingScore")), PlayerPrefs.GetString("BoxingState"));
            }
            else if (GameManager.instance.randomNum[i] == 10)  //나무자르기
            {
                if (PlayerPrefs.GetString("TreeSlashState") == "Failure")
                {
                    timeText[i].SetActive(false); scoreText[i].gameObject.SetActive(false); //시간, TIME텍스트 비활성화
                    timeBack[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/GameOver");
                }
                else
                    scoreText[i].text = PlayerPrefs.GetString("TreeSlashScore");

                ScoreLevel(PlayerPrefs.GetString("TreeSlashState"), int.Parse(PlayerPrefs.GetString("TreeSlashScore")), i, 9);
                //ScoreLevelChoice(i, 9, int.Parse(PlayerPrefs.GetString("TreeSlashScore")), PlayerPrefs.GetString("TreeSlashState"));
                //levelText[i].text = PlayerPrefs.GetString("StarshipLevel");
                //timerText[i].text = PlayerPrefs.GetString("StarshipTime");
                //LevelColor(i, levelText[i].text);
            }
        }
    }

    //시간에 따른 시간등급 이미지 선택함수
    void ClickLevelChoice(int num, int _score, string gameState)
    {
        if (gameState == "Success")
        {
            if (_score <= 10f && _score > 0f)
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_1");
                copyObj[num] = Instantiate(levelPrefabs[0], pos[num].transform);
            }
            else if (_score <= 15f && _score > 10f)
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_2");
                copyObj[num] = Instantiate(levelPrefabs[1], pos[num].transform);
            }
            else if (_score <= 20f && _score > 15f)
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_3");
                copyObj[num] = Instantiate(levelPrefabs[2], pos[num].transform);
            }
            else if (_score <= 25f && _score > 20f)
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_4");
                copyObj[num] = Instantiate(levelPrefabs[3], pos[num].transform);
            }
            else if (_score < 30f && _score > 25f)
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_5");
                copyObj[num] = Instantiate(levelPrefabs[4], pos[num].transform);
            }
        }
        else if (gameState == "Failure")
        {
            //if (timer <= 0)
            //{
            levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_6");
            copyObj[num] = Instantiate(levelPrefabs[5], pos[num].transform);
            //}
        }
    }


    void ScoreLevel(string gameState, int _score, int num, int levelNum)
    {
        if (gameState == "Success")
        {
            if (_score > 0 && _score <= levelMax1[levelNum])
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_5");
                copyObj[num] = Instantiate(levelPrefabs[4], pos[num].transform);
            }
            else if (_score > levelMax1[levelNum] && _score <= levelMax2[levelNum])
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_4");
                copyObj[num] = Instantiate(levelPrefabs[3], pos[num].transform);
            }
            else if (_score > levelMax2[levelNum] && _score <= levelMax3[levelNum])
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_3");
                copyObj[num] = Instantiate(levelPrefabs[2], pos[num].transform);
            }
            else if (_score > levelMax3[levelNum] && _score <= levelMax4[levelNum])
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_2");
                copyObj[num] = Instantiate(levelPrefabs[1], pos[num].transform);
            }
            else if (_score > levelMax4[levelNum])
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_1");
                copyObj[num] = Instantiate(levelPrefabs[0], pos[num].transform);
            }
        }
        else if (gameState == "Failure")
        {
            //if (timer <= 0)
            //{
            levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_6");
            copyObj[num] = Instantiate(levelPrefabs[5], pos[num].transform);
            //}
        }
    }




    //점수에 따른 등급 이미지 선택함수
    void ScoreLevelChoice(int num, int levelNum,  int _score, string gameState)
    {
        if (gameState == "Success")
        {

            if (_score > 0 && _score <= levelMax1[levelNum])
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_1");
                copyObj[num] = Instantiate(levelPrefabs[0], pos[num].transform);
            }
            else if (_score > levelMax1[levelNum] && _score <= levelMax2[levelNum])
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_2");
                copyObj[num] = Instantiate(levelPrefabs[1], pos[num].transform);
            }
            else if (_score > levelMax2[levelNum] && _score <= levelMax3[levelNum])
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_3");
                copyObj[num] = Instantiate(levelPrefabs[2], pos[num].transform);
            }
            else if (_score > levelMax3[levelNum] && _score <= levelMax4[levelNum])
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_4");
                copyObj[num] = Instantiate(levelPrefabs[3], pos[num].transform);
            }
            else if (_score > levelMax4[levelNum])
            {
                levelBackImage[num].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_5");
                copyObj[num] = Instantiate(levelPrefabs[4], pos[num].transform);
            }





            //if (gameNum == 1)   //마녀
            //{
            //    //점수, 게임실제번호, 저장된 순서번호
            //    LevelGauge(_score, 0, num);
            //}
            //else if(gameNum == 2)   //과일
            //{
            //    LevelGauge(_score, 1, num);
            //}
            //else if (gameNum == 3)  //차
            //{
            //    LevelGauge(_score, 2, num);
            //}
            //else if (gameNum == 4)  //고기
            //{
            //    LevelGauge(_score, 3, num);
            //}
            //else if (gameNum == 5)  //물풍선
            //{
            //    LevelGauge(_score, 4, num);
            //}
            //else if (gameNum == 6)  //백호
            //{
            //    LevelGauge(_score, 5, num);
            //}
            //else if (gameNum == 7)  //바둑
            //{
            //    LevelGauge(_score, 6, num);
            //}
            //else if (gameNum == 8)  //모기
            //{
            //    LevelGauge(_score, 7, num);
            //}
            //else if (gameNum == 9)  //권투
            //{
            //    LevelGauge(_score, 8, num);
            //}
            //else if (gameNum == 10) //나무
            //{
            //    LevelGauge(_score, 9, num);
            //}

        }
    }

    public void LevelGauge(int _score, int num, int i)
    {
        if (_score > 0 && _score <= levelMax1[num])
        {
            levelBackImage[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_1");
            copyObj[i] = Instantiate(levelPrefabs[4], pos[i].transform);
        }
        else if (_score > levelMax1[num] && _score <= levelMax2[num])
        {
            levelBackImage[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_2");
            copyObj[i] = Instantiate(levelPrefabs[3], pos[i].transform);
        }
        else if (_score > levelMax2[num] && _score <= levelMax3[num])
        {
            levelBackImage[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_3");
            copyObj[i] = Instantiate(levelPrefabs[2], pos[i].transform);
        }
        else if (_score > levelMax3[num] && _score <= levelMax4[num])
        {
            levelBackImage[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_4");
            copyObj[i] = Instantiate(levelPrefabs[1], pos[i].transform);
        }
        else if (_score > levelMax4[num])
        {
            levelBackImage[i].sprite = Resources.Load<Sprite>("Textures/Main/ClickBack/Level_5");
            copyObj[i] = Instantiate(levelPrefabs[0], pos[i].transform);
        }
    }


    //게임 종료
    public void GameEndOKButtonOnClick()
    {
        for (int i = 0; i < 10; i++)
            Destroy(copyObj[i]);

        Application.Quit();
    }
}
