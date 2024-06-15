using System.Collections;
using UnityEngine;

public class MostrarPuzleValvula : MonoBehaviour
{
    public static MostrarPuzleValvula Instance { get; private set; }

    public bool cabezaColocada = false;
    public bool cuerpoColocado = false;

    [SerializeField] private GameObject panelSiguientePaso;
    [SerializeField] private ValvulaRotar rotarValvula;
    [SerializeField] private GameObject puzleMoverPiezasValvula;
    [SerializeField] private GameObject puzleGirarValvula;
    [SerializeField] private PuertaLaberinto puertaLaberinto;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (panelSiguientePaso != null)
        {
            panelSiguientePaso.SetActive(false);
        }
    }

    public void ColocarCabeza()
    {
        cabezaColocada = true;
        ComprobarEstado();
    }

    public void ColocarCuerpo()
    {
        cuerpoColocado = true;
        ComprobarEstado();
    }

    private void ComprobarEstado()
    {
        if (cabezaColocada && cuerpoColocado)
        {
            // Sonido de piezas encajando
            if (panelSiguientePaso != null)
            {
                panelSiguientePaso.SetActive(true);
            }
            Debug.Log("Has montado la válvula completa");
            StartCoroutine(SiguientePaso());
        }
    }

    private IEnumerator SiguientePaso()
    {
        yield return new WaitForSecondsRealtime(5f);
        if (panelSiguientePaso != null)
        {
            puzleMoverPiezasValvula.SetActive(false);
            panelSiguientePaso.SetActive(false);
            puzleGirarValvula.SetActive(true);
        }
    }

    public void GiroCompletado()
    {
        StartCoroutine(DesactivarPanelGirarValvula());
    }

    private IEnumerator DesactivarPanelGirarValvula()
    {
        puertaLaberinto.AbrirPuertaLaberinto();

        yield return new WaitForSecondsRealtime(1f);
        // Sonido de giro de válvula
        puzleGirarValvula.SetActive(false);
    }
}
