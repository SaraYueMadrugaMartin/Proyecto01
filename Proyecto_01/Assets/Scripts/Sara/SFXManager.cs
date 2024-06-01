using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] AudioSource SFXScore;
    [SerializeField] AudioSource musicSource;

    public AudioClip cancionNana;
    public AudioClip cancionCabana;
    public AudioClip seleccionBoton01;
    public AudioClip seleccionBoton02;
    public AudioClip seleccionBoton03;
    public AudioClip pasosAlex;
    public AudioClip correrAlex;
    public AudioClip ataqueBate;
    public AudioClip disparoPistola;
    public AudioClip recargaPistola;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        musicSource.clip = cancionNana;
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

    public void CancionCabaña()
    {
        musicSource.clip = cancionCabana;
        musicSource.Play();
    }
}
