using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MosqouitoSceneChange : MonoBehaviour
{
    public static MosqouitoSceneChange Instance { get; private set; }
    

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else Instance = this;
        
    }

    public void SceneChange()
    {
        StartCoroutine(NextSceneChange());  //다음 게임으로
    }

    //게임끝나고 다음 게임으로 가는 함수
    IEnumerator NextSceneChange()
    {
        GameManager.instance.gamePlayNum += 1;
        //Debug.Log(GameManager.instance.gamePlayNum + ":::" + GameManager.instance.gameTotalSu);

        //마지막 게임이 끝나기 전까지 
        if (GameManager.instance.gamePlayNum < GameManager.instance.gameTotalSu)
            GameManager.instance.SceneMove(GameManager.instance.gamePlayNum);
        else if (GameManager.instance.gamePlayNum == GameManager.instance.gameTotalSu)
            SceneManager.LoadScene("EndScene");

        yield return null;
    }
}
