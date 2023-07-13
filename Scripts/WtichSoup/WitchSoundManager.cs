using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSoundManager : MonoBehaviour
{
    public static WitchSoundManager instance { get; private set; }

    [Header("사운드 종류")]
    public AudioClip score_sound;   //점수올라가는 
    public AudioClip death_sound;   //폭발
    public AudioClip bubble_sound;  //거품
    public AudioClip wtich_sound;   //마녀소리

    public AudioClip failure_sound; //실패
    public AudioClip success_sound; //성공
    public AudioClip trumpet_sound; //성공프럼펫
    public AudioClip onetwo_soung;  //1.2.3사운드

    [Header("사운드 오디오")]
    public AudioSource myAudio;
    public AudioSource bgmAudio;    //BGM사운드
    public AudioSource bubbleAudio; //거품사운드
    public AudioSource timer5Audio; //5초남았을때 사운드
    public AudioSource fireAudio;   //모낙불 사운드
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

        myAudio = GetComponent<AudioSource>();
    }
    
    

    public void BGMSoundStart()
    {
        bgmAudio.Play();
        bgmAudio.loop = true;
    }

    public void StartSocreUpSound()
    {
        //myAudio.clip = sound[Random.Range(0, sound.Length)];
        //myAudio.clip = score_sound;
        //myAudio.Play();
        myAudio.PlayOneShot(score_sound);
    }

    public void DeathSound()
    {
        //myAudio.clip = death_sound;
        //myAudio.Play();
        myAudio.PlayOneShot(death_sound);
    }

    public void WtichSound()
    {
        myAudio.PlayOneShot(wtich_sound);
    }

    public void BublleSoundStart()
    {
        timer5Audio.Stop();
        //Debug.Log("사운드 시작");
        bubbleAudio.clip = bubble_sound;
        bubbleAudio.Play();
        bubbleAudio.loop = true;
    }

    public void BublleSoundEnd()
    {
        bubbleAudio.Stop();
    }

    //성공 시 사운드
    public void SuccessSound()
    {
        myAudio.PlayOneShot(success_sound);
        myAudio.PlayOneShot(trumpet_sound);
        bgmAudio.Stop();
        bubbleAudio.Stop();
        timer5Audio.Stop();
    }

    //실패 시 사운드
    public void FailureSound()
    {
        myAudio.PlayOneShot(failure_sound);
        bgmAudio.Stop();
        bubbleAudio.Stop();
        timer5Audio.Stop();
    }

    public IEnumerator OneTwoThreeSound()
    {
        myAudio.PlayOneShot(onetwo_soung);
        yield return null;
    }

    //5초남았을때 사운드
    public void Timer5Sound()
    {
        timer5Audio.Play();
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
        bubbleAudio.Pause();
        timer5Audio.Pause();
        fireAudio.Pause();
        levelUp.Pause();
        iconChange.Pause();
    }

    //재생
    public void AllSoundPlay()
    {
        myAudio.UnPause();
        bgmAudio.UnPause();
        bubbleAudio.UnPause();
        timer5Audio.UnPause();
        fireAudio.UnPause();
        levelUp.UnPause();
        iconChange.UnPause();
    }

}
