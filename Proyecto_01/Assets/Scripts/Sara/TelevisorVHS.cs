using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TelevisorVHS : MonoBehaviour
{
    [SerializeField] private GameObject panelNoVHS;
    [SerializeField] private GameObject cintaVHS;
    [SerializeField] private VideoPlayer cinematica;

    private Inventario inventario;
    private bool jugadorTocando = false;

    private void Start()
    {
        inventario = FindObjectOfType<Inventario>();
        panelNoVHS.SetActive(false);
        cintaVHS.SetActive(false);
        cinematica = cintaVHS.GetComponent<VideoPlayer>();
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
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0f;
        cintaVHS.SetActive(true);
        cinematica.Play();
    }
}
