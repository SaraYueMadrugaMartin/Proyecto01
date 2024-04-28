using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertasIDControler : MonoBehaviour
{
    /*[SerializeField] private PlantillaPuertas puertaAsociada;
    [SerializeField] private PlantillaLlaves llaveAsociada;

    [SerializeField] private GameObject panelMensajeNo;
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private Inventario inventario;

    //private PuertasIDControler compararID;

    //public test idLlaveCorrecta;

    public bool puertaBloqueada = true;
    // public bool jugadorTocando = false;

    private void Start()
    {
        ActualizarEstadoPuerta();
    }


    private void Update()
    {
        if (Puerta.jugadorTocando && Input.GetKeyDown("e"))
        {
            ActualizarEstadoPuerta();
            if (puertaBloqueada)
            {
                panelMensajeNo.SetActive(true);
            }
            else
            {
                panelPregunta.SetActive(true);
            }
        }
    }
    public void CompararIDs()
    {
        if (puertaAsociada.puertasID == llaveAsociada.ID)
        {
            Debug.Log("La llave es correcta.");
        }
        else
        {
            Debug.Log("La llave no es la correcta. Necesitas otra.");
        }
    }

    public void ActualizarEstadoPuerta()
    {
        //puertaAsociada.puertaBloqueada = !inventario.TieneObjeto("Llave") || llaveAsociada.ID != puertaAsociada.puertasID;
        //Debug.Log("La puerta está: " + (puertaAsociada.puertaBloqueada ? "bloqueada" : "desbloqueada"));
    }*/
    [SerializeField] private GameObject panelPregunta;
    public static bool destruye = false;

    public void UsarLlave()
    {
        //gameObject.SetActive(false);
        destruye = true;
        panelPregunta.SetActive(false);
    }

    public void NoUsarLlave()
    {
        panelPregunta.SetActive(false);
    }
}
