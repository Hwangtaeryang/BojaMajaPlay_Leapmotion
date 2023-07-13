using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FruitSpawner : MonoBehaviour
{
    public Transform northPoint;
    public Transform southPoint;
    public Transform eastPoint;
    public Transform westPoint;
    public int minFruitsPerBatch = 1;
    public int maxFruitsPerBatch = 5;
    public float minSecondsInbetweenSpawns = 0.5f;
    public float maxSecondsInbetweenSpawns = 2f;
    public GameObject[] fruits;

    private List<GameObject> fruitPool;
    private FruitGoreSplash splasher;


    public LeapMotionOptionBtnOnClick spawner;

    void Start()
    {
        fruitPool = new List<GameObject>();
        splasher = GetComponent<FruitGoreSplash>();
        maxFruitsPerBatch += 1;

        // StartSpawner();
    }

    public void StartSpawner()
    {
        StopAllCoroutines();
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        GameObject fruitObj;
        Vector3 randPos;
        int batchCount;
        //if (AppManager_FruitSlice.Instance.gamePlay)
        //{
            while (FruitDataManager.Instance.levelTimer.timeLeft > 0)
            {
                batchCount = Random.Range(minFruitsPerBatch, maxFruitsPerBatch);

                yield return new WaitForSeconds(minSecondsInbetweenSpawns + (batchCount * 0.2f));

                for (int i = 0; i < batchCount; i++)
                {
                    randPos.x = Random.Range(westPoint.position.x, eastPoint.position.x);
                    randPos.z = Random.Range(southPoint.position.z, northPoint.position.z);
                    randPos.y = this.transform.position.y;

                    fruitObj = Instantiate(fruits[Random.Range(0, fruits.Length)], randPos, Quaternion.identity, this.transform);
                    //FruitSoundManager.Instance.FruitFlySound();
                    splasher.SplashGore(fruitObj.transform);

                    fruitPool.Add(fruitObj);
                }

            }
       // }


        yield return null;
    }

    public void OnRoundEnd()
    {
        StopAllCoroutines();
        foreach (var v in fruitPool)
        {
            Destroy(v);
            
        }
    }
}

