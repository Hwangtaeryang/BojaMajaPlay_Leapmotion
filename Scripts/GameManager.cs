using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public AudioSource myAudio;
    public AudioSource bgmAudio;
    public AudioClip viewChange_sound;
    public AudioClip BtnClick_sound;

    public Image mainView;  //메인화면
    public int[] randomNum; //씬 변환 번호 저장


    public GameObject mainBtn;  //메인 버튼(립모션)



    //각각의 게임 성공 시 GameManager.instance.SceneMove(GameManager.instance.gamePlayNum);
    //사용하는데 GameManager.instance.playGameNum+=1로 꼭 해준다!
    public int gamePlayNum; //게임플레이 회수(0~9)
    public int gameTotalSu = 10;   //게임 갯수
    public int countForAdvertising = 0; // 게임 전면광고를 위한 횟수체크

    float randomTime; //화면 돌릴 시간

    public bool choiceGame = false;
    //AsyncOperation asyncOper;

    private void Awake()
    {
        if (instance == null)
        {
            //이 클래스 인스턴스가 생겼을 때 전역변수 instance에 게임 매니저 인스턴스가 담겨이지 않다면, 넣어줌
            instance = this;

            //씬 전환이 되어도 파괴되지 않게 한다.
            //DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 게임매니저가 존재할 수 있다.
            //그 경우 이전 씬에 사용하던 인스턴스를 계속 사용해주는 경우가 많은듯..
            //그래서 이미 전역변수인  instance에 인스턴스가 존재한다면 자신(새로운 씬의 게임메니저)을 삭제해준다.
            Destroy(gameObject);
        }
        myAudio = GetComponent<AudioSource>();
        InitGame();
    }



    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    //public static GameManager Instance
    //{
    //    get
    //    {
    //        if(null == instance)
    //        {
    //            return null;
    //        }
    //        return instance;
    //    }
    //}

    void InitGame()
    {
        Resources.UnloadUnusedAssets();
        gamePlayNum = 0;
        //게임 진행 순서
        randomTime = 10.1f;// Random.Range(6, 10);
        RandomNamberScene();
    }


    //랜덤으로 게임 순서 정하는 화면
    void RandomNamberScene()
    {
        randomNum = new int[gameTotalSu];
        bool isSame;

        for (int i = 0; i < gameTotalSu; i++)
        {
            while(true)
            {
                randomNum[i] = Random.Range(1, gameTotalSu + 1);
                isSame = false;

                for(int j = 0; j < i; j++)
                {
                    if(randomNum[j] == randomNum[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame)
                    break;
            }
            //Debug.Log(randomNum[i]);
        }
    }
    

    public IEnumerator GameMainTexture()
    {
        float _time = 0;
        float a = 0;

        while (_time <= randomTime)
        {
            _time+=0.5f;
            a = _time;

            //게임 배경을 선택 1~10이라 다시 돌려놓기위함
            if (a > 5f)
                a = a % 5f;

            mainView.sprite = Resources.Load<Sprite>("Textures/Main/" + (a * 2));
            ViewChangeSound();

            yield return new WaitForSecondsRealtime(0.1f);
        }

        if (_time >= randomTime)
        {
            //첫 시작은 randomNum[0]에 속한 숫자에 해당하는 게임 시작
            mainView.sprite = Resources.Load<Sprite>("Textures/Main/" + randomNum[gamePlayNum]);
            choiceGame = true;
        }
    }

    //게임 전환
    public void SceneMove(int playGameNum)
    {
        //Debug.Log(playGameNum);
        
        if (randomNum[playGameNum] == 1)    //마녀스프
        {
            AsyncOperation asyncOper =  SceneManager.LoadSceneAsync("WitchGame");
        }
        else if (randomNum[playGameNum] == 2)  //과일베기
        {
            AsyncOperation asyncOper = SceneManager.LoadSceneAsync("Fruit Slice");
        }
        else if (randomNum[playGameNum] == 3)  //차닦기
        {
            AsyncOperation asyncOper = SceneManager.LoadSceneAsync("Window");
        }
        else if (randomNum[playGameNum] == 4)  //삼겹살
        {
            AsyncOperation asyncOper = SceneManager.LoadSceneAsync("GrillingMeat");
        }
        else if (randomNum[playGameNum] == 5)  //캐치팡
        {
            AsyncOperation asyncOper = SceneManager.LoadSceneAsync("CatchPang");
        }
        else if (randomNum[playGameNum] == 6)  //호랑이잡기
        {
            AsyncOperation asyncOper = SceneManager.LoadSceneAsync("Ice Tiger");
        }
        else if (randomNum[playGameNum] == 7)  //바둑
        {
            AsyncOperation asyncOper = SceneManager.LoadSceneAsync("StoneGGagi");
        }
        else if (randomNum[playGameNum] == 8)  //모기잡기
        {
            AsyncOperation asyncOper = SceneManager.LoadSceneAsync("Mosquito");
        }
        else if (randomNum[playGameNum] == 9)  //권투
        {
            AsyncOperation asyncOper = SceneManager.LoadSceneAsync("Boxing");
        }
        else if (randomNum[playGameNum] == 10) //나무자르기
        {
            AsyncOperation asyncOper = SceneManager.LoadSceneAsync("TreeSlash");
        }
        
        
        //if (playGameNum == 11) //나무오르기
        //{
        //    AsyncOperation asyncOper = SceneManager.LoadSceneAsync("EndScene");
        //}

    }

    public void EndingScene()
    {
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync("EndScene");
    }

    //화면 바뀌는 사운드
    public void ViewChangeSound()
    {
        myAudio.PlayOneShot(viewChange_sound);
    }

    public void AllBtnOnClick()
    {
        myAudio.PlayOneShot(BtnClick_sound);
    }

    //일시정지
    public void AllSoundPause()
    {
        mainBtn.SetActive(false);
        myAudio.Pause();
        bgmAudio.Pause();
    }

    //재생
    public void AllSoundPlay()
    {
        mainBtn.SetActive(true);
        myAudio.UnPause();
        bgmAudio.UnPause();
    }

    //팝업창 닫기
    public void GameEndUnButtonOnClick()
    {
        //popup.SetActive(false); //팝업창 비활성화
    }

    //게임 종료
    public void GameEndOKButtonOnClick()
    {
        //PlayerPrefs.SetInt("Daimond", TimeManager.instance.diamondSu);   //다이아몬드 저장
        //PlayerPrefs.SetFloat("OverTime", TimeManager.instance.currFreeTime); //시간저장
        Application.Quit();
    }
}
