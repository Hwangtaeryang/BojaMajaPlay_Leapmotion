using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeapMotionOptionPopup : MonoBehaviour
{
    //-----------------립모션 버전 ----------------
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

    //활성화된 윈도우-함수를 호출한 쓰레드와 연동된 녀석의 핸들을 받는다.
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();



    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "MainCubeBtn" || gameObject.name == "HomeBtn")
        {
            if (other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
            {
                MainShow();
            }
        }

        else if (gameObject.name == "MiniCubeBtn")
        {
            if (other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
            {
                MiniWindow();
            }
        }

        else if (gameObject.name == "GameEndCubeBtn")
        {
            if (other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
            {
                Debug.Log("종료");
                GameEnd();
            }
        }

    }

    public void MainShow()
    {
        SceneManager.LoadScene("Main");
    }

    public void MiniWindow()
    {
        ShowWindow(GetActiveWindow(), 2);
    }

    public void GameEnd()
    {
        Application.Quit();
    }
}
