using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TelevisorVHS : MonoBehaviour
{
    [SerializeField] private GameObject panelNoVHS;
    [SerializeField] private GameObject cintaVHS;
    [SerializeField] private VideoPlayer cinematica;
    AudioManager audioManager;
    private float duracion;

    private Inventario inventario;
    private bool jugadorTocando = false;

    [SerializeField] GameObject puertaGO;
    MirrorPuerta puerta;

    private void Start()
    {
        audioManager = AudioManager.instance;
        inventario = FindObjectOfType<Inventario>();
        panelNoVHS.SetActive(false);
        puerta = puertaGO.GetComponent<MirrorPuerta>();
        cinematica = cintaVHS.GetComponent<VideoPlayer>();
        duracion = (float)cinematica.clip.length;
        cintaVHS.SetActive(false);
    }

    private void Update()
    {
        if (jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            if (inventario != null)
            {
                if (inventario.TieneObjeto("VHS"))
                {
                    StartCoroutine(ReproducirVHS());
                    inventario.VaciarHueco("VHS");
                }
                else
                {
                    panelNoVHS.SetActive(true);
                    Debug.Log("No tienes ninguna cinta para reproducir.");
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = false;
            panelNoVHS.SetActive(false);
        }
    }

    IEnumerator ReproducirVHS()
    {
        Time.timeScale = 0f;
        cintaVHS.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cinematica.Prepare();
        while (!cinematica.isPrepared)
        {
            yield return null;
        }

        cinematica.Play();
        audioManager.Stop("MainTheme");
        StartCoroutine(PararVHS());
    }

    IEnumerator PararVHS()
    {
        yield return new WaitForSecondsRealtime(duracion);
        cinematica.Pause();
        puerta.VHSVisto();
        cintaVHS.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
    }
}

