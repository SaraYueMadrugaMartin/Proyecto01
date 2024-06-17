using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelAjustes;
    [SerializeField] private GameObject panelCreditos;

    private AudioManager audioManager;

    SFXManager sfxManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        sfxManager = FindObjectOfType<SFXManager>();
    }

    public void ComenzarJuego()
    {
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[0]);

        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        int siguienteEscena = escenaActual + 1;
        if (siguienteEscena == 2)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(siguienteEscena);

        audioManager.Stop("MainTheme");
    }

    public void CargarPartida()
    {
        GameManager.instance.ReiniciarEscena();
    }


    public void IrAjustes()
    {
        gameObject.SetActive(false);
        panelAjustes.SetActive(true);
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[1]);
    }

    public void IrCreditos()
    {
        gameObject.SetActive(false);
        panelCreditos.SetActive(true);
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[1]);
    }

    public void VolverAtras()
    {
        gameObject.SetActive(true);
        panelAjustes.SetActive(false);
        panelCreditos.SetActive(false);
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[2]);
    }

    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Has salido del juego.");
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[0]);
    }
}
