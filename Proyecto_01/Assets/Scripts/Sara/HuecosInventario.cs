using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.VFX;

public class HuecosInventario : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image fotoItem;
    private Inventario inventario;

    public string nombreItem;
    public int cantidad;
    public Sprite sprite;
    public bool estaCompleto;
    public int idLlave; // ID de la llave
    public GameObject panelSeleccion;
    public Dictionary<string, GameObject> panelesBotones = new Dictionary<string, GameObject>();
    public bool objetoSeleccionado;

    SFXManager sfxManager;

    [SerializeField] private GameObject panelBotonesLlave;
    [SerializeField] private GameObject panelBotonesBate;
    [SerializeField] private GameObject panelBotonesPistola;
    [SerializeField] private GameObject panelBotonesBotiquin;
    [SerializeField] private GameObject panelBotonesTinta;
    [SerializeField] private GameObject panelBotonesLinterna;
    [SerializeField] private GameObject panelBotonesMunicion;
    [SerializeField] private GameObject panelBotonesMoneda;
    [SerializeField] private GameObject panelBotonesVHS;
    [SerializeField] private GameObject panelBotonesValvula;
    [SerializeField] private GameObject panelBotonesFusible;

    private void Awake()
    {
        fotoItem.enabled = false;
    }

    private void Start()
    {
        inventario = GameObject.Find("Canvas").GetComponent<Inventario>();

        sfxManager = FindObjectOfType<SFXManager>();

        panelesBotones.Add("Llave", panelBotonesLlave);
        panelesBotones.Add("Bate", panelBotonesBate);
        panelesBotones.Add("Pistola", panelBotonesPistola);
        panelesBotones.Add("Botiquin", panelBotonesBotiquin);
        panelesBotones.Add("Tinta", panelBotonesTinta);
        panelesBotones.Add("Linterna", panelBotonesLinterna);
        panelesBotones.Add("Municion", panelBotonesMunicion);
        panelesBotones.Add("Moneda", panelBotonesMoneda);
        panelesBotones.Add("VHS", panelBotonesVHS);
        panelesBotones.Add("Valvula", panelBotonesValvula);
        panelesBotones.Add("Fusible", panelBotonesFusible);
    }

    public void AñadirObjeto(string nombreItem, Sprite sprite, int idLlave)
    {
        this.nombreItem = nombreItem;
        this.sprite = sprite;
        this.idLlave = idLlave; // Asignar el ID de la llave
        estaCompleto = true;
        fotoItem.sprite = sprite;
        fotoItem.enabled = true;
    }

    public void VaciarHueco()
    {
        nombreItem = "";
        sprite = null;
        idLlave = -1; // Resetear el ID de la llave
        estaCompleto = false;
        fotoItem.sprite = null;
        fotoItem.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ClickIzquierdo(eventData);
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ClickDerecho();
        }
    }

    public void ClickDerecho()
    {
        inventario.DeseleccionarObjetos();
        panelSeleccion.SetActive(false);
        foreach (var panelBoton in panelesBotones.Values)
        {
            panelBoton.SetActive(false);
        }
        objetoSeleccionado = false;
    }

    public void ClickIzquierdo(PointerEventData eventData)
    {
        sfxManager.PlaySFX(sfxManager.clipsDeAudio[10]);
        inventario.DeseleccionarObjetos();
        panelSeleccion.SetActive(true);
        objetoSeleccionado = true;

        Vector3 posicionRaton = Camera.main.ScreenToWorldPoint(eventData.position);

        Vector3 posicionHuecos = transform.position;

        Vector3 panelPosicion = posicionHuecos;

        if (panelesBotones.ContainsKey(nombreItem))
        {
            panelesBotones[nombreItem].SetActive(true);
            panelesBotones[nombreItem].transform.position = panelPosicion;
        }
    }
}
