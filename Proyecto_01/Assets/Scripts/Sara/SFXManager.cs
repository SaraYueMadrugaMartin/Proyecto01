using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    //public static SFXManager instance;

    [SerializeField] public AudioSource SFXScore;

    public AudioClip[] clipsDeAudio;

    public AudioClip[] audiosEnemigos;

    [Range(0f, 1f)] public float volumen;

    //public AudioClip[] audiosXela;

    /*
    public AudioClip seleccionBoton01;
    public AudioClip seleccionBoton02;
    public AudioClip seleccionBoton03;
    public AudioClip pasosAlex;
    public AudioClip correrAlex;
    public AudioClip ataqueBate;
    public AudioClip disparoPistola;
    public AudioClip recargaPistola;
    public AudioClip abrirInventario;
    public AudioClip cerrarInventario;
    public AudioClip seleccionarObjetos;
    public AudioClip abrirCerradura;
    public AudioClip abrirPuerta;
    public AudioClip abrirPuertaCerrada;
    public AudioClip corazonLatir;
    public AudioClip guardarPartida;
    public AudioClip meterMoneda;
    public AudioClip movBate;
    public AudioClip AlexMuerte;
    /*public AudioClip AlexHit01;
    public AudioClip AlexHit02;
    public AudioClip AlexHit03;   
    public AudioClip AlexHerida01;
    public AudioClip AlexHerida02;
    public AudioClip AlexHerida03;*/

    public AudioClip[] AlexHitClips;
    public AudioClip[] AlexHeridaClips;

    private void Start()
    {
        SFXScore.ignoreListenerPause = true; // Para que no le afecte el timeScale 0.
        
    }

    private void Update()
    {
        SFXScore.volume = volumen;
    }

    public void PlaySFX(AudioClip efecto)
    {
        if (efecto != null)
        {
            // Para que el sonido nos e corte en mitad de la acción creamos un GameObject temporal, mientras dura el sonido.
            GameObject tempGO = new GameObject("TempAudio");
            AudioSource tempAudioSource = tempGO.AddComponent<AudioSource>();
            tempAudioSource.clip = efecto;
            tempAudioSource.volume = volumen;
            tempAudioSource.Play();

            Destroy(tempGO, efecto.length); // Cuando acabe, se destruye el GameObjectTemporal.
        }
    }

    public void PlayRandomAlexHit()
    {
        if (AlexHitClips != null && AlexHitClips.Length > 0)
        {
            int randomIndex = Random.Range(0, AlexHitClips.Length);
            AudioClip randomClip = AlexHitClips[randomIndex];
            PlaySFX(randomClip);
        }
    }

    public void PlayRandomAlexHerida()
    {
        if (AlexHeridaClips != null && AlexHeridaClips.Length > 0)
        {
            int randomIndex = Random.Range(0, AlexHeridaClips.Length);
            AudioClip randomClip = AlexHeridaClips[randomIndex];
            PlaySFX(randomClip);
        }
    }
}
