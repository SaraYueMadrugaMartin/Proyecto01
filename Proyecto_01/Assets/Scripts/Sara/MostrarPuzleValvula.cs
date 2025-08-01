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
    [SerializeField] private FadeAnimation fadeAnimation;

    SFXManager sfxManager;

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
        sfxManager = FindObjectOfType<SFXManager>();

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
            sfxManager.PlaySFX(sfxManager.clipsDeAudio[11]);
            if (panelSiguientePaso != null)
            {
                panelSiguientePaso.SetActive(true);
            }
            Debug.Log("Has montado la v�lvula completa");
            StartCoroutine(SiguientePaso());
        }
    }

    private IEnumerator SiguientePaso()
    {
        yield return new WaitForSecondsRealtime(2f);
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
        fadeAnimation.FadeOutNivel();
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[19]);
        yield return new WaitForSecondsRealtime(1f);
        puzleGirarValvula.SetActive(false);
        puertaLaberinto.AbrirPuertaLaberinto();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SalirPanelMoverValvula()
    {
        puzleMoverPiezasValvula.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SalirPanelGirarValvula()
    {
        puzleGirarValvula.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
