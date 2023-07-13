using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CatchPang_Transitioner : MonoBehaviour
{
    private Image image;
    private float b;
    private float perc = 0f;
    private string parentName;
    private bool b_isEndProc;

    public float time = 5.2f;
    // 이벤트 트리거
    public UnityEvent onTransition = null;

    void OnEnable()
    {
        b = 5.2f;
        b_isEndProc = true;
    }

    void Start()
    {
        image = GetComponent<Image>();
        // 부모 오브젝트 이름 가져옴
        parentName = transform.parent.name;

        //CatchPang_SoundManager.Instance.bgmPlayerVolumeControll(0.4f);
        ////현재 부모에 따라 다른 효과음: 성공 / 실패
        //CatchPang_SoundManager.Instance.bgmAfterGameEnd(parentName);
    }

    void Update()
    {
        if (b_isEndProc)
        {
            //Debug.Log("Transition!!");
            b_isEndProc = false;
            // 브금 소리 줄임
            CatchPang_SoundManager.Instance.bgmPlayerVolumeControll(0f);
            // 현재 부모에 따라 다른 효과음: 성공 / 실패
            CatchPang_SoundManager.Instance.bgmAfterGameEnd(parentName);
        }

        b -= Time.deltaTime;

        perc = b / time;
        image.fillAmount = Mathf.LerpAngle(0f, 1f, perc);

        if (image.fillAmount == 0f)
        {
            CatchPang_SoundManager.Instance.bgmPlayerVolumeControll(1f);
            CatchPang_SoundManager.Instance.StopSfx();
            // fillAmount = 1 이면 (성공한 스크립트 일때) 이벤트를 한번 더 부름
            onTransition.Invoke();
            // 이벤트를 부르면
            // AppManager.OnRoundStart 함수 실행 , 현재 나와있는 Success Screen > GameObject.SetActive(False); 시킴
        }
    }
}
