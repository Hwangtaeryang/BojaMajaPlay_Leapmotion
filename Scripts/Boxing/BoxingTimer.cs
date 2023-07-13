using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class BoxingTimer : MonoBehaviour
{
    public static UnityAction RoundEnd = null;

    public float timeLeft;
    public int roundLength;
    public static float copyTime;

    public Slider timerSlider;
    public Image sliderHandle;

    int levelCount = 0;
    int levelMax1 = 2000, levelMax2 = 5000, levelMax3 = 8000, levelMax4 = 10000;


    private void Awake()
    {
        timeLeft = roundLength;
        timerSlider.value = timeLeft / roundLength;
    }


    void Start()
    {
        
    }

    //시작
    public void StartTimer()
    {
        timeLeft = roundLength;
        copyTime = timeLeft;    //카피
        StartCoroutine(Clock());
    }



    IEnumerator Clock()
    {
        RectTransform rectTran = sliderHandle.gameObject.GetComponent<RectTransform>();


        while (timeLeft > 0 )//&& !WindowDataManager.instance.GameEndScoreState())
        {
            
            timeLeft -= Time.deltaTime;
            copyTime = timeLeft;

            if (timeLeft <= 0)
            {
                timeLeft = 0;
            }

            if (timeLeft <= 30f && timeLeft >= 15f)
            {
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 75f);
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_1");
            } 
            else if(timeLeft < 15f && timeLeft >= 5f)
            {
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_2");

                if(timeLeft < 15f && timeLeft > 14.8f)
                    BoxingSoundManager.instance.IconImageChange();
            }
            else if(timeLeft < 5f && timeLeft >= 0)
            {
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_3");
                if (timeLeft < 5f && timeLeft > 4.8f)
                {
                    BoxingSoundManager.instance.Timer5Sound();
                    BoxingSoundManager.instance.IconImageChange();
                }
                    
            }


            if (BoxingDataManager.instance.score > 0 && BoxingDataManager.instance.score <= levelMax1 && levelCount == 0)
            {
                BoxingSoundManager.instance.LevelUpSound();
                levelCount++;
            }
            else if (BoxingDataManager.instance.score > levelMax1 && BoxingDataManager.instance.score <= levelMax2 && levelCount == 1)
            {
                BoxingSoundManager.instance.LevelUpSound();
                levelCount++;
            }
            else if (BoxingDataManager.instance.score > levelMax2 && BoxingDataManager.instance.score <= levelMax3 && levelCount == 2)
            {
                BoxingSoundManager.instance.LevelUpSound();
                levelCount++;
            }
            else if (BoxingDataManager.instance.score > levelMax3 && BoxingDataManager.instance.score <= levelMax4 && levelCount == 3)
            {
                BoxingSoundManager.instance.LevelUpSound();
                levelCount++;
            }
            else if (BoxingDataManager.instance.score > levelMax4 && levelCount == 4)
            {
                BoxingSoundManager.instance.LevelUpSound();
                levelCount++;
            }


            timerSlider.value = timeLeft / roundLength;

            //소수점 두번째 자리, 23.12을 23:12로 표시
            //timer.text = timeLeft.ToString("N2").Replace(".", ":");
            yield return new WaitForEndOfFrame();

            
                

            //yield return new WaitForSecondsRealtime(0.01f);
        }
        BoxingSoundManager.instance.Timer5SoundStop();


        if (RoundEnd != null)
            RoundEnd.Invoke();
    }

}
