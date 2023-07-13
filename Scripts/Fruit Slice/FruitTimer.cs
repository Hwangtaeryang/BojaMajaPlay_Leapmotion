using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class FruitTimer : MonoBehaviour
{
    public static UnityAction RoundEnd = null;

    public float timeLeft;
    public static float copyTime;
    
    public int roundLength;

    float timer_5second;  //5초타이머

    public Slider timerSlider;
    public Image sliderHandle;

    int levelCount = 0;
    int levelMax1 = 1000, levelMax2 = 2000, levelMax3 = 3000, levelMax4 = 4000;

    private void Awake()
    {

        timeLeft = roundLength;
        timerSlider.value = timeLeft / roundLength;
    }

    public void ReStartTimer()
    {
        timeLeft = roundLength;
        copyTime = timeLeft;    //카피
        timer_5second = timeLeft;    //복사
    }

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


        StartCoroutine(Timer5Second()); //5초 사운드
        while (timeLeft > 0 )//&& !FruitDataManager.Instance.WonRound())
        {
            timeLeft -= Time.deltaTime;
            copyTime = timeLeft;

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
                    FruitSoundManager.Instance.IconImageChange();
            }
            else if (timeLeft < 5f && timeLeft >= 0)
            {
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_3");
                if (timeLeft < 5f && timeLeft > 4.8f)
                {
                    FruitSoundManager.Instance.IconImageChange();
                    FruitSoundManager.Instance.Timer5Sound();
                }
                    
            }


            if (FruitDataManager.Instance.score > 0 && FruitDataManager.Instance.score <= levelMax1 && levelCount == 0)
            {
                FruitSoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (FruitDataManager.Instance.score > levelMax1 && FruitDataManager.Instance.score <= levelMax2 && levelCount == 1)
            {
                FruitSoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (FruitDataManager.Instance.score > levelMax2 && FruitDataManager.Instance.score <= levelMax3 && levelCount == 2)
            {
                FruitSoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (FruitDataManager.Instance.score > levelMax3 && FruitDataManager.Instance.score <= levelMax4 && levelCount == 3)
            {
                FruitSoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (FruitDataManager.Instance.score > levelMax4 && levelCount == 4)
            {
                FruitSoundManager.Instance.LevelUpSound();
                levelCount++;
            }


            timerSlider.value = timeLeft / roundLength;



            //소수점 두번째 자리, 23.12을 23:12로 표시
            //timer.text = timeLeft.ToString("N2").Replace(".", ":");
            yield return new WaitForEndOfFrame();
        }

        FruitSoundManager.Instance.Timer5SoundStop();


        if (RoundEnd != null)
            RoundEnd.Invoke();
    }

    //5초 남았을 때 사운드
    IEnumerator Timer5Second()
    {
        while (timer_5second > 0 )//&& !FruitDataManager.Instance.WonRound() )
        {
            timer_5second--;

            if (timer_5second <= 0)
                timer_5second = 0;

            if (timer_5second == 5f)
                FruitSoundManager.Instance.Timer5Sound();

            yield return new WaitForSecondsRealtime(1f);

        }

    }
}
