using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UISceneOnOff : MonoBehaviour
{
    Image thisImage;

    float value = 0;
    float _time = 0f;
    public float time = 5f;

    // 이벤트 트리거
    public UnityEvent onMyOnOff = null;


    void OnEnable()
    {
        time = 5f;  //초기화
    }


    void Start()
    {
        thisImage = GetComponent<Image>();
    }


    void Update()
    {
        CoolTime();
    }

    void CoolTime()
    {
        time -= Time.deltaTime;

        if (time < _time)
        {
            time = _time;
        }

        value = time / 5f;
        thisImage.fillAmount = Mathf.LerpAngle(0f, 1f, value);

        if (thisImage.fillAmount == 0f)
        {
            onMyOnOff.Invoke(); // GameManager.GamePlayStart()함수에서 실행.  오브젝트 비활성화 시켜준다
        }
    }
}
