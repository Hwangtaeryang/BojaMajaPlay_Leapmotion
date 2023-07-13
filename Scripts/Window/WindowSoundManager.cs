using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowSoundManager : MonoBehaviour
{
    public static WindowSoundManager instance { get; private set; }

    [Header("사운드 종류")]
    public AudioClip score_sound;   //점수올라가는 
    public AudioClip failure_sound; //실패
    public AudioClip success_sound; //성공
    public AudioClip trumpet_sound; //성공프럼펫
    public AudioClip onetwo_soung;  //1.2.3사운드

    [Header("창문 효과음")]
    public AudioClip[] window_sound;    //창문닦기
    public AudioClip bridwindow_sound;    //새똥창문닦기
    public AudioClip[] windowClean_sound;   //창문 완료
    public AudioClip[] poop_sound;  //똥소리


    [Header("사운드 오디오")]
    public AudioSource myAudio;
    public AudioSource window; //창문닦기소리
    public AudioSource bgmAudio;    //BGM사운드
    public AudioSource timer5Audio; //5초남았을때 사운드
    public AudioSource levelUp;
    public AudioSource iconChange;

    [Header("효과음")]
    public AudioClip levelup_sound; //레벨업 소리
    public AudioClip iconChange_sound;  //슬라이더 아이콘 변경



    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

    }


    public void WindowSound()
    {
        int num = Random.Range(0, window_sound.Length);
        window.PlayOneShot(window_sound[num]); 
    }

    public void BirdWindowSound()
    {
        window.PlayOneShot(bridwindow_sound);
    }




    public void WindowCleanSound()
    {
        int num = Random.Range(0, windowClean_sound.Length);
        myAudio.PlayOneShot(windowClean_sound[num]);
    }

    public void BridPoopSound()
    {
        int num = Random.Range(0, poop_sound.Length);
        myAudio.PlayOneShot(poop_sound[num]);
    }


    //성공 시 사운드
    public void SuccessSound()
    {
        myAudio.PlayOneShot(success_sound);
        myAudio.PlayOneShot(trumpet_sound);
        bgmAudio.Stop();
        timer5Audio.Stop();
    }

    //실패 시 사운드
    public void FailureSound()
    {
        myAudio.PlayOneShot(failure_sound);
        bgmAudio.Stop();
        timer5Audio.Stop();
    }

    public void OneTwoThreeSound()
    {
        myAudio.PlayOneShot(onetwo_soung);
    }

    //5초남았을때 사운드
    public void Timer5Sound()
    {
        timer5Audio.Play();
    }

    //5초사운드 정지
    public void Timer5SoundStop()
    {
        timer5Audio.Stop();
    }

    //Level UP 사운드
    public void LevelUpSound()
    {
        levelUp.PlayOneShot(levelup_sound);

    }

    public void IconImageChange()
    {
        iconChange.PlayOneShot(iconChange_sound);
    }

    //일시정지
    public void AllSoundPause()
    {
        myAudio.Pause();
        window.Pause();
        bgmAudio.Pause();
        timer5Audio.Pause();
        levelUp.Pause();
        iconChange.Pause();
    }

    //재생
    public void AllSoundPlay()
    {
        myAudio.UnPause();
        window.UnPause();
        bgmAudio.UnPause();
        timer5Audio.UnPause();
        levelUp.UnPause();
        iconChange.UnPause();
    }

}
