using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using System;

public class MainUIManager : MonoBehaviour
{
    public static bool mainImageChange; //랜덤버튼 눌렀는지 확인 여부
    public static bool startGameBtn;    //게임 시작 버튼 클릭 여부

    public GameObject mainBtn;  //랜덤버튼
    //public GameObject diamondPan;   //다이아 판

    public GameObject randomView;
    public GameObject startView;
    public GameObject storePanel;


    public GameObject mainCubeBtn;  //메인 버튼(립모션)
    public LeapMotionGameStartBtnOnClick gameMainbtn;
    public GameObject startCubeBtn; //스타트 버튼
    public LeapMotionGameStartBtnOnClick gameStartbtn;


    private readonly FullScreenMode fullscreen;

    //[Header("립모션버전")]
    //-----------------립모션 버전 ----------------
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

    //활성화된 윈도우-함수를 호출한 쓰레드와 연동된 녀석의 핸들을 받는다.
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();


    void Start()
    {
        gameMainbtn = mainCubeBtn.GetComponent<LeapMotionGameStartBtnOnClick>();
        gameStartbtn = startCubeBtn.GetComponent<LeapMotionGameStartBtnOnClick>();

        // 캔버스 해상도 조절 > 16 : 9
        Screen.SetResolution(Screen.width, (Screen.width * 9) / 16, fullscreen);

        mainImageChange = false;
        randomView.SetActive(true);
        startView.SetActive(false);

        mainBtn.SetActive(true);
        //mainBtnLeapMotion.SetActive(true);
        //diamondPan.SetActive(true);
    }


    void Update()
    {
        Invoke("Touch", 2f);

        //게임 선택 완료가 되었으면
        if (GameManager.instance.choiceGame)
        {
            startView.SetActive(true);   //시작 버튼 활성화

            GameManager.instance.choiceGame = false;
        }

        //게임 스타트 버튼 눌렀을 경우
        if (startGameBtn || gameStartbtn.startBtnClick)
        {
            //처음 시작하니깐 무조건 0번째
            GameManager.instance.SceneMove(GameManager.instance.gamePlayNum);

            //Debug.Log(GameManager.instance.gamePlayNum);
            startGameBtn = false;   //시작버튼 누른거 다시 false
            gameStartbtn.startBtnClick = false;
        }



        //게임종료
        //if (LeftFingerTouch.instance.closeBtnOnClick)// || RightFingerTouch.instance.closeBtnOnClick)
        //{
        //    CloseWindow();
        //}

        ////창 최소화
        //if (LeftFingerTouch.instance.miniBtnOnClick)// || RightFingerTouch.instance.miniBtnOnClick)
        //{
        //    MiniWindow();
        //}

    }

    public void Touch()
    {
        //게임 랜덤 선택 버튼 클릭했으면
        if (mainImageChange || gameMainbtn.mainBtnClick)
        {
            mainBtn.SetActive(false);
            StartCoroutine(GameManager.instance.GameMainTexture());

            mainImageChange = false;    //터치 끝났다는 상태 전환
            gameMainbtn.mainBtnClick = false;
            randomView.SetActive(false);
        }
    }

    //랜덤버튼 클릭(메인에서 게임 선택)
    public void RandomBtnOnClick()
    {
        mainBtn.SetActive(false);
        //mainBtnLeapMotion.SetActive(false); //립모션 버튼 비활성화
        mainImageChange = true;
        LeftFingerTouch.instance.mainBtnOnClick = false;    //터치 끝났다면 상태 전환
        RightFingerTouch.instance.mainBtnOnClick = false;    //터치 끝났다면 상태 전환
    }

    //게임 시작 버튼(메인에서 선택된 게임 시작)
    public void GameStartBtnOnClick()
    {
        startGameBtn = true;
        LeftFingerTouch.instance.mainBtnOnClick = true;
        RightFingerTouch.instance.mainBtnOnClick = true;
        //GameManager.instance.AllBtnOnClick();
    }

    //게임 종료
    public void CloseWindow()
    {
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

    ////상점 열기
    //public void StoreOpen()
    //{
    //    storePanel.SetActive(true);
    //}

    ////상점 닫기
    //public void StoreClose()
    //{
    //    storePanel.SetActive(false);
    //}
}
