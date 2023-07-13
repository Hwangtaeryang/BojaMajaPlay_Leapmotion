using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPartical : MonoBehaviour
{
    public GameObject[] hitPartical;
    public Transform hitPos;
    GameObject hitPartical_copy;


    void Start()
    {
        
    }

    
    void Update()
    {
        if(BoxingGameManager.instance.gamePlay)
        {
            if (TouchBag.instance.hitLeftState && this.gameObject.name == "boxing glove_left")
            {
                BoxingSoundManager.instance.LeftPunchSound();   // 펀치사운드

                TouchBag.instance.hitLeftState = false; //노펀치로 변경
                StartCoroutine(HitPartical());  //펀치 파티클
                BoxingDataManager.instance.SetScore(50);    //점수
                
            }

            if (TouchBag.instance.hitRightState && this.gameObject.name == "boxing glove_right")
            {
                BoxingSoundManager.instance.RightPunchSound();

                TouchBag.instance.hitRightState = false;
                StartCoroutine(HitPartical());
                BoxingDataManager.instance.SetScore(50);
                BoxingSoundManager.instance.RightPunchSound();
            }
        }
        
    }

    IEnumerator HitPartical()
    {
        int num = Random.Range(0, hitPartical.Length);

        hitPartical_copy = Instantiate(hitPartical[num]);
        hitPartical_copy.transform.position = hitPos.position;

        yield return new WaitForSeconds(2f);

        Destroy(hitPartical_copy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PunchZone"))
        {
            if (other.gameObject.name == "PunchZone")
            {
                BoxingSoundManager.instance.RightSwingSound();
            }
        }
    }
}
