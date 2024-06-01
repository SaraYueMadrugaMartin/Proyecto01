using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelAjustes;
    [SerializeField] private GameObject panelCreditos;

    SFXManager sfxManager;

    private void Start()
    {
        sfxManager = SFXManager.instance;
    }

    public void ComenzarJuego()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        int siguienteEscena = escenaActual + 1;
        if (siguienteEscena == 2)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(siguienteEscena);

        sfxManager.PlaySFX(sfxManager.seleccionBoton01);
        sfxManager.StopMusic();
    }

    public void IrAjustes()
    {
        gameObject.SetActive(false);
        panelAjustes.SetActive(true);
        sfxManager.PlaySFX(sfxManager.seleccionBoton02);
    }

    public void IrCreditos()
    {
        gameObject.SetActive(false);
        panelCreditos.SetActive(true);
        sfxManager.PlaySFX(sfxManager.seleccionBoton02);
    }

    public void VolverAtras()
    {
        gameObject.SetActive(true);
        panelAjustes.SetActive(false);
        panelCreditos.SetActive(false);
        sfxManager.PlaySFX(sfxManager.seleccionBoton03);
    }

    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Has salido del juego.");
        sfxManager.PlaySFX(sfxManager.seleccionBoton01);
    }
}
