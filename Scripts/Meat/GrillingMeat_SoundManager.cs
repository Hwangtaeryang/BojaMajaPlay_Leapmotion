using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}

public class GrillingMeat_SoundManager : MonoBehaviour
{
    public static GrillingMeat_SoundManager Instance { get; private set; }

    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSounds;
    [SerializeField] Sound[] sfxSounds;

    [Header("브금 플레이어")]
    [SerializeField] AudioSource bgmPlayer;
    [SerializeField] AudioSource grillingBGMPlayer;

    [Header("효과음 플레이어")]
    [SerializeField] AudioSource[] sfxPlayer;
    public AudioSource levelUp;
    public AudioSource iconChange;

    [Header("효과음")]
    public AudioClip levelup_sound; //레벨업 소리
    public AudioClip iconChange_sound;  //슬라이더 아이콘 변경



    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayMainBGM();
    }

    public void PlaySE(string _soundName)
    {
        for (int i = 0; i < sfxSounds.Length; i++)
        {
            if (_soundName == sfxSounds[i].soundName)
            {
                for (int x = 0; x < sfxPlayer.Length; x++)
                {
                    if (!sfxPlayer[x].isPlaying)
                    {
                        sfxPlayer[x].clip = sfxSounds[i].clip;
                        sfxPlayer[x].Play();
                        return;
                    }
                }
                //Debug.Log("모든 효과음이 사용중!");
                return;
            }
        }
        //Debug.Log("등록된 효과음이 없습니다!");
    }

    public void StopSfx()
    {
        for (int x = 0; x < sfxPlayer.Length; x++)
        {
            if (sfxPlayer[x].isPlaying)
            {
                sfxPlayer[x].Stop();
            }
        }
    }

    public void StopSelectedSfx(string _soundName)
    {
        for (int i = 0; i < sfxSounds.Length; i++)
        {
            if (_soundName == sfxSounds[i].soundName)
            {
                for (int x = 0; x < sfxPlayer.Length; x++)
                {
                    if (sfxPlayer[x].isPlaying)
                    {
                        sfxPlayer[x].Stop();
                    }
                }
            }
        }
    }

    public void PlayMainBGM()
    {
        bgmPlayer.clip = bgmSounds[0].clip;
        bgmPlayer.Play(); 
    }

    public void PlayGrillingBGM()
    {
        grillingBGMPlayer.clip = bgmSounds[1].clip;
        grillingBGMPlayer.Play();
    }

    public void StopMainBGM()
    {
        bgmPlayer.Stop();
    }

    public void StopGrillingBGM()
    {
        grillingBGMPlayer.Stop();
    }

    public void bgmPlayerVolumeControll(float _volume)
    {
        bgmPlayer.volume = _volume;
        grillingBGMPlayer.volume = _volume;
    }

    public void bgmPlayerPitchControll(float Pitch)
    {
        switch (Pitch)
        {
            case 1f:
                bgmPlayer.pitch = 1f;
                break;
            case 1.05f:
                bgmPlayer.pitch = 1.05f;
                break;
            case 1.1f:
                bgmPlayer.pitch = 1.1f;
                break;
        }
    }

    public void bgmAfterGameEnd(string endState)
    {
        if (endState.Equals("Success Screen"))
        {
            PlaySE("ClearSFX");
        }
        else if (endState.Equals("Fail Screen"))
        {
            PlaySE("FailSFX");
        }
    }

    public void sfxLimitFiveSec()
    {
        PlaySE("Limit5sec");
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
        bgmPlayer.Pause();
        grillingBGMPlayer.Pause();

        for (int i = 0; i < sfxPlayer.LongLength; i++)
            sfxPlayer[i].Pause();

        levelUp.Pause();
        iconChange.Pause();
    }

    //재생
    public void AllSoundPlay()
    {
        bgmPlayer.UnPause();
        grillingBGMPlayer.UnPause();

        for (int i = 0; i < sfxPlayer.LongLength; i++)
            sfxPlayer[i].UnPause();

        levelUp.UnPause();
        iconChange.UnPause();
    }
}
