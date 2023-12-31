﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTiger : MonoBehaviour
{
    private Vector3 initPos;
    private float move;
    private bool randMove = false;
    private float DownCount;
    private Animator animator;
    private GameObject hitParticle;

    [Header("BackHoe Info")]
    public float health = 10;
    public float damage = 10;
    public float points = 1000;

    [Header("Random Info")]
    public float speed = 15;
    public float minSecondbetweenCubes = 1f;
    public float maxSecondbetweenCubes = 3f;

    [Header("Hit Sfx")]
    [SerializeField] string[] sound_hit;

    [Header("Move Sfx")]
    [SerializeField] string[] sound_move;

    private void Start()
    {
        animator = GetComponent<Animator>();
        initPos = gameObject.transform.position;

        animator.SetBool("Idle", true);
    }

    private void Update()
    {
        if (randMove)
        {
            if (initPos == transform.position)
            {
                IceTiger_SoundManager.Instance.PlaySE(sound_move[Random.Range(0, 3)]);
            }

            move = Time.deltaTime * speed;
            DownCount -= Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, new Vector3(initPos.x, initPos.y + 0.2f, initPos.z), move);

            //Debug.Log("DownCount : " + DownCount);
            // 여기서 시간을 랜덤으로 주고 넘어가야함, 바로 내려가는 루틴은 안됨
            // 랜덤으로 준 시간보다 작아졌는지, 올라와있는 위치값인지
            if (DownCount < 0f && transform.position == new Vector3(initPos.x, initPos.y + 0.2f, initPos.z))
            {
                randMove = false;
                StartCoroutine(_OnDown());
            }
        }
    }

    public void OnDown()
    {
        if (randMove)
        {
           
            // 파티클
            hitParticle = Instantiate(IceTiger_DataManager.Instance.hitParticle[Random.Range(0, 3)], gameObject.transform);
            hitParticle.transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
            

            // 피격시 효과음
            IceTiger_SoundManager.Instance.PlaySE(sound_hit[Random.Range(0, 3)]);

            // 데미지 체크
            health -= IceTiger_DataManager.Instance.hammerDamage;

            // 체크 후 제어
            if (health <= 0)
            {
                //Debug.Log("points! : " + points);
                IceTiger_DataManager.Instance.AddScore(points);
                health = 10;
            }
        }

        randMove = false;

        animator.SetBool("Idle", false);
        animator.SetBool("Eat", true);
        //StopAllCoroutines();
        StartCoroutine(_OnDown());
    }

    IEnumerator _OnDown()
    {
        yield return new WaitUntil(() => DownCharacter());
    }

    private bool DownCharacter()
    {
        if (transform.position != initPos)
        {
            move = Time.deltaTime * speed;
            transform.position = Vector3.Lerp(transform.position, initPos, move);
            return false;
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Eat", false);
            return true;
        }
    }

    private void OnEnable()
    {
        IceTiger_AppManager.RoundStart += RoundStart;
        IceTiger_Timer.RoundEnd += RoundEnd;
    }

    private void OnDisable()
    {
        IceTiger_AppManager.RoundStart -= RoundStart;
        IceTiger_Timer.RoundEnd -= RoundEnd;
    }

    private void RoundStart()
    {
        StopAllCoroutines();
        StartCoroutine(_RoundStart());
    }

    private IEnumerator _RoundStart()
    {
        float UpCount;

        yield return new WaitUntil(() => IceTiger_Timer.isPlaying);

        while (IceTiger_Timer.Instance.timeLeft > 0)
        {
            // 랜덤 1, 2 
            // 2.3초 ~ 2.6초
            //UpCount = 2f + (Random.Range(minSecondbetweenCubes, maxSecondbetweenCubes) * 0.3f);

            if (IceTiger_Timer.Instance.timeLeft > 19.9f)
            {
                //IceTiger_SoundManager.Instance.bgmPlayerPitchControll(1f);
                // 2.3초 ~ 2.6초
                UpCount = 2f + (Random.Range(minSecondbetweenCubes, maxSecondbetweenCubes) * 0.3f);
            }
            else if(IceTiger_Timer.Instance.timeLeft > 9f)
            {
                //IceTiger_SoundManager.Instance.bgmPlayerPitchControll(1.05f);
                // 1.5초 ~ 1.8초
                UpCount = 1.5f + (Random.Range(minSecondbetweenCubes, maxSecondbetweenCubes) * 0.3f);
            }
            else
            {
                //IceTiger_SoundManager.Instance.bgmPlayerPitchControll(1.1f);
                // 1.5초 ~ 1.8초
                UpCount = 1f + (Random.Range(minSecondbetweenCubes, maxSecondbetweenCubes) * 0.3f);
            }

            // 0.7 ~ 0.9초
            //DownCount = 0.5f + (Random.Range(minSecondbetweenCubes, maxSecondbetweenCubes) * 0.2f);

            if (IceTiger_Timer.Instance.timeLeft > 19.9f)
            {
                // 0.7 ~ 0.9초
                DownCount = 0.5f + (Random.Range(minSecondbetweenCubes, maxSecondbetweenCubes) * 0.2f);
            }
            else if(IceTiger_Timer.Instance.timeLeft > 9f)
            {
                // 0.6 ~ 0.7초
                DownCount = 0.4f + (Random.Range(minSecondbetweenCubes, maxSecondbetweenCubes) * 0.2f);
            }
            else
            {
                // 0.4 ~ 0.5초
                DownCount = 0.15f + (Random.Range(minSecondbetweenCubes, maxSecondbetweenCubes) * 0.2f);
            }

            // 1일때 Up / 0 일때 Down
            randMove = Random.Range(0, 2) == 1 ? true : false;
            //Debug.Log("randMove: " + randMove);
            /*********************************************************************/

            // 2.3초 ~ 2.6초
            yield return new WaitForSeconds(UpCount);
        }

        yield return null;
    }

    private void RoundEnd()
    {
        StopAllCoroutines();
        StartCoroutine(_RoundEnd());
    }

    private IEnumerator _RoundEnd()
    {
        //animator.SetBool("Eat", false);
        //animator.SetBool("Idle", true);

        yield return new WaitUntil(() => DownCharacter());

        yield return null;
    }
}
