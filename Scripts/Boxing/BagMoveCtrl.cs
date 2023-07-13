using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagMoveCtrl : MonoBehaviour
{
    public static BagMoveCtrl instance { get; private set; }


    int moveDirection = 4;  // 1.5:왼쪽, 2.6:오른쪽, 3.7:앞, 4.8:뒤
    float bagSpeed = 4f;
    int randomNum;
    float leftMax = -10.2f, rightMax = -8.2f, backMax = 6.1f, forwardMax = 5.3f;

    


    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
       // Debug.Log(transform.localPosition.x);
    }

    public void BagDirection()
    {
        StartCoroutine(_BagDirection());
    }

    public void StopBag()
    {
        StopAllCoroutines();
    }

    public IEnumerator _BagDirection()
    {
        float time = 1.5f;
        float bagMove;

        randomNum = Random.Range(1, moveDirection + 1);

        while (time > 0f)
        {
            time -= Time.deltaTime;


            if (randomNum == 1)  //왼쪽
            {
                bagMove = leftMax - transform.localPosition.x;
                if (transform.localPosition.x  >= leftMax)
                    transform.Translate(Vector3.left * bagSpeed * Time.deltaTime);
                else if (transform.localPosition.x < leftMax)
                    transform.localPosition = new Vector3(leftMax, transform.localPosition.y, transform.localPosition.z);
            }
            else if (randomNum == 2 ) //오른쪽
            {
                bagMove = rightMax - transform.localPosition.x;
                if (transform.localPosition.x  <= rightMax )
                    transform.Translate(Vector3.right * bagSpeed *Time.deltaTime);
                else if (transform.localPosition.x > rightMax)
                    transform.localPosition = new Vector3(rightMax, transform.localPosition.y, transform.localPosition.z);
            }
            else if (randomNum == 3 ) //앞
            {
                bagMove = forwardMax - transform.localPosition.z;
                if (transform.localPosition.z  >= forwardMax)
                    transform.Translate(Vector3.up * bagSpeed * Time.deltaTime);
                else if (transform.localPosition.z < forwardMax)
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, forwardMax);
            }
            else if (randomNum == 4) //뒤
            {
                bagMove = backMax - transform.localPosition.z;
                if (transform.localPosition.z <= backMax)
                    transform.Translate(Vector3.down * bagSpeed * Time.deltaTime);
                else if (transform.localPosition.z > backMax)
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, backMax);
            }


            yield return new WaitForEndOfFrame();
        }

       // StopAllCoroutines();
        StartCoroutine(_BagDirection());
    }


}
