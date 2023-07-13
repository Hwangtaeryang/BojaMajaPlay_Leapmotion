using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class TreeSlashTimer : MonoBehaviour
{
    public static UnityAction RoundEnd = null;

    public float timeLeft;
    public int roundLength;
    public static float copyTime;

    public Slider timerSlider;
    public Image sliderHandle;

    int levelCount = 0;
    int levelMax1 = 1000, levelMax2 = 2500, levelMax3 = 4000, levelMax4 = 5000;

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
                    TreeSlashSoundManager.Instance.IconImageChange();
            }
            else if (timeLeft < 5f && timeLeft >= 0)
            {
                rectTran.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
                sliderHandle.sprite = Resources.Load<Sprite>("TimerIcon_3");
                if (timeLeft < 5f && timeLeft > 4.8f)
                {
                    TreeSlashSoundManager.Instance.IconImageChange();
                    TreeSlashSoundManager.Instance.sfxLimitFiveSec();
                }

            }


            if (TreeSlashDataManager.instance.score > 0 && TreeSlashDataManager.instance.score <= levelMax1 && levelCount == 0)
            {
                TreeSlashSoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (TreeSlashDataManager.instance.score > levelMax1 && TreeSlashDataManager.instance.score <= levelMax2 && levelCount == 1)
            {
                TreeSlashSoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (TreeSlashDataManager.instance.score > levelMax2 && TreeSlashDataManager.instance.score <= levelMax3 && levelCount == 2)
            {
                TreeSlashSoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (TreeSlashDataManager.instance.score > levelMax3 && TreeSlashDataManager.instance.score <= levelMax4 && levelCount == 3)
            {
                TreeSlashSoundManager.Instance.LevelUpSound();
                levelCount++;
            }
            else if (TreeSlashDataManager.instance.score > levelMax4 && levelCount == 4)
            {
                TreeSlashSoundManager.Instance.LevelUpSound();
                levelCount++;
            }


            timerSlider.value = timeLeft / roundLength;




            //소수점 두번째 자리, 23.12을 23:12로 표시
            yield return new WaitForEndOfFrame();

            

            //yield return new WaitForSecondsRealtime(0.01f);
        }

        TreeSlashSoundManager.Instance.StopSelectedSfx("타이머");

        if (RoundEnd != null)
            RoundEnd.Invoke();
    }

  
}
