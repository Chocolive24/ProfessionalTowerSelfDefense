using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")] 
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundEffectSource;
    
    [Header("Music Clips")] 
    [SerializeField] private AudioClip _mainTheme;
    [SerializeField] private AudioClip _gameOverTheme;

    private Nexus _nexus;

    private void Awake()
    {
        _nexus = FindObjectOfType<Nexus>();
        _nexus.OnDeath += PlayGameOverSound;
        
        PlayMusic(_mainTheme, true);
    }
    
    void PlayMusic(AudioClip musicClip, bool loop)
    {
        _musicSource.clip = musicClip;
        _musicSource.loop = loop;
        _musicSource.Play();
    }
    
    void PlaySoundEffect(AudioClip soundEffectClip)
    {
        _soundEffectSource.clip = soundEffectClip;
        _soundEffectSource.Play();
    }
    
    private void PlayGameOverSound(Entity obj)
    {
        PlayMusic(_gameOverTheme, false);
    }
}
