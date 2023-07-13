using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFingerTouch : MonoBehaviour
{
    public static LeftFingerTouch instance { get; private set; }

    public bool mainBtnOnClick; //메인버튼(랜덤화면 돌아가는)
    public bool startBtnOnClick;    //게임 시작 버튼 
    public bool homeBtnOnClick; //홈버튼
    public bool closeBtnOnClick;    //게임종료버튼
    public bool miniBtnOnClick; //최소화버튼


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    //void Start()
    //{
        
    //}

    
    //void Update()
    //{
        
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MainBtn"))
        {
            mainBtnOnClick = true;
            Debug.Log("클릭");
        }
        if(other.CompareTag("StartBtn"))
        {
            startBtnOnClick = true;
        }

        if(other.CompareTag("HomeBtn"))
        {
            Debug.Log("홈");
            homeBtnOnClick = true;
        }

        if(other.CompareTag("CloseBtn"))
        {
            Debug.Log("게임종료");
            closeBtnOnClick = true;
        }

        if (other.CompareTag("MiniBtn"))
        {
            Debug.Log("최소화");
            miniBtnOnClick = true;
        }
    }
}
