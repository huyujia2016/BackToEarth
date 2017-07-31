using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{

    public static SoundManager _instance;
    private Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    public AudioClip[] audioClipArray;
    [HideInInspector]
    public AudioSource audioSource;
    private bool isQuiet = false;

    private void Awake()
    {
        _instance = this;
        audioSource = this.GetComponent<AudioSource>();
    }

    private void Start()
    {
        foreach (AudioClip ac in audioClipArray)
        {
            audioDic.Add(ac.name, ac);
        }
    }

    public void Play(string audioName, AudioSource audioSource,bool isAutoStop=true,float volume = 1)
    {
        if (isQuiet)
        {
            return;
        }
        //自动停止正在播放的音效播放当前音效
        if (isAutoStop)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        if (audioSource.isPlaying)
        {
            return;
        }
        AudioClip ac;
        if (audioDic.TryGetValue(audioName, out ac))
        {
            audioSource.PlayOneShot(ac, volume);
        }
    }

    public void StopPlay(AudioSource audioSource)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}

