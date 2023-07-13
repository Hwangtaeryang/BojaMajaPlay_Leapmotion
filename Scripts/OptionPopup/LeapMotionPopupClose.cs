using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeapMotionPopupClose : MonoBehaviour
{

    public GameObject optionPopup;
    public OptionCtrl optionctrlScript;

    //사운드 재생
    public void AllSoundPlay()
    {
        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().name == "Main")
        {
            GameManager.instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "WitchGame")
        {
            WitchSoundManager.instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "Fruit Slice")
        {
            FruitSoundManager.Instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "Window")
        {
            WindowSoundManager.instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "GrillingMeat")
        {
            GrillingMeat_SoundManager.Instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "CatchPang")
        {
            CatchPang_SoundManager.Instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "Ice Tiger")
        {
            IceTiger_SoundManager.Instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "StoneGGagi")
        {
            GoSoundManager.Instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "Mosquito")
        {
            SoundManager.Instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "Boxing")
        {
            BoxingSoundManager.instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "TreeSlash")
        {
            TreeSlashSoundManager.Instance.AllSoundPlay();
        }
        else if (SceneManager.GetActiveScene().name == "EndScene")
        {
            GameManager.instance.AllSoundPause();
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
        {
            AllSoundPlay();
            optionctrlScript.optionCount = 0;
            optionPopup.SetActive(false);
        }
    }
}
