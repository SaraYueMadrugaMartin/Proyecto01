using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sonidos[] sonidos;
    public static AudioManager instance;

    private float volumenGeneral = 1.0f; // Multiplicador de volumen general

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

            s.source.volume = s.volumen * volumenGeneral; // Multiplicar por el volumen general
            s.source.pitch = s.tono;
            s.source.loop = s.loop;
        }

        // Cargar el volumen general guardado
        CargarVolumen();
    }

    private void Start()
    {
        Play("MainTheme");
    }

    public void Play(string nombreSonido)
    {
        Sonidos s = Array.Find(sonidos, sonido => sonido.nombre == nombreSonido);
        if (s == null)
        {
            Debug.LogWarning("Sonido: " + nombreSonido + " no encontrado.");
            return;
        }
        StopAllSounds(); // Detiene todas las canciones antes de reproducir la nueva
        s.source.Play();
    }

    public void Stop(string nombreSonido)
    {
        Sonidos s = Array.Find(sonidos, sonido => sonido.nombre == nombreSonido);
        if (s == null)
        {
            Debug.LogWarning("Sonido: " + nombreSonido + " no encontrado.");
            return;
        }
        s.source.Stop();
    }

    public void StopAllSounds()
    {
        foreach (Sonidos s in sonidos)
        {
            s.source.Stop();
        }
    }

    public void ActualizarVolumenGeneral(float nuevoVolumen)
    {
        volumenGeneral = nuevoVolumen;
        foreach (Sonidos s in sonidos)
        {
            s.source.volume = s.volumen * volumenGeneral;
        }
        // Guardar el volumen general
        GuardarVolumen();
    }

    private void GuardarVolumen()
    {
        PlayerPrefs.SetFloat("VolumenGeneral", volumenGeneral);
        PlayerPrefs.Save();
    }

    private void CargarVolumen()
    {
        if (PlayerPrefs.HasKey("VolumenGeneral"))
        {
            volumenGeneral = PlayerPrefs.GetFloat("VolumenGeneral");
            ActualizarVolumenGeneral(volumenGeneral);
        }
    }
}

