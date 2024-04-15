using UnityEngine;

public class Interaccion : MonoBehaviour
{
    [SerializeField] private GameObject llave01;
    [SerializeField] private GameObject puerta;

    private bool cogerObjeto;
    private GameObject objetoContacto;
    private bool tieneLlave01 = false;

    private void Start()
    {
        // Al inicio, asegúrate de que la llave no esté presente
        if (llave01 != null)
            tieneLlave01 = false;
    }

    void Update()
    {
        if (cogerObjeto && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();

            if (objetoContacto != null && objetoContacto.tag == "Llave")
            {
                tieneLlave01 = true;
            }
        }

        if (tieneLlave01 && objetoContacto != null && objetoContacto.tag == "Puerta" && Input.GetKeyDown(KeyCode.E))
        {
            PuertaAbierta();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interaccion") || other.CompareTag("Llave") || other.CompareTag("Puerta"))
        {
            Debug.Log("¿Coger el objeto?");
            cogerObjeto = true;
            objetoContacto = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interaccion") || other.CompareTag("Llave") || other.CompareTag("Puerta"))
        {
            cogerObjeto = false;
            objetoContacto = null;
            Debug.Log("No hay objetos cerca para interactuar");
        }
    }

    private void PickUp()
    {
        if (objetoContacto != null)
        {
            Destroy(objetoContacto);
            Debug.Log("Has recogido un objeto");
        }
    }

    private void PuertaAbierta()
    {
        // Si el jugador tiene la llave, abre la puerta
        if (tieneLlave01 && puerta != null)
        {
            Destroy(puerta);
            Debug.Log("Puerta abierta");
        }
        else
        {
            Debug.Log("Necesitas la llave para abrir esta puerta");
        }
    }
}
