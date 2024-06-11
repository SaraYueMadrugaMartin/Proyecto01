using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TelevisorVHS : MonoBehaviour
{
    [SerializeField] private GameObject panelNoVHS;

    private Inventario inventario;
    private bool jugadorTocando = false;


    private void Start()
    {
        inventario = FindObjectOfType<Inventario>();
        panelNoVHS.SetActive(false);
    }

    private void Update()
    {
        if (jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            if (inventario != null)
            {
                if (inventario.TieneObjeto("VHS"))
                {
                    Cinematicas.Instance.Reproducir(0);
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
}
