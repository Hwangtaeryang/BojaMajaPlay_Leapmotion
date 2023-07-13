using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class IceTiger_Transitioner : MonoBehaviour
{
    private Image image;
    private float b;
    private float perc = 0f;
    private string parentName;
    private bool b_isEndProc;
    public float time = 5.1f;
    // 이벤트 트리거
    public UnityEvent onTransition = null;

    void OnEnable()
    {
        b = 5.1f;
        b_isEndProc = true;
    }

    void Start()
    {
        image = GetComponent<Image>();
        // 부모 오브젝트 이름 가져옴
        parentName = transform.parent.name;

    }

    void Update()
    {
        if (b_isEndProc)
        {
            b_isEndProc = false;
            // 껐다 켰다 할 때 계속 나와야함
            IceTiger_SoundManager.Instance.bgmPlayerVolumeControl(0f);
            // 현재 부모에 따라 다른 효과음 : 성공/실패
            IceTiger_SoundManager.Instance.bgmAfterGameEnd(parentName);
        }

        b -= Time.deltaTime;
        //if (b > time)
        //{
        //    b = time;
        //}

        perc = b / time;
        image.fillAmount = Mathf.LerpAngle(0f, 1f, perc);

        if (image.fillAmount == 0f)
        {
            IceTiger_SoundManager.Instance.bgmPlayerVolumeControl(1f);
            IceTiger_SoundManager.Instance.StopSfx();
            // fillAmount = 1 이면 (성공한 스크립트 일때) 이벤트를 한번 더 부름
            onTransition.Invoke();
            // 이벤트를 부르면
            // AppManager.OnRoundStart 함수 실행 , 현재 나와있는 Success Screen > GameObject.SetActive(False); 시킴
        }
    }
}
