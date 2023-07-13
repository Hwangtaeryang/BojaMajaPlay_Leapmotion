using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IceTiger_Timer : MonoBehaviour
{
    public static UnityAction RoundEnd = null;
    public static bool isPlaying;
    public int roundLength; // 30초

    public float timeLeft;  // 남는시간 계산
    private string secToString;

    public Slider timerSlider;
    public Image sliderHandle;

    int levelCount = 0; //레벨업을 위한 카운트
    int levelMax1 = 6000, levelMax2 = 18000, levelMax3 = 28000, levelMax4 = 40000;



    public static IceTiger_Timer Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }

    public void StartTimer()
    {

        timeLeft = roundLength; // 30초 초기화
        timerSlider.value = timeLeft / roundLength;

        StartCoroutine(_Clock());    // 코루틴 시작
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
                    IceTiger_SoundManager.Instance.IconImageChange();
            }
            else if (timeLeft < 5f && timeLeft >= 0)
            {
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_3");
                if (timeLeft < 5f && timeLeft > 4.8f)
                {
                    IceTiger_SoundManager.Instance.IconImageChange();
                    IceTiger_SoundManager.Instance.Timer5Sound();
                }
                    
            }


            if (IceTiger_DataManager.Instance.score > 0 && IceTiger_DataManager.Instance.score <= levelMax1 && levelCount == 0)
            {
                IceTiger_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (IceTiger_DataManager.Instance.score > levelMax1 && IceTiger_DataManager.Instance.score <= levelMax2 && levelCount == 1)
            {
                IceTiger_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (IceTiger_DataManager.Instance.score > levelMax2 && IceTiger_DataManager.Instance.score <= levelMax3 && levelCount == 2)
            {
                IceTiger_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (IceTiger_DataManager.Instance.score > levelMax3 && IceTiger_DataManager.Instance.score <= levelMax4 && levelCount == 3)
            {
                IceTiger_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (IceTiger_DataManager.Instance.score > levelMax4 && levelCount == 4)
            {
                IceTiger_SoundManager.Instance.LevelUpSound();
                levelCount++;
            }


            timerSlider.value = timeLeft / roundLength;



            yield return new WaitForEndOfFrame();

        }
        IceTiger_SoundManager.Instance.Timer5SoundStop();




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
