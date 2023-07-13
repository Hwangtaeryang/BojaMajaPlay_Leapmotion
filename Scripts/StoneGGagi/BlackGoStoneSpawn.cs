using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackGoStoneSpawn : MonoBehaviour
{
    public Transform goStoneSpawnPos;

    public GameObject goStone;
    private List<GameObject> goStonePool;
#pragma warning disable IDE0052 // 읽지 않은 private 멤버 제거
    private bool is_InCollider;
#pragma warning restore IDE0052 // 읽지 않은 private 멤버 제거

    // Start is called before the first frame update
    void Start()
    {
        //is_InCollider = false;
        // 리스트 객체 생성
        goStonePool = new List<GameObject>();
    }

    public void StartSpawner()
    {
        //Debug.Log("여긴되나??");
        StopAllCoroutines();
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (GoDataManager.instance.playTime.timeLeft > 0)
        {
            // 검은색 돌이 1. 테이블위에 존재하지 않으면 생성
            // 2. Detector 안에 있지않으면
            // 3. 필드박스안에 없다면
            if (!CheckOnGoStone.Instance.is_OntheTable && /*!CheckOnGoStone.Instance.is_InCollider &&*/ !FieldDetector.Instance.is_OnField)
            {
                CreateGoStone();
            }

            yield return new WaitForSecondsRealtime(1.2f);
        }

        yield return null;
    }

    public void CreateGoStone()
    {
        GameObject go;  // 바둑알

        go = Instantiate(goStone);

        goStonePool.Add(go);

        go.transform.SetParent(this.transform);
        go.transform.position = goStoneSpawnPos.position;
    }

    // 바둑알 존재하는지 체크 : true
    public void DetectingOnDistance()
    {
        is_InCollider = true;
    }

    // 바둑알 존재하는지 체크 : false
    public void DetectingOffDistance()
    {
        is_InCollider = false;
    }

    public void OnRoundEnd()
    {
        foreach(var stone in goStonePool)
        {
            Destroy(stone);
        }

        goStonePool.Clear();
    }

}
