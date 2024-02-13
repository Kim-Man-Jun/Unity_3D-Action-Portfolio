using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public Image panel;
    public float fadeTime = 1f;
    private float timer;

    public GameObject SettingWindow;
    Slider BGMVolume;
    Slider SFXVolume;

    float lastSFXVolumeValue = 0;

    private void Start()
    {
        SettingWindow.SetActive(false);
        BGMVolume = SettingWindow.transform.GetChild(2).GetComponent<Slider>();
        SFXVolume = SettingWindow.transform.GetChild(4).GetComponent<Slider>();
    }

    private void Update()
    {
        BGMManager.instance.GetComponent<AudioSource>().volume = BGMVolume.value;
        SFXManager.instance.GetComponent<AudioSource>().volume = SFXVolume.value;

        float sfxVolumeValue = SFXVolume.value;

        // 0.1단위마다 사운드 재생
        if (Mathf.Floor(sfxVolumeValue * 10) != Mathf.Floor(lastSFXVolumeValue * 10))
        {
            lastSFXVolumeValue = sfxVolumeValue;

            if (lastSFXVolumeValue >= 0.1f)
            {
                SFXManager.instance.ButtonSound();
            }
        }
    }

    public void GameStartButton()
    {
        SFXManager.instance.ButtonSound();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        //초기값
        float colorA = panel.color.a;
        //목표값
        float targetColorA = 1f;

        while (timer < fadeTime)
        {
            float color = Mathf.Lerp(colorA, targetColorA, timer / fadeTime);
            panel.color = new Color(0, 0, 0, color);
            timer += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("Stage");
    }

    public void SettingButton()
    {
        SFXManager.instance.ButtonSound();
        SettingWindow.SetActive(true);
    }

    public void SettingButtonQuit()
    {
        SFXManager.instance.ButtonSound();
        SettingWindow.SetActive(false);
    }

    public void GameQuitButton()
    {
        SFXManager.instance.ButtonSound();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
