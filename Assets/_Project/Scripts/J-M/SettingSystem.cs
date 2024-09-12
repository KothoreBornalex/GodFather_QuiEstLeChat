using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingSystem : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;

    Animator uiAnimator;

    private bool settingsOpen = false;


    void Awake()
    {
        uiAnimator = GetComponent<Animator>();
    }
    //void Start()
    //{
    //    // Garantit que ce GameObject ne sera pas d�truit lorsque la sc�ne est chang�e
    //    DontDestroyOnLoad(gameObject);
    //}

    public void StartAnimation()
    {
        if (uiAnimator != null)
        {
            if(!settingsOpen)
            {
                uiAnimator.SetBool("SettingsON", true);
                settingsOpen = true;
            }
            else
            {
                EndAnimation();
            }
            
        }
    }

    public void EndAnimation()
    {
        if (uiAnimator != null)
        {
            uiAnimator.SetBool("SettingsON", false);
            settingsOpen = false;

        }
    }

    public void SetMusicVolume(float sliderValue)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }
}
