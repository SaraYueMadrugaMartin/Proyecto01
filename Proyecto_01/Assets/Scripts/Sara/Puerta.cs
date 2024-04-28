using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puerta : MonoBehaviour
{
    [SerializeField] private PlantillaPuertas puertaAsociada;
    //[SerializeField] private PlantillaLlaves llaveAsociada;
    [SerializeField] private Inventario inventario;

    public static bool jugadorTocando;
    [SerializeField] private GameObject panelMensajeNo;
    [SerializeField] private GameObject panelPregunta;
    //public bool puertaBloqueada = true;

    private void Update()
    {
        if (jugadorTocando && Input.GetKeyDown("e"))
        {
            ActualizarEstadoPuerta();
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
        if (inventario.TieneObjeto("Llave") && CompararIDs())
            puertaAsociada.puertaBloqueada = false;
        else if (!inventario.TieneObjeto("Llave") || !CompararIDs())
            puertaAsociada.puertaBloqueada = true;
        //puertaAsociada.puertaBloqueada = !inventario.TieneObjeto("Llave") || llaveAsociada.ID != puertaAsociada.puertasID;
        Debug.Log("La puerta está: " + (puertaAsociada.puertaBloqueada ? "bloqueada" : "desbloqueada"));
    }

    bool CompararIDs()
    {
        if (puertaAsociada.puertasID == LlavesController.llaveID)
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
    }
}
