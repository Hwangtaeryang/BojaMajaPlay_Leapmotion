using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoopWindowCtrl : MonoBehaviour
{
    public Poop poop;
    Image poopwindowImg;

    int touchCount = 0;
    int poopwindowImgVer;
    int poopwindowImgNum;

    public WindowCtrl windowCtrl;
    public GameObject window;
    public Transform pos;   //파티클 생성 위치
    public GameObject partical;   //파티클 

    GameObject partical_copy;
    Collider windowCollider;

    void Start()
    {
        windowCollider = window.GetComponent<Collider>();

        touchCount = 0;
        poopwindowImg = this.gameObject.GetComponent<Image>();
        poopwindowImgVer = Random.Range(1, 4);
        poopwindowImgNum = Random.Range(1, 4);
        poopwindowImg.sprite = Resources.Load<Sprite>("Window/새똥" + poopwindowImgVer + "_" + poopwindowImgNum);

    }

    
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (WindowGameManager.instance.gamePlay)
        {
            if (other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
            {
                touchCount++;

                if (touchCount <= 15)
                {
                    WindowDataManager.instance.SetScore(100);    //점수올리기
                    if (touchCount % 2 == 0)
                    {
                         WindowSoundManager.instance.BirdWindowSound();
                    }
                }
                    
                                                                 //Debug.Log("count" + touchCount);
                WindowChange(touchCount, poopwindowImgNum);
            }
        }
    }

    void WindowChange(int count, int imgNum)
    {
        if (imgNum == 1)
        {
            if (count == 5)
            {
                poopwindowImg.sprite = Resources.Load<Sprite>("Window/새똥" + poopwindowImgVer + "_2");
            }
            else if(count == 10)
            {
                poopwindowImg.sprite = Resources.Load<Sprite>("Window/새똥" + poopwindowImgVer + "_3");
            }
            else if (count == 15)
            {
                poopwindowImg.sprite = Resources.Load<Sprite>("Window/4");
                StartCoroutine(ClearPartical());
                WindowSoundManager.instance.WindowCleanSound();
                //windowCtrl.windowTouch = true;
                windowCollider.enabled = true;  //윈도우창 콜라이더 활성화
                this.gameObject.SetActive(false);   //자신 비활성화
            }
        }
        else if(imgNum == 2)
        {
            if (count == 5)
            {
                poopwindowImg.sprite = Resources.Load<Sprite>("Window/새똥" + poopwindowImgVer + "_3");
            }
            else if (count == 10)
            {
                poopwindowImg.sprite = Resources.Load<Sprite>("Window/4");
                StartCoroutine(ClearPartical());
                WindowSoundManager.instance.WindowCleanSound();
                //windowCtrl.windowTouch = true;
                windowCollider.enabled = true;
                this.gameObject.SetActive(false);
            }
        }
        else if(imgNum == 3)
        {
            if (count == 5)
            {
                poopwindowImg.sprite = Resources.Load<Sprite>("Window/4");
                StartCoroutine(ClearPartical());
                WindowSoundManager.instance.WindowCleanSound();
                //windowCtrl.windowTouch = true;
                windowCollider.enabled = true;
                this.gameObject.SetActive(false);

                
            }
        }
    }

    IEnumerator ClearPartical()
    {
        partical_copy = Instantiate(partical);
        partical_copy.transform.position = pos.position;

        yield return new WaitForSeconds(1f);
    }

    public void ReSetting()
    {
        touchCount = 0;
        poopwindowImg = this.gameObject.GetComponent<Image>();
        poopwindowImgVer = Random.Range(1, 4);
        poopwindowImgNum = Random.Range(1, 4);
        poopwindowImg.sprite = Resources.Load<Sprite>("Window/새똥" + poopwindowImgVer + "_" + poopwindowImgNum);
    }
}
