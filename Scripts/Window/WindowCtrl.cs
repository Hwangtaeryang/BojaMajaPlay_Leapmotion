using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowCtrl : MonoBehaviour
{
    public Poop poop;   //똥스크립트
    public GameObject poopWindow;   //똥창문   
    public Transform pos;
    public GameObject[] partical;
    public GameObject poopPartical;

    GameObject partical_copy;
    PoopWindowCtrl poopwindowCtrl;
    Collider thisCollision;
    Image windowImg;


    public bool windowTouch;
    int touchCount = 0;
    int windowImgVer;
    int windowImgNum;
    
    void Start()
    {
        thisCollision = GetComponent<Collider>();
        poopwindowCtrl = poopWindow.GetComponent<PoopWindowCtrl>();
        poopWindow.SetActive(false);

        windowImg = this.gameObject.GetComponent<Image>();
        windowImgVer = Random.Range(1, 9);
        windowImgNum = Random.Range(1, 4);
        windowImg.sprite = Resources.Load<Sprite>("Window/얼룩" + windowImgVer + "_"+ windowImgNum);
    }

    
    void Update()
    {
        BirdPoopWindowMake();

        if (windowTouch)
        {
            windowTouch = false;
            thisCollision.enabled = true;
            //poopWindow.SetActive(false); //창문 활성화하기
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if(WindowGameManager.instance.gamePlay)
        {
            if (other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
            {
                touchCount++;

                if (touchCount <= 15)
                {
                    WindowDataManager.instance.SetScore(100);    //점수올리기
                    if(touchCount % 2 == 0)
                    {
                        WindowSoundManager.instance.WindowSound();
                    }
                }
                    
                                                                 //Debug.Log("count" + touchCount);
                WindowChange(touchCount, windowImgNum);
            }
        }
        
    }


    void WindowChange(int count, int imgNum)
    {
        int num = Random.Range(0, partical.Length);
        if(imgNum == 1)
        {
            if (count == 5)
            {
                windowImg.sprite = Resources.Load<Sprite>("Window/얼룩"+ windowImgVer +"_2");
            }
            else if (count == 10)
            {
                windowImg.sprite = Resources.Load<Sprite>("Window/얼룩" + windowImgVer + "_3");
            }
            else if (count == 15)
            {
                windowImg.sprite = Resources.Load<Sprite>("Window/4");
                WindowSoundManager.instance.WindowCleanSound();

                //파티클 생성 
                StartCoroutine(ClearPartical(num));
            }
        }
        else if(imgNum == 2)
        {
            if (count == 5)
            {
                windowImg.sprite = Resources.Load<Sprite>("Window/얼룩" + windowImgVer + "_3");
            }
            else if (count == 10)
            {
                windowImg.sprite = Resources.Load<Sprite>("Window/4");

                //파티클 생성 
                StartCoroutine(ClearPartical(num));
            }
        }
        else if(imgNum == 3)
        {
            if (count == 5)
            {
                windowImg.sprite = Resources.Load<Sprite>("Window/4");
                WindowSoundManager.instance.WindowCleanSound();

                //파티클 생성 
                StartCoroutine(ClearPartical(num));
            }
        }
        
    }

    IEnumerator ClearPartical(int num)
    {
        partical_copy = Instantiate(partical[num]);
        partical_copy.transform.position = pos.position;

        yield return new WaitForSeconds(1f);
    }

    //새똥 이미지 입히는 함수
    void BirdPoopWindowMake()
    {
        //새똥이 창문에 부딪혔을 때
        if(Poop.windowName == gameObject.name)
        {
            thisCollision.enabled = false;  //창문 콜라이더 끄기
            poopWindow.SetActive(true); //창문 활성화하기
            poopwindowCtrl.ReSetting(); //창문 무늬 재생성
            StartCoroutine(PoopClearPartical());
        }
    }

    IEnumerator PoopClearPartical()
    {
        partical_copy = Instantiate(poopPartical);
        partical_copy.transform.position = pos.position;

        yield return new WaitForSeconds(1f);
    }
}
