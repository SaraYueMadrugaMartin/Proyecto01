using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HuecosInventario : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image fotoItem;
    private Inventario inventario;

    public string nombreItem;
    public int cantidad;
    public Sprite sprite;
    public bool estaCompleto;
    public Vector2 coordItem;

    public GameObject panelSeleccion;
    public GameObject panelBotones;
    public bool objetoSeleccionado;

    private void Start()
    {
        inventario = GameObject.Find("Canvas").GetComponent<Inventario>();
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
        panelBotones.SetActive(false);
        objetoSeleccionado = false;
    }

    public void ClickIzquierdo(PointerEventData eventData)
    {
        if(inventario != null && inventario.TieneObjeto(nombreItem))
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

            // Establecemos que la posición del panel de Botones sea la misma que la variable de panelPosicion que hemos calculado antes.
            panelBotones.transform.position = panelPosicion;
            panelBotones.SetActive(true);
        }
    }
}

