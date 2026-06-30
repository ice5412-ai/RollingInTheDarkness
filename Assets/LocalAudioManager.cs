using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class LocalAudioManager : MonoBehaviour
{
    public Slider MusicHolder;
    public Slider SFXHolder;
    private AudioSource Preventation;
    public AudioMixer mixer;

    void Awake()
    {
        Preventation = SFXHolder.gameObject.GetComponent<AudioSource>();
        Preventation.enabled = false;
        bool musicValueCheck = mixer.GetFloat("MusicVol", out float musicValue);
        bool sfxValueCheck = mixer.GetFloat("SFXVol", out float sfxValue);
        MusicHolder.value = musicValue;
        SFXHolder.value = sfxValue;
    }
    
    void Start()
    {
        Preventation.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changeMusicvalue()
    {
        mixer.SetFloat("MusicVol", MusicHolder.value);
    }
    public void changeSFXvalue()
    {
        mixer.SetFloat("SFXVol", SFXHolder.value);
    }
}
