using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sonidos
{
    public string nombre;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volumen;
    [Range(0.1f, 3f)]
    public float tono;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
