using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageOver : MonoBehaviour
{
    public Vector2[] page;  

    public bool check = true;


    private void Update()
    {
        
    }

    //화면 이동 함수
    public void Page_Change(int num)
    {
        if (check)
        {
            check = false;
            StartCoroutine("Page_Change_", num);
        }
    }

    IEnumerator Page_Change_(int num)
    {
        while (check == false)
        {
            //각페이지 위치로 이동
            this.transform.localPosition = new Vector2(page[num].x, this.transform.localPosition.y);
            //Debug.Log(page[num].x);
            check = true;

            //부드럽게 넘어가는 화면(옆으로 스르르 넘어가는 화면)
            //transform.localPosition = Vector2.Lerp(this.transform.localPosition, page[num], Time.deltaTime * 7);

            //if (Mathf.Abs(this.transform.localPosition.x - page[num].x) <= 2)
            //{
            //    Debug.Log("???");
            //    this.transform.localPosition = new Vector2(page[num].x, this.transform.localPosition.y);
            //    check = true;
            //}

            yield return null;
        }
    }
}
