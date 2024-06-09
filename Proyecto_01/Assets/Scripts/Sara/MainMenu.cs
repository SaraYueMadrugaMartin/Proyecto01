using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelAjustes;
    [SerializeField] private GameObject panelCreditos;

    AudioMainMenu audioMainMenu;

    private void Start()
    {
        audioMainMenu = FindObjectOfType<AudioMainMenu>();
    }

    public void ComenzarJuego()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        int siguienteEscena = escenaActual + 1;
        if (siguienteEscena == 2)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(siguienteEscena);

        audioMainMenu.PlaySFX(audioMainMenu.seleccionBoton01);
        audioMainMenu.StopMusic();
    }

    public void CargarPartida()
    {
        SceneManager.LoadScene(4);
        GameManager.instance.ReiniciarEscena();
    }


    public void IrAjustes()
    {
        gameObject.SetActive(false);
        panelAjustes.SetActive(true);
        audioMainMenu.PlaySFX(audioMainMenu.seleccionBoton02);
    }

    public void IrCreditos()
    {
        gameObject.SetActive(false);
        panelCreditos.SetActive(true);
        audioMainMenu.PlaySFX(audioMainMenu.seleccionBoton02);
    }

    public void VolverAtras()
    {
        gameObject.SetActive(true);
        panelAjustes.SetActive(false);
        panelCreditos.SetActive(false);
        audioMainMenu.PlaySFX(audioMainMenu.seleccionBoton03);
    }

    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Has salido del juego.");
        audioMainMenu.PlaySFX(audioMainMenu.seleccionBoton01);
    }
}
