using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxingGameManager : MonoBehaviour
{
    public static BoxingGameManager instance { get; private set; }

    public bool gamePlay = false;
    public GameObject countdownPanel;


    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
        GamePlayStart();
        countdownPanel.SetActive(true);
    }

    // 활성화될때마다 호출 > Scene 오브젝트에 있음. 켜질때 한번
    void OnEnable()
    {
        BoxingTimer.RoundEnd += GamePlayEnd;
        //Debug.Log("Timer.RoundEnd OnEnable : " + Timer.RoundEnd);
    }

    // 비활성화될때마다 호출 > 꺼질때 한번
    void OnDisable()
    {
        BoxingTimer.RoundEnd -= GamePlayEnd;
        //Debug.Log("Timer.RoundEnd OnDisable : " + Timer.RoundEnd);
    }

    void GamePlayEnd()
    {
        StopAllCoroutines();
        StartCoroutine(_GameEnd());
    }


    public void GamePlayStart()
    {
        //Debug.Log("ddd");
        // 실행중인 코루틴 다 멈추고
        StopAllCoroutines();

        // 게임 플레이 시간 시작
        StartCoroutine(_GameStart());
    }

    //게임 시작 코루틴
    private IEnumerator _GameStart()
    {
        yield return BoxingUIManager.instance.GameStart();
        BoxingSoundManager.instance.OneTwoThreeSound();

        yield return new WaitForSeconds(4f);

        gamePlay = true;
        
        yield return BoxingDataManager.instance.GameStart();
        
    }

    //게임 종료 코루틴
    private IEnumerator _GameEnd()
    {
        gamePlay = false;

        
        yield return BoxingDataManager.instance.GameEnd();
        yield return BoxingUIManager.instance.GameEnd();

        yield return null;
    }

    public void HomeBtnOnClick()
    {
        SceneManager.LoadScene("Main");
    }
}
