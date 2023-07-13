using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WitchGameManager : MonoBehaviour
{

    public bool gamePlay = false;
    public GameObject countdownPanel;

    public static WitchGameManager instance { get; private set; }


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

    
    void Update()
    {
        
    }

    // 활성화될때마다 호출 > Scene 오브젝트에 있음. 켜질때 한번
    void OnEnable()
    {
        WitchTimer.RoundEnd += GamePlayEnd;
        //Debug.Log("Timer.RoundEnd OnEnable : " + Timer.RoundEnd);
    }

    // 비활성화될때마다 호출 > 꺼질때 한번
    void OnDisable()
    {
        WitchTimer.RoundEnd -= GamePlayEnd;
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
        WitchSoundManager.instance.BGMSoundStart();
        yield return WitchSoundManager.instance.OneTwoThreeSound();
        yield return WitchUIManager.instance.GameStart();
        yield return WitchDataManager.instance.ParticleEnd();   //성공 시 파티클 끄는 함수
        yield return new WaitForSeconds(4f);

        gamePlay = true;
        //게임 플레이 시간 시작
        yield return WitchDataManager.instance.GameStart();

        //AdManager.Instance.ToggleAd(true);

    }

    //게임 종료 코루틴
    private IEnumerator _GameEnd()
    {
        //AdManager.Instance.ToggleAd(false);
        
        gamePlay = false;
        WitchSoundManager.instance.BublleSoundEnd();
        yield return WitchDataManager.instance.GameEnd();
        yield return WitchUIManager.instance.GameEnd();

        yield return null;
    }

    public void HomeBtnOnClick()
    {
        SceneManager.LoadScene("Main");
    }


}
