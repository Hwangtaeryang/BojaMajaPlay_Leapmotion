using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawn : MonoBehaviour
{
    // x 좌표 랜덤, y, z 는 고정
    public Transform onePoint;
    public Transform twoPoint;

    public GameObject[] balls;
    private List<GameObject> ballPool;


    void Start()
    {
        ballPool = new List<GameObject>();
        //StartSpawner();
    }

    public void StartSpawner()
    {
        StopAllCoroutines();
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        GameObject go;
        Vector3 randPos;
        
        while (CatchPang_DataManager.Instance.levelTimer.timeLeft > 0)
        {
            go = Instantiate(balls[Random.Range(0, balls.Length - 1)]);
            randPos = new Vector3(Random.Range(onePoint.localPosition.x, twoPoint.localPosition.x), transform.localPosition.y , transform.localPosition.z);
            
            ballPool.Add(go);

            go.transform.SetParent(this.transform);
            go.transform.position = randPos;

            yield return new WaitForSecondsRealtime(0.5f);
        }

        yield return null;
    }

    public void OnRoundEnd()
    {
        foreach (var v in balls)
        {
            Destroy(v);
        }
    }
}
