using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puerta : MonoBehaviour
{
    [SerializeField] private PlantillaPuertas puertaAsociada;
    [SerializeField] private Inventario inventario;

    public bool jugadorTocando; // "static" lo he eliminado porque sino detectaba otras puertas y se abrían los dos paneles.

    [SerializeField] private GameObject panelMensajeNo;
    [SerializeField] private GameObject panelPregunta;

    private void Update()
    {
        if (jugadorTocando && Input.GetKeyDown("e"))
        {
            if (puertaAsociada.puertaBloqueada)
            {
                panelMensajeNo.SetActive(true);
            }
            else
            {
                panelPregunta.SetActive(true);
            }
        }

        if (PuertasIDControler.destruye)
        {
            DestruirPuerta();
            PuertasIDControler.destruye = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = true;
            ActualizarEstadoPuerta();
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
        if (inventario.TieneObjeto("Llave"))
        {
            int llaveID = inventario.BuscaIDLlave();

            if (CompararIDs(llaveID))
                puertaAsociada.puertaBloqueada = false;
            else
                puertaAsociada.puertaBloqueada = true;
        }
        else
            puertaAsociada.puertaBloqueada = true;

        if (inventario.TieneObjeto("Fusible"))
        {
            int llaveID = inventario.BuscaIDLlave();

            if (CompararIDs(llaveID))
                puertaAsociada.puertaBloqueada = false;
            else
                puertaAsociada.puertaBloqueada = true;
        }
        else
            puertaAsociada.puertaBloqueada = true;

        Debug.Log("La puerta está: " + (puertaAsociada.puertaBloqueada ? "bloqueada" : "desbloqueada"));
    }

    bool CompararIDs(int idLlave)
    {
        if (puertaAsociada.puertasID == idLlave)
        {
            Debug.Log("La llave es correcta.");
            return true;
        }
        else
        {
            Debug.Log("La llave no es la correcta. Necesitas otra.");
            return false;
        }
    }

    public void DestruirPuerta()
    {
        this.gameObject.SetActive(false);
        Debug.Log("Destruye puerta: " + this.gameObject.name);
    }
}
