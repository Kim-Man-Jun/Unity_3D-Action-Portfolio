using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    AudioSource Audio;

    [Header("Button Sound")]
    public AudioClip buttonSound;

    [Header("Battle Sound")]
    public AudioClip attackHit;
    public AudioClip attackMiss;
    public AudioClip skill1Sound;
    public AudioClip skill2Sound;
    public AudioClip skill2Hit;
    public AudioClip skill3Sound;
    public AudioClip playerHit;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    public void ButtonSound()
    {
        Audio.PlayOneShot(buttonSound);
    }

    public void AttackHit()
    {
        Audio.PlayOneShot(attackHit);
    }

    public void AttackMiss()
    {
        Audio.PlayOneShot(attackMiss);
    }

    public void Skill1Sound()
    {
        Audio.PlayOneShot(skill1Sound);
    }

    public void Skill2Sound()
    {
        Audio.PlayOneShot(skill2Sound);
    }

    public void Skill2Hit()
    {
        Audio.PlayOneShot(skill2Hit);
    }

    public void Skill3Sound()
    {
        Audio.PlayOneShot(skill3Sound);
    }

    public void PlayerHit()
    {
        Audio.PlayOneShot(playerHit);
    }
}
