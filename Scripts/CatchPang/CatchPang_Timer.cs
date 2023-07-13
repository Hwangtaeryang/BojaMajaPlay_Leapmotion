using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CatchPang_Timer : MonoBehaviour
{
    public static UnityAction RoundEnd = null;
    public static bool isPlaying;
    public int roundLength; // 30sec

    public float timeLeft;    // 0
    //public float timeLeft2; // 0
    //private Text timer;     // 텍스트 표시
    private string secToString;


    public Slider timerSlider;
    public Image sliderHandle;

    int levelCount = 0;
    int levelMax1 = 9000, levelMax2 = 25000, levelMax3 = 40000, levelMax4 = 55000;

    public static CatchPang_Timer Instance { get; private set; }
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
        //timeLeft2 = roundLength;

        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;

            //timer.text = SecToString(timeLeft);

            if (timeLeft <= 30f && timeLeft >= 15f)
            {
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 75f);
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_1");
            }
            else if (timeLeft < 15f && timeLeft >= 5f)
            {
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_2");

                if (timeLeft < 15f && timeLeft > 14.8f)
                    CatchPang_SoundManager.Instance.IconImageChange();
            }
            else if (timeLeft < 5f && timeLeft >= 0)
            {
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_3");
                if (timeLeft < 5f && timeLeft > 4.8f)
                    CatchPang_SoundManager.Instance.IconImageChange();
            }


            if (CatchPang_DataManager.Instance.score > 0 && CatchPang_DataManager.Instance.score <= levelMax1 && levelCount == 0)
            {
                CatchPang_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (CatchPang_DataManager.Instance.score > levelMax1 && CatchPang_DataManager.Instance.score <= levelMax2 && levelCount == 1)
            {
                CatchPang_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (CatchPang_DataManager.Instance.score > levelMax2 && CatchPang_DataManager.Instance.score <= levelMax3 && levelCount == 2)
            {
                CatchPang_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (CatchPang_DataManager.Instance.score > levelMax3 && CatchPang_DataManager.Instance.score <= levelMax4 && levelCount == 3)
            {
                CatchPang_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (CatchPang_DataManager.Instance.score > levelMax4 && levelCount == 4)
            {
                CatchPang_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }


            timerSlider.value = timeLeft / roundLength;

            if (timeLeft < 5f && timeLeft > 4.8f)
            {
                CatchPang_SoundManager.Instance.sfxLimitFiveSec();
            }


            yield return new WaitForEndOfFrame();
        }

        CatchPang_SoundManager.Instance.StopSelectedSfx("Limit5sec");

        //timer.text = SecToString(30);

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

        //Debug.Log("changeText : " + changeText);

        return changeText;
    }


}
