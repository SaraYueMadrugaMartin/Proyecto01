using UnityEngine;

public class PuertaController : MonoBehaviour
{
    [SerializeField] private GameObject panelMensajeNo;
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private GameObject llaveClave;

    public bool puertaBloqueada = true;
    public bool jugadorTocando = false;

    private void Start()
    {

    }

    private void Update()
    {
        if(jugadorTocando && Input.GetKeyDown("e"))
        {
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
            Debug.Log("Estas tocando la puerta");
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
        if (llaveClave != null && !llaveClave.activeSelf)
        {
            puertaBloqueada = false;
            Debug.Log("La puerta está desbloqueada.");
        }
    }

    public void UsarLlave()
    {
        gameObject.SetActive(false);
        panelPregunta.SetActive(false);
    }

    public void NoUsarLlave()
    {
        panelPregunta.SetActive(false);
    }
}
