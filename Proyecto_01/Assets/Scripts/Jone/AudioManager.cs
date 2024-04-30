using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sonidos[] sonidos;

    public static AudioManager instance;

    void Awake()
    {
        // Estructura Singleton
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

        foreach (Sonidos s in sonidos)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volumen;
            s.source.pitch = s.tono;
            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        Play("MainTheme");
    }
    public void Play (string nombreSonido)
    {
        Sonidos s = Array.Find(sonidos, sonido => sonido.nombre == nombreSonido);
        if (s == null)
        {
            Debug.LogWarning("Sonido: " + nombreSonido + " no encontrado.");
            return;
        }
        s.source.Play();
    }
}
