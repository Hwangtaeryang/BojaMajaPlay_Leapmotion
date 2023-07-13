using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeapMotionOptionBtnOnClick : MonoBehaviour
{

    public GameObject optionPopup;
    public OptionCtrl optionctrlScript;

    void Start()
    {
        
    }



    //일시정지
    public void PressPause()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            GameManager.instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "WitchGame")
        {
            WitchSoundManager.instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "Fruit Slice")
        {
            FruitSoundManager.Instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "Window")
        {
            WindowSoundManager.instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "GrillingMeat")
        {
            GrillingMeat_SoundManager.Instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "CatchPang")
        {
            CatchPang_SoundManager.Instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "Ice Tiger")
        {
            IceTiger_SoundManager.Instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "StoneGGagi")
        {
            GoSoundManager.Instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "Mosquito")
        {
            SoundManager.Instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "Boxing")
        {
            BoxingSoundManager.instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "TreeSlash")
        {
            TreeSlashSoundManager.Instance.AllSoundPause();
        }
        else if (SceneManager.GetActiveScene().name == "EndScene")
        {
            GameManager.instance.AllSoundPause();
        }
        
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("L_Hand") || other.CompareTag("R_Hand"))
        {
            PressPause();
            optionctrlScript.optionCount = 1;
            optionPopup.SetActive(true);
        }
    }
}
