
using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;
    public SoundScript[] soundScripts;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        foreach (SoundScript s in soundScripts)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.Volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
            s.source.priority = 0;
        }
    }
    void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        SoundScript s = Array.Find(soundScripts, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound :" + name + "not found!");
            return;
        }

        s.source.Play();
    }
}
