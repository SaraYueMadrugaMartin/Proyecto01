using UnityEngine;

public class PuertaController : MonoBehaviour
{
    [SerializeField] private GameObject panelMensajeNo;
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private Inventario inventario;

    [SerializeField] private GameObject[] puertas;
    [SerializeField] private GameObject[] llaves;

    private bool[] doorStates;
    public bool jugadorTocando = false;
    private int indicePuerta = 0;

    private void Start()
    {
        doorStates = new bool[puertas.Length];
        for (int i = 0; i < doorStates.Length; i++)
        {
            doorStates[i] = false;
        }
    }

    private void Update()
    {
        if (jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            InteractuarConPuerta();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = true;
            Debug.Log("Estás tocando la puerta");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = false;
            panelMensajeNo.SetActive(false);
            Debug.Log("No estás tocando la puerta");
        }
    }

    public void InteractuarConPuerta()
    {
        if (doorStates[indicePuerta] && !inventario.TieneObjeto(llaves[indicePuerta].name))
        {
            Debug.Log("Esta puerta está cerrada. Necesitas encontrar la llave primero.");
            panelMensajeNo.SetActive(true);
            return;
        }

        puertas[indicePuerta].SetActive(false);
        Debug.Log("Puerta abierta.");

        doorStates[indicePuerta] = true;

        if (inventario.TieneObjeto(llaves[indicePuerta].name))
        {
            panelPregunta.SetActive(true);
        }
    }

    public void UsarLlave()
    {
        Destroy(puertas[indicePuerta]);

        inventario.VaciarHueco(llaves[indicePuerta].name);

        panelPregunta.SetActive(false);
    }

    public void NoUsarLlave()
    {
        panelPregunta.SetActive(false);
    }
}
