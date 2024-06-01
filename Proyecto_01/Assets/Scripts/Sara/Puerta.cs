using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puerta : MonoBehaviour
{
    [SerializeField] public PlantillaPuertas puertaAsociada; // Referenciamos el scriptable para asignar a cada puerta y poder comparar con la ID de las llaves.
    [SerializeField] private Inventario inventario;
    //[SerializeField] private TutorialGeneralController tutoController;
    [SerializeField] private GameObject panelMensajeNo;
    [SerializeField] private GameObject panelPregunta;
    //[SerializeField] private FadeAnimation fadeAnimation;

    public PuertasIDControler controladorPuertas;

    public bool jugadorTocando;

    public int idPuerta;

    public bool puertaBloqueada;

    public Collider2D[] puertaColliders;

    private void Awake()
    {
        puertaColliders = GetComponents<Collider2D>();
        panelMensajeNo.SetActive(false);
        //puertaSL.SetActive(false);
    }

    private void Update()
    {
        if (jugadorTocando && Input.GetKeyDown("e"))
        {
            InteractuarConPuerta();
            //tutoController.ActivarPanelTuto01();

            //RecibeIDPuerta(idPuerta);
            Debug.Log("Esta es la puerta con ID: " + idPuerta);
        }
    }

    public void InteractuarConPuerta()
    {
        if (puertaAsociada.puertaBloqueada)
        {
            panelMensajeNo.SetActive(true);
            puertaBloqueada = true;
        }
        else
        {
            panelPregunta.SetActive(true);
            puertaBloqueada = false;
            controladorPuertas.NotificarDestruccionPuerta(this);
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
        if (inventario.TieneObjeto("Llave") || inventario.TieneObjeto("Fusible"))
        {
            int llaveID = inventario.BuscaIDLlave();

            if (CompararIDs(llaveID))
            {
                puertaAsociada.puertaBloqueada = false;
            }
            else
            {
                puertaAsociada.puertaBloqueada = true;
            }               
        }
        else
        {
            puertaAsociada.puertaBloqueada = true;
        }
        Debug.Log("La puerta está: " + (puertaAsociada.puertaBloqueada ? "bloqueada" : "desbloqueada"));
    }

    public bool CompararIDs(int idLlave)
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

    public void DesactivarColliders()
    {
        foreach (Collider2D colliderPuerta in puertaColliders)
        {
            colliderPuerta.enabled = false;
        }
    }

    /*public void DestruirPuerta()
    {
        // Obtener el ID de la puerta actual
        int idActual = idPuerta;

        // Comparar el ID de la puerta actual con el ID almacenado en el script
        if (idActual == idPuerta)
        {
            // Desactivar solo esta puerta
            gameObject.SetActive(false);
            Debug.Log("Destruye puerta: " + gameObject.name);
        }    
    }*/

    public bool GetPuertaBloqueada()
    {
        return puertaBloqueada;
    }

    public void SetPuertaBloqueada(bool value)
    {
        puertaBloqueada = value;
        //puertaAsociada.puertaBloqueada = value;
    }

    /*public int RecibeIDPuerta(int puertaAsociada) // DE MOMENTO NO SIRVE DE NADA
    {
        idPuerta = puertaAsociada;
        return idPuerta;
    }*/
}
