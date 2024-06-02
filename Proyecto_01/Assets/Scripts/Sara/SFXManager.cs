using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    //public static SFXManager instance;

    [SerializeField] public AudioSource SFXScore;

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
    public AudioClip abrirInventario;
    public AudioClip cerrarInventario;
    public AudioClip seleccionarObjetos;

    private void Start()
    {
        SFXScore.ignoreListenerPause = true; // Para que no le afecte el timeScale 0.
    }

    public void PlaySFX(AudioClip efecto)
    {
        SFXScore.PlayOneShot(efecto);
    }
}
