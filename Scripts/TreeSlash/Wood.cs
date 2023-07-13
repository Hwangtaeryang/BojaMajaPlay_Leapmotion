using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Wood : MonoBehaviour
{
    public GameObject woodObj;
    public GameObject brokenWood;
    //private bool gamePlayShoot = true;
    private static int Index = 1;
    // Start is called before the first frame update
    void Start()
    {
        Index = 0;
    }

    private void Update()
    {
        if (TreeSlashGameManager.instance.gamePlay)
        {
            if (RightWoodenPillar.rightWoodmesheIndex == 3 && LeftWoodenPillar.leftWoodmesheIndex == 3 && this.gameObject.name == "Wood"+ Index)
            {
                RightWoodenPillar.rightWoodmesheIndex = 0;
                LeftWoodenPillar.leftWoodmesheIndex = 0;
                Index++;
                woodObj.SetActive(false);
                brokenWood.SetActive(true);

                // 점수
                TreeSlashDataManager.instance.AddScore(500);
                // 점수 사운드
                TreeSlashSoundManager.Instance.PlaySE("GetScore");
                // 파티클/사운드 : 나무 넘어가는소리, 부서질때 나오는 파편
                TreeSlashSoundManager.Instance.PlaySE("FelledTree");

                StartCoroutine(_Destroy());
            }
        }
    }

    IEnumerator _Destroy()
    {
        WaitForSeconds ws = new WaitForSeconds(0.8f);

        yield return ws;

        //Destroy(gameObject);
        GameObject parentObj = transform.parent.gameObject;
        parentObj.transform.GetChild(1).gameObject.SetActive(false);
        parentObj.SetActive(false);

        yield return null;
    }

    public void WoodStart()
    {
        StopAllCoroutines();
        StartCoroutine(WoodState());
    }

    IEnumerator WoodState()
    {
        WaitForSeconds ws = new WaitForSeconds(0.5f);

        while (TreeSlashDataManager.instance.playTime.timeLeft > 0f)
        {
            // 체크
            if (RightWoodenPillar.rightWoodmesheIndex == 3 && LeftWoodenPillar.leftWoodmesheIndex == 3)
            {
                yield return ws;

                woodObj.SetActive(false);
                brokenWood.SetActive(true);
            }
        }
        //Debug.Log("TreeSlashGameManager.instance.gamePlay : " + TreeSlashGameManager.instance.gamePlay);
        //Debug.Log("TreeSlashDataManager.instance.playTime.timeLeft : " + TreeSlashDataManager.instance.playTime.timeLeft);


        //yield return null;
    }

    public void WoodEnd()
    {
        StopAllCoroutines();
        //gamePlayShoot = true;
    }
}
