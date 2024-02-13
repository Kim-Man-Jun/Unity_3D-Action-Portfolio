using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    AudioSource Audio;

    public AudioClip startSound;
    public AudioClip stageSound;
    public AudioClip victorySound;
    public AudioClip defeatSound;

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

        StartSound();
    }

    public void StartSound()
    {
        Audio.clip = startSound;
        Audio.loop = true;

        Audio.Play();
    }

    public void StageSound()
    {
        Audio.clip = stageSound;
        Audio.loop = true;

        Audio.Play();
    }

    public void VictorySound()
    {
        Audio.clip = victorySound;
        Audio.loop = false;

        Audio.Play();
    }

    public void DefeatSound()
    {
        Audio.clip = defeatSound;
        Audio.loop = false;

        Audio.Play();
    }

    public void musicStop()
    {
        Audio.Stop();
    }
}
