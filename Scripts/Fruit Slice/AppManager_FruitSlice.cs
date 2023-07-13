using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager_FruitSlice : MonoBehaviour
{
    private FruitSpawner fruitSpawner;
    public GameObject countdownPanel;
    public bool gamePlay = false;


    public static AppManager_FruitSlice Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
        fruitSpawner = FindObjectOfType<FruitSpawner>();
        countdownPanel.SetActive(true);
        FruitSoundManager.Instance.OneTwoThreeSound();

        OnRoundStart();
    }

    void OnEnable()
    {
        FruitTimer.RoundEnd += OnRoundEnd;
    }

    void OnDisable()
    {
        FruitTimer.RoundEnd -= OnRoundEnd;
    }

    void OnRoundEnd()
    {
        StopAllCoroutines();
        StartCoroutine(_OnRoundEnd());
    }

    public void OnRoundStart()
    {
        StopAllCoroutines();
        StartCoroutine(_OnRoundStart());
    }


    private IEnumerator _OnRoundStart()
    {
        FruitSoundManager.Instance.BGMSoundStart();
        yield return FruitDataManager.Instance.FireShowOff();
        yield return FruitUIManager.Instance.OnRoundStart();
        yield return new WaitForSeconds(4f);

        gamePlay = true;
        yield return FruitDataManager.Instance.OnRoundStart();

        fruitSpawner.StartSpawner();    //과일 스폰

        FindObjectOfType<MaterialChanger>().ChangeWallMaterial();

        //AdManager.Instance.ToggleAd(true);
    }

    private IEnumerator _OnRoundEnd()
    {
        //AdManager.Instance.ToggleAd(false);

        gamePlay = false;
        yield return FruitDataManager.Instance.OnRoundEnd();
        yield return FruitUIManager.Instance.OnRoundEnd();

        fruitSpawner.OnRoundEnd();
    }

    public void HomeBtnOnClick()
    {
        SceneManager.LoadScene("Main");
    }
}
