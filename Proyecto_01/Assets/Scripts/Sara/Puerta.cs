using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Puerta : MonoBehaviour
{
    public static bool panelAbierto = false;

    [SerializeField] private GameObject player;
    [SerializeField] public PlantillaPuertas puertaAsociada; // Referenciamos el scriptable para asignar a cada puerta y poder comparar con la ID de las llaves.
    [SerializeField] private Inventario inventario;
    [SerializeField] private GameObject panelMensajeNo;
    [SerializeField] private TextMeshProUGUI textoPuertasCerradas;
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] public GameObject puertasSinLlave; // Asociamos las puertas sin llave que deben activarse cuando se desbloqueen las puertas con llave.

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
            InteractuarConPuerta();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if(panelAbierto && Input.GetKeyDown(KeyCode.Escape))
        {
            panelPregunta.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void InteractuarConPuerta()
    {
        if (inventario.TieneObjeto("Llave") || inventario.TieneObjeto("Fusible"))
        {
            panelPregunta.SetActive(true);
            panelAbierto = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
        }
        else
        {
            panelMensajeNo.SetActive(true);
            puertaBloqueada = true;
            if (idPuerta == 2)
                textoPuertasCerradas.text = "Necesitas una fuente de alimentación para abrir esta puerta";
            else if (idPuerta == 3)
                textoPuertasCerradas.text = "Necsitas una llave para poder quitar estas cadenas";
            else
                textoPuertasCerradas.text = "Necesitas una llave para abrir esta puerta";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            jugadorTocando = true;
            controladorPuertas.SetPuertaActual(this);
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
        List<int> llavesIDs = inventario.BuscaIDsLlaves();

        bool llaveCorrectaEncontrada = false;
        foreach (int idLlave in llavesIDs)
        {
            if (CompararIDs(idLlave))
            {
                llaveCorrectaEncontrada = true;
                puertaAsociada.puertaBloqueada = false;
                inventario.EliminarLlavePorID(idLlave);
                break;
            }
        }

        if (!llaveCorrectaEncontrada)
        {
            puertaAsociada.puertaBloqueada = true;
        }
        
        Debug.Log("La puerta está: " + (puertaAsociada.puertaBloqueada ? "bloqueada" : "desbloqueada"));
    }

    public bool CompararIDs(int idLlave)
    {
        Debug.Log("Estoy comparando la llave con ID: " +  idLlave + " y la puerta con ID: " + puertaAsociada.puertasID);
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
        //if(idPuerta != 3)
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
