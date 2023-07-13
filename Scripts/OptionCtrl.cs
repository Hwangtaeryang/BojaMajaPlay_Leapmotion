using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class OptionCtrl : MonoBehaviour
{
    //지정된 창의 표시 상태 설정
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

    //활성화된 윈도우-함수를 호출한 쓰레드와 연동된 녀석의 핸들을 받는다.
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();


    public GameObject optionPopup;

    public int optionCount = 0;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameEnd();
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            //옵션창 열기
            if(optionCount == 0)
            {
                optionCount = 1;
                PressPause();

                
            }
            //옵션창 닫기
            else if(optionCount == 1)
            {
                PressPlay();
                optionCount = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("홈");
            HomeBtn();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("메인");
            WindowViewDown();
        }
    }

    //일시정지
    public void _PressPause()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            
            GameManager.instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "WitchGame")
        {
            WitchSoundManager.instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "Fruit Slice")
        {
            FruitSoundManager.Instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "Window")
        {
            WindowSoundManager.instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "GrillingMeat")
        {
            GrillingMeat_SoundManager.Instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "CatchPang")
        {
            CatchPang_SoundManager.Instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "Ice Tiger")
        {
            IceTiger_SoundManager.Instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "StoneGGagi")
        {
            GoSoundManager.Instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "Mosquito")
        {
            SoundManager.Instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "Boxing")
        {
            BoxingSoundManager.instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "TreeSlash")
        {
            TreeSlashSoundManager.Instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "EndScene")
        {
            GameManager.instance.AllSoundPause();
        }

        Time.timeScale = 0f;
    }

    //사운드 재생
    public void _AllSoundPlay()
    {
        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().name == "Main")
        {
            GameManager.instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "WitchGame")
        {
            WitchSoundManager.instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "Fruit Slice")
        {
            FruitSoundManager.Instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "Window")
        {
            WindowSoundManager.instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "GrillingMeat")
        {
            GrillingMeat_SoundManager.Instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "CatchPang")
        {
            CatchPang_SoundManager.Instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "Ice Tiger")
        {
            IceTiger_SoundManager.Instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "StoneGGagi")
        {
            GoSoundManager.Instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "Mosquito")
        {
            SoundManager.Instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "Boxing")
        {
            BoxingSoundManager.instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "TreeSlash")
        {
            TreeSlashSoundManager.Instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "EndScene")
        {
            GameManager.instance.AllSoundPause();
        }

    }

    //일시정지
    public void PressPause()
    {
        optionPopup.SetActive(true);
        _PressPause();
        //BoxingSoundManager.instance.AllSoundPause();
        Time.timeScale = 0f;
    }

    //게임 플레이
    public void PressPlay()
    {
        Time.timeScale = 1;
        _AllSoundPlay();
        //BoxingSoundManager.instance.AllSoundPlay();
        optionPopup.SetActive(false);

    }

    //게임 종료
    public void GameEnd()
    {
        Application.Quit();
    }

    //화면내리기
    public void WindowViewDown()
    {
        ShowWindow(GetActiveWindow(), 2);
    }

    public void HomeBtn()
    {
        SceneManager.LoadScene("Main");
    }

}
