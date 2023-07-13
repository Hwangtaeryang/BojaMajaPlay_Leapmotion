using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteGoStoneSpawn : MonoBehaviour
{
    //public Transform goStoneSpawnPos;

    public GameObject goStone;
    public int StoneAmount;
    public List<GameObject> goStonePool;
    private new BoxCollider collider;
    private bool is_OnCheckWhiteGoStone;

    public static WhiteGoStoneSpawn Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 리스트 객체 생성
        goStonePool = new List<GameObject>();
        collider = GetComponent<BoxCollider>();
        is_OnCheckWhiteGoStone = false;
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
            // 흰색 돌이 없다면
            if (goStonePool.Count == 0)
            {
                CreateGoStone();
            }

            yield return new WaitForSecondsRealtime(0.4f);
        }

        yield return null;
    }

    public void CreateGoStone()
    {
        // 검은돌이 존재한다면
        if (!is_OnCheckWhiteGoStone)
        {
            GameObject go;  // 바둑알
            Vector3 randPos;

            // 15개 정도
            for (int i = 0; i < StoneAmount; i++)
            {
                go = Instantiate(goStone);

                goStonePool.Add(go);

                go.transform.SetParent(this.transform);
                randPos = GetRandomPointInCollider();
                go.transform.position = randPos;
            }
        }
    }

    // 스폰위치 랜덤값 받기
    public Vector3 GetRandomPointInCollider()
    {
        Bounds bounds = collider.bounds;
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    public void OnRoundEnd()
    {
        foreach (var v in goStonePool)
        {
            Destroy(v);
        }
        goStonePool.Clear();
    }

}
