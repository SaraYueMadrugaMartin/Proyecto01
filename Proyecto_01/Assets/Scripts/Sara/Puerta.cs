using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puerta : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] public PlantillaPuertas puertaAsociada; // Referenciamos el scriptable para asignar a cada puerta y poder comparar con la ID de las llaves.
    [SerializeField] private Inventario inventario;
    [SerializeField] private GameObject panelMensajeNo;
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private GameObject puertasSinLlave; // Asociamos las puertas sin llave que deben activarse cuando se desbloqueen las puertas con llave.

    [SerializeField] private Vector2 posNueva;

    public PuertasIDControler controladorPuertas;

    public bool jugadorTocando;

    public int idPuerta;

    public bool puertaBloqueada;

    public Collider2D[] puertaColliders;

    private void Awake()
    {
        puertaColliders = GetComponents<Collider2D>();
        panelMensajeNo.SetActive(false);
        puertasSinLlave.SetActive(false); // Las puertas sin llave asociadas a las puertas bloqueadas las iniciamos desactivadas.
    }

    private void Update()
    {
        if (jugadorTocando && Input.GetKeyDown("e"))
        {
            ActualizarEstadoPuerta();
            InteractuarConPuerta();
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
            jugadorTocando = true;
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
                puertaAsociada.puertaBloqueada = false;
            else
                puertaAsociada.puertaBloqueada = true;
        }
        else
            puertaAsociada.puertaBloqueada = true;
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

    public void ActivarPuertaSinLlave() // Cuando se desbloquee la puerta con llave, se activará la puerta sin llave asociada a esta.
    {
        puertasSinLlave.SetActive(true);
        Debug.Log("Se ha activado la puerta sin llave");
    }

    public void CambioPosicionPlayer()
    {
        if(idPuerta != 3)
            StartCoroutine(EsperarCambioPos());
    }

    IEnumerator EsperarCambioPos()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        player.transform.position = posNueva;
    }

    public void DesactivarColliders()
    {
        foreach (Collider2D colliderPuerta in puertaColliders)
            colliderPuerta.enabled = false;
    }

    public bool GetPuertaBloqueada()
    {
        return puertaBloqueada;
    }

    public void SetPuertaBloqueada(bool value)
    {
        puertaBloqueada = value;
        //puertaAsociada.puertaBloqueada = value;
    }
}
