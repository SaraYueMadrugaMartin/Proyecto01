using UnityEngine;

public class PuertaController : MonoBehaviour
{
    [SerializeField] private GameObject panelMensajeNo;
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private Inventario inventario;

    [SerializeField] private GameObject[] puertas;
    [SerializeField] private GameObject[] llaves;

    private bool[] doorStates; // Array para contener el estado de cada puerta
    public bool jugadorTocando = false;
    private int indicePuerta = 0; // Índice de la puerta con la que se interactúa

    private void Start()
    {
        // Inicializar el array de estadosPuertas
        doorStates = new bool[puertas.Length];
        for (int i = 0; i < doorStates.Length; i++)
        {
            doorStates[i] = false; // Todas las puertas comienzan cerradas
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
        // Comprobar si la puerta está cerrada y si se ha recolectado la llave correspondiente
        if (doorStates[indicePuerta] && !inventario.TieneObjeto(llaves[indicePuerta].name))
        {
            Debug.Log("Esta puerta está cerrada. Necesitas encontrar la llave primero.");
            panelMensajeNo.SetActive(true);
            return; // Salir del método si la puerta está cerrada y la llave no ha sido recolectada
        }

        // Abrir la puerta
        puertas[indicePuerta].SetActive(false);
        Debug.Log("Puerta abierta.");

        // Establecer el estado de la puerta como abierta
        doorStates[indicePuerta] = true;

        // Mostrar panel de pregunta si se tiene la llave
        if (inventario.TieneObjeto(llaves[indicePuerta].name))
        {
            panelPregunta.SetActive(true);
        }
    }

    public void UsarLlave()
    {
        // Destruir la puerta con la que se está interactuando
        Destroy(puertas[indicePuerta]);

        // Eliminar la llave del inventario
        inventario.VaciarHueco(llaves[indicePuerta].name);

        // Ocultar el panel de pregunta
        panelPregunta.SetActive(false);
    }

    public void NoUsarLlave()
    {
        panelPregunta.SetActive(false);
    }
}
