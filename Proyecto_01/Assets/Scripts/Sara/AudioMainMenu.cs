using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMainMenu : MonoBehaviour
{
    //public static SFXManager instance;

    [SerializeField] public AudioSource SFXScore;
    [SerializeField] public AudioSource musicSource;

    public AudioClip cancionFondo;
    public AudioClip seleccionBoton01;
    public AudioClip seleccionBoton02;
    public AudioClip seleccionBoton03;

    void Start()
    {
        musicSource.clip = cancionFondo;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip efecto)
    {
        SFXScore.PlayOneShot(efecto);
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
}
