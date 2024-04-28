using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puerta : MonoBehaviour
{
    /*[SerializeField] private GameObject panelMensajeNo;
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private Inventario inventario;

    private PuertasIDControler compararID;

    //public test idLlaveCorrecta;

    public bool puertaBloqueada = true;
    public bool jugadorTocando = false;

    private void Start()
    {
        ActualizarEstadoPuerta();
    }


    private void Update()
    {
        if(jugadorTocando && Input.GetKeyDown("e"))
        {
            ActualizarEstadoPuerta();
            if(puertaBloqueada)
            {
                panelMensajeNo.SetActive(true);
            }
            else
            {
                panelPregunta.SetActive(true);
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
            panelMensajeNo.SetActive(false);
        }
    }

    public void ActualizarEstadoPuerta()
    {
        /*puertaAsociada.puertaBloqueada = !inventario.TieneObjeto("Llave") || llaveAsociada.ID != puertaAsociada.puertasID;
        Debug.Log("La puerta está: " + (puertaAsociada.puertaBloqueada ? "bloqueada" : "desbloqueada"));
    }

    public void UsarLlave()
    {
        gameObject.SetActive(false);
        panelPregunta.SetActive(false);
    }

    public void NoUsarLlave()
    {
        panelPregunta.SetActive(false);
    }*/
}
