using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Cinematicas : MonoBehaviour
{
    public static Cinematicas Instance; // Singleton

    [SerializeField] private GameObject panelPausa;
    [SerializeField] private GameObject cintaVHS;
    private VideoPlayer cinematica;
    private bool pausado = false;
    private bool reproduciendo = false;
    AudioManager audioManager;
    public static bool CineReproduciendo { get; private set; } = false;

    [SerializeField] GameObject puertaGO;
    MirrorPuerta puerta;

    private void Awake()
    {
        // Estructura singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void ReproducirVHS()
    {
        cintaVHS.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cinematica.Play();
        reproduciendo = true;
        CineReproduciendo = true;
        audioManager.Stop("MainTheme");
        cinematica.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        puerta.VHSVisto();
        cintaVHS.SetActive(false);
        reproduciendo = false;
        CineReproduciendo = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pausar()
    {
        Debug.Log("Pausar llamada"); // Mensaje de depuración
        cinematica.Pause();
        panelPausa.SetActive(true);
        Debug.Log("Panel de pausa activado"); // Mensaje de depuración
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pausado = true;
    }

    public void Reanudar()
    {
        Debug.Log("Reanudar llamada"); // Mensaje de depuración
        cinematica.Play();
        panelPausa.SetActive(false);
        Debug.Log("Panel de pausa desactivado"); // Mensaje de depuración
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausado = false;
    }

    public void SaltarCinematica()
    {
        Debug.Log("SaltarCinematica llamada"); // Mensaje de depuración
        cinematica.Pause();
        puerta.VHSVisto();
        cintaVHS.SetActive(false);
        reproduciendo = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
