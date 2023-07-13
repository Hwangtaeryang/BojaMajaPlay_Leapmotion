using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

public class GrillingMeat_Timer : MonoBehaviour
{
    public static UnityAction RoundEnd = null;
    public static bool isPlaying;
    public int roundLength; // 30sec
    public float timeLeft;    // 0

    private string secToString;


    public Slider timerSlider;
    public Image sliderHandle;

    int levelCount = 0; //레벨업을 위한 카운트
    int levelMax1 = 3000, levelMax2 = 9000, levelMax3 = 15000, levelMax4 = 20000;

    public static GrillingMeat_Timer Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;

        timeLeft = roundLength;
        timerSlider.value = timeLeft / roundLength;
    }

    public void StartTimer()
    {
        // 30초 넣고
        timeLeft = roundLength;


        StartCoroutine(_Clock());
    }

    public IEnumerator _Clock()
    {
        RectTransform rectTran = sliderHandle.gameObject.GetComponent<RectTransform>();

        isPlaying = true;
        bool gameOver = true;
        timeLeft = roundLength;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 30f && timeLeft >= 15f)
            {
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 75f);
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_1");
            }
            else if (timeLeft < 15f && timeLeft >= 5f)
            {
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_2");

                if (timeLeft < 15f && timeLeft > 14.8f)
                    GrillingMeat_SoundManager.Instance.IconImageChange();
            }
            else if (timeLeft < 5f && timeLeft >= 0)
            {
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_3");
                if (timeLeft < 5f && timeLeft > 4.8f)
                    GrillingMeat_SoundManager.Instance.IconImageChange();
            }


            if (GrillingMeat_DataManager.Instance.score > 0 && GrillingMeat_DataManager.Instance.score <= levelMax1 && levelCount == 0)
            {
                GrillingMeat_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (GrillingMeat_DataManager.Instance.score > levelMax1 && GrillingMeat_DataManager.Instance.score <= levelMax2 && levelCount == 1)
            {
                GrillingMeat_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (GrillingMeat_DataManager.Instance.score > levelMax2 && GrillingMeat_DataManager.Instance.score <= levelMax3 && levelCount == 2)
            {
                GrillingMeat_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (GrillingMeat_DataManager.Instance.score > levelMax3 && GrillingMeat_DataManager.Instance.score <= levelMax4 && levelCount == 3)
            {
                GrillingMeat_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (GrillingMeat_DataManager.Instance.score > levelMax4 && levelCount == 4)
            {
                GrillingMeat_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }


            timerSlider.value = timeLeft / roundLength;




            if (timeLeft < 5f && timeLeft > 4.8f)
            {
                GrillingMeat_SoundManager.Instance.sfxLimitFiveSec();
            }


            yield return new WaitForEndOfFrame();
        }

        GrillingMeat_SoundManager.Instance.StopSelectedSfx("Limit5sec");


        isPlaying = false;

        if (gameOver)
        {
            RoundEnd.Invoke();
        }
    }

    private string SecToString(float sec)
    {
        // 초를 받아서 스트링으로        
        // 콜론 앞에 있는 부분은 Format에 들어갈 매개변수의 순서
        string changeText;

        secToString = sec.ToString("N2");
        //changeText = secToString.Replace(".", "'") + "\"";
        changeText = secToString.Replace(".", ":");

        return changeText;
    }


}