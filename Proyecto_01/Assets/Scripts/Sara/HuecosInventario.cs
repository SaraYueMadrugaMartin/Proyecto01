using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class HuecosInventario : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image fotoItem;
    private Inventario inventario;

    public string nombreItem;
    public int cantidad;
    public Sprite sprite;
    public bool estaCompleto;
    //public Vector2 coordItem;

    public GameObject panelSeleccion;
    public Dictionary<string, GameObject> panelesBotones = new Dictionary<string, GameObject>();
    public bool objetoSeleccionado;

    [SerializeField] private GameObject panelBotonesDocumento;
    [SerializeField] private GameObject panelBotonesArma;
    [SerializeField] private GameObject panelBotonesBotiquin;
    [SerializeField] private GameObject panelBotonesTinta;

    private void Start()
    {
        inventario = GameObject.Find("Canvas").GetComponent<Inventario>();

        panelesBotones.Add("Documento", panelBotonesDocumento);
        panelesBotones.Add("Bate", panelBotonesArma);
        panelesBotones.Add("Botiquin", panelBotonesBotiquin);
        panelesBotones.Add("Tinta", panelBotonesTinta);
    }

    public void AñadirObjeto(string nombreItem, Sprite sprite)
    {
        this.nombreItem = nombreItem;
        this.sprite = sprite;
        estaCompleto = true;
        fotoItem.sprite = sprite;
    }

    public void VaciarHueco()
    {
        nombreItem = "";
        cantidad = 0;
        sprite = null;
        estaCompleto = false;
        fotoItem.sprite = null;
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
        panelSeleccion.SetActive(false);
        foreach (var panelBoton in panelesBotones.Values)
        {
            panelBoton.SetActive(false);
        }
        objetoSeleccionado = false;
    }

    public void ClickIzquierdo(PointerEventData eventData)
    {
        inventario.DeseleccionarObjetos();
        panelSeleccion.SetActive(true);
        objetoSeleccionado = true;

        // Expresión de Unity que convierte una posición de coordenadas de pantalla a coordenadas en el mundo.
        Vector3 posicionRaton = Camera.main.ScreenToWorldPoint(eventData.position);

        // Creamos la variable de posicionHuecos para obtener sus posiciones en el mundo (sus coordenadas).
        Vector3 posicionHuecos = transform.position;

        // Creamos la variable de panelPosicion y la igualamos a la posicionHuecos, así las coordenadas serán las mismas.
        Vector3 panelPosicion = posicionHuecos;

        // Verificar si existe un panel para este tipo de objeto y activarlo
        if (panelesBotones.ContainsKey(nombreItem))
        {
            panelesBotones[nombreItem].SetActive(true);
            panelesBotones[nombreItem].transform.position = panelPosicion;
        }
    }
}
