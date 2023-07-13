using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSoundManager : MonoBehaviour
{
    public static FruitSoundManager Instance { get; private set; }

    [Header("사운드 소스")]
    public AudioSource myAudio;
    public AudioSource bgmAudio;    //BGM사운드
    public AudioSource timer5Audio; //5초남았을때 사운드
    public AudioSource levelUp;
    public AudioSource iconChange;


    [Header("사운드 종류")]
    public AudioClip[] fly_sound;
    public AudioClip[] touch_sound;
    
    public AudioClip failure_sound; //실패
    public AudioClip success_sound; //성공
    public AudioClip trumpet_sound; //성공프럼펫
    public AudioClip onetwo_soung;  //1.2.3사운드

    

    [Header("효과음")]
    public AudioClip levelup_sound; //레벨업 소리
    public AudioClip iconChange_sound;  //슬라이더 아이콘 변경


    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }
    

    public void BGMSoundStart()
    {
        timer5Audio.Stop();
        bgmAudio.Play();
        bgmAudio.loop = true;
    }

    public void FruitFlySound()
    {
        int num = Random.Range(0, fly_sound.Length);
        myAudio.PlayOneShot(fly_sound[num]);
    }

    public void FruitTouchSound()
    {
        int num = Random.Range(0, touch_sound.Length);
        myAudio.PlayOneShot(touch_sound[num]);
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

    //원투쓰리 사운드
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
        bgmAudio.Pause();
        timer5Audio.Pause();
        levelUp.Pause();
        iconChange.Pause();
    }

    //재생
    public void AllSoundPlay()
    {
        myAudio.UnPause();
        bgmAudio.UnPause();
        timer5Audio.UnPause();
        levelUp.UnPause();
        iconChange.UnPause();
    }
}
