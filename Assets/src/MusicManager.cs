using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip normalMusicStart;
    [SerializeField] private AudioClip normalMusicLoop;
    [SerializeField] private AudioClip suwakoMusicLoop;

    public void ChangeBGMSuwako(){
        musicSource.Stop();
        musicSource.clip = suwakoMusicLoop;
        musicSource.Play();
    }

    public void ChangeBGMNormal(){
        musicSource.Stop();
        musicSource.clip = normalMusicLoop;
        musicSource.PlayOneShot(normalMusicStart);
        musicSource.PlayScheduled(AudioSettings.dspTime + normalMusicStart.length);
    }

    void Start(){
        musicSource.PlayOneShot(normalMusicStart);
        musicSource.PlayScheduled(AudioSettings.dspTime + normalMusicStart.length);
    }
}
