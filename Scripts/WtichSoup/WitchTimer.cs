using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class WitchTimer : MonoBehaviour
{
    public static UnityAction RoundEnd = null;

    public float timeLeft;
    public static float copyTime;

    public int roundLength;

    public Slider timerSlider;
    public Image sliderHandle;

    int levelCount = 0;
    int levelMax1 = 5000, levelMax2 = 15000, levelMax3 = 30000, levelMax4 = 40000;




    float timer_5second;  //5초타이머

    private void Awake()
    {
        timeLeft = roundLength;
        timerSlider.value = timeLeft / roundLength;
    }

    //재시작
    public void ReStartTimer()
    {
        
        copyTime = timeLeft;    //카피
        timer_5second = timeLeft;    //복사
    }

    //시작
    public void StartTimer()
    {
        timeLeft = roundLength;
        copyTime = timeLeft;    //카피
        timer_5second = timeLeft;    //복사
        StartCoroutine(Clock());
    }

    IEnumerator Clock()
    {
        RectTransform rectTran = sliderHandle.gameObject.GetComponent<RectTransform>();

        while (timeLeft > 0)// && !WitchDataManager.instance.GameEndScoreState())
        {
            timeLeft -= Time.deltaTime;
            copyTime = timeLeft;

            //소수점 두번째 자리, 23.12을 23:12로 표시
            //timer.text = timeLeft.ToString("N2").Replace(".", ":");

            //마녀 웃음소리
            if ((int)timeLeft % 8 == 0)
            {
                WitchSoundManager.instance.WtichSound();
            }

            if (timeLeft <= 0)
                timeLeft = 0;


            if (timeLeft <= 30f && timeLeft >= 15f)
            {
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 75f);
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_1");
            }
            else if (timeLeft < 15f && timeLeft >= 5f)
            {
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_2");

                if (timeLeft < 15f && timeLeft > 14.8f)
                    WitchSoundManager.instance.IconImageChange();
            }
            else if (timeLeft < 5f && timeLeft >= 0)
            {
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_3");
                if (timeLeft < 5f && timeLeft > 4.8f)
                {
                    WitchSoundManager.instance.IconImageChange();
                    WitchSoundManager.instance.Timer5Sound();
                }

            }


            if (WitchDataManager.instance.score > 0 && WitchDataManager.instance.score <= levelMax1 && levelCount == 0)
            {
                WitchSoundManager.instance.LevelUpSound();
                levelCount++;
            }
            else if (WitchDataManager.instance.score > levelMax1 && WitchDataManager.instance.score <= levelMax2 && levelCount == 1)
            {
                WitchSoundManager.instance.LevelUpSound();
                levelCount++;
            }
            else if (WitchDataManager.instance.score > levelMax2 && WitchDataManager.instance.score <= levelMax3 && levelCount == 2)
            {
                WitchSoundManager.instance.LevelUpSound();
                levelCount++;
            }
            else if (WitchDataManager.instance.score > levelMax3 && WitchDataManager.instance.score <= levelMax4 && levelCount == 3)
            {
                WitchSoundManager.instance.LevelUpSound();
                levelCount++;
            }
            else if (WitchDataManager.instance.score > levelMax4 && levelCount == 4)
            {
                WitchSoundManager.instance.LevelUpSound();
                levelCount++;
            }


            timerSlider.value = timeLeft / roundLength;





            yield return new WaitForEndOfFrame();

            

            //yield return new WaitForSecondsRealtime(0.01f);
        }


        if (RoundEnd != null)
            RoundEnd.Invoke();
    }


}
