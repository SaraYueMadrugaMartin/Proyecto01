using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TelevisorVHS : MonoBehaviour
{
    [SerializeField] private GameObject panelNoVHS;
    [SerializeField] private GameObject cintaVHS;
    [SerializeField] private VideoPlayer cinematica;
    private float duracion;

    private Inventario inventario;
    private bool jugadorTocando = false;

    [SerializeField] GameObject puertaGO;
    MirrorPuerta puerta;

    private void Start()
    {
        inventario = FindObjectOfType<Inventario>();
        panelNoVHS.SetActive(false);
        cinematica = cintaVHS.GetComponent<VideoPlayer>();
        duracion = (float)cinematica.clip.length;
        cintaVHS.SetActive(false);
        puerta = puertaGO.GetComponent<MirrorPuerta>();
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
        puerta.VHSVisto();

        cinematica.Prepare();
        while (!cinematica.isPrepared)
        {
            yield return null;
        }

        cinematica.Play();
        StartCoroutine(PararVHS());
    }

    IEnumerator PararVHS()
    {
        yield return new WaitForSecondsRealtime(duracion);
        cinematica.Pause();
        cintaVHS.SetActive(false);
        Time.timeScale = 1f;
    }
}

