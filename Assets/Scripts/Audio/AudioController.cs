using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    
    [SerializeField] private AudioSource _effectsSource, _buttonsSource;
    public string GetMute()
    {
        return Saver.GetStringPrefs("MuteSound");
    }
    public void OffFullSound()
    {
        if (_effectsSource.clip != null) _effectsSource.Stop();
    }
    public static void MuteSound()
    {
        bool mute = false;
        if (Saver.GetStringPrefs("MuteSound") == "True") mute = false;
        else if (Saver.GetStringPrefs("MuteSound") == "False") mute = true;
        Saver.SaveStringPrefs("MuteSound", mute.ToString());
    }
    public void Pause()
    {
        _effectsSource.Pause();
    }
    public void Play()
    {
        _effectsSource.UnPause();
    }
    public void PlaySound(AudioClip clip)
    {
        if (Saver.GetStringPrefs("MuteSound") == "True") return;
        _effectsSource.PlayOneShot(clip);
        _effectsSource.clip = clip;
    }

    public void PlayButtons()
    {
        if (Saver.GetStringPrefs("MuteSound") == "True") return;
        _buttonsSource.Play();
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;       
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
