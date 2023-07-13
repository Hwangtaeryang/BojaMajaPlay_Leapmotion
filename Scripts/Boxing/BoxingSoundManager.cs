using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingSoundManager : MonoBehaviour
{
    public static BoxingSoundManager instance { get; private set; }

    [Header("사운드 종류")]
    public AudioClip score_sound;   //점수올라가는 
    public AudioClip failure_sound; //실패
    public AudioClip success_sound; //성공
    public AudioClip trumpet_sound; //성공프럼펫
    public AudioClip onetwo_soung;  //1.2.3사운드

    [Header("권투 효과음")]
    public AudioClip[] punch_sound; //펀치 소리
    public AudioClip[] swing_sound; //스윙 소리
    public AudioClip levelup_sound; //레벨업 소리
    public AudioClip iconChange_sound;  //슬라이더 아이콘 변경

    [Header("사운드 오디오")]
    public AudioSource myAudio;
    public AudioSource bgmAudio;    //BGM사운드
    public AudioSource timer5Audio; //5초남았을때 사운드
    public AudioSource leftGlove;
    public AudioSource rightGlove;
    public AudioSource levelUp;
    public AudioSource iconChange;


    void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;

    }

    //왼쪽 펀치
    public void LeftPunchSound()
    {
        int num = Random.Range(0, punch_sound.Length);
        leftGlove.PlayOneShot(punch_sound[num]);
    }

    //오른쪽 펀치
    public void RightPunchSound()
    {
        int num = Random.Range(0, punch_sound.Length);
        rightGlove.PlayOneShot(punch_sound[num]);
    }

    public void LeftSwingSound()
    {
        int num = Random.Range(0, swing_sound.Length);
        leftGlove.PlayOneShot(swing_sound[num]);
    }

    public void RightSwingSound()
    {
        int num = Random.Range(0, swing_sound.Length);
        rightGlove.PlayOneShot(swing_sound[num]);
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

    public void AllSoundPause()
    {
        myAudio.Pause();
        bgmAudio.Pause();
        timer5Audio.Pause();
        levelUp.Pause();
        iconChange.Pause();
    }

    public void AllSoundPlay()
    {
        myAudio.UnPause();
        bgmAudio.UnPause();
        timer5Audio.UnPause();
        levelUp.UnPause();
        iconChange.UnPause();
    }
}
