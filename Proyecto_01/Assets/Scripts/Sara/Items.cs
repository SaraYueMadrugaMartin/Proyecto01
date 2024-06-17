using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] public string nombreItem;
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject panelTutoLinterna;
    private bool tutoAbierto = false;
    private Inventario inventario;
    private bool cogerObjeto = false;

    public IDsItems idsItems;

    private PanelesInteracciones panelesInteracciones, panelAvisoInventarioCompleto;

    public Vector2 posicionInicial;
    public bool objetoRecogido = false;
    public SpriteRenderer[] spriteRenderers;
    public Collider2D[] collidersItems;

    private List<bool> objetoHaSidoRecogido;

    SFXManager sfxManager;

    void Start()
    {
        collidersItems = GetComponents<Collider2D>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        sfxManager = FindObjectOfType<SFXManager>();
        inventario = GameObject.Find("Canvas").GetComponent<Inventario>();
        panelesInteracciones = FindObjectOfType<PanelesInteracciones>();
        panelAvisoInventarioCompleto = FindObjectOfType<PanelesInteracciones>();
        posicionInicial = transform.position;
        objetoHaSidoRecogido = new List<bool>();
    }

    private void Update()
    {
        if (cogerObjeto && Input.GetKeyDown("e"))
        {
            if (!inventario.InventarioCompleto())
            {
                if (nombreItem == "Llave")
                {
                    LlavesController llave = GetComponent<LlavesController>();
                    int llaveID = llave.ObtenerID();
                    inventario.AñadirObjeto(nombreItem, sprite, llaveID); // Pasar el ID de la llave
                    Debug.Log("El ID de la llave es: " + llaveID);
                    if (llaveID == 222)
                    {
                        Debug.Log("Es fusible");
                        ApagaLuces();
                    }
                }
                else
                {
                    inventario.AñadirObjeto(nombreItem, sprite);
                }

                panelesInteracciones.AparecerPanelInteraccion(nombreItem);
                
                ObjetoRecogido(true);
                sfxManager.PlaySFX(sfxManager.clipsDeAudio[1]);

                if (nombreItem == "Municion")
                    Player.municion += 12;
                else if (nombreItem == "Linterna")
                {
                    Time.timeScale = 0f;
                    panelTutoLinterna.SetActive(true);
                    tutoAbierto = true;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }

                DesactivarItem();
            }
            else
            {
                panelAvisoInventarioCompleto.AparecerPanelAvisoInventarioCompleto();
                Debug.Log("¡El inventario está lleno! No se puede recoger el objeto.");
            }
        }
        if (tutoAbierto && Input.GetKeyDown(KeyCode.Escape))
        {
            BotonSalirTutoLinterna();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            cogerObjeto = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            cogerObjeto = false;
        }
    }

    public void BotonSalirTutoLinterna()
    {
        Time.timeScale = 1f;
        panelTutoLinterna.SetActive(false);
        tutoAbierto = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ObjetoRecogido(bool value)
    {
        objetoHaSidoRecogido.Add(value);
    }

    public bool GetObjetoRecogido()
    {
        return objetoRecogido;
    }

    public void SetObjetoRecogido(bool value)
    {
        objetoRecogido = value;
    }

    /*public Sprite GetSpriteItems()
    {
        return sprite;
    }*/

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    /*public SpriteRenderer[] GetSpriteRenderers()
    {
        return spriteRenderers;
    }*/

    public void MoverYActivar(Vector3 nuevaPosicion)
    {
        transform.position = nuevaPosicion;
        ActivarItem();
        Debug.Log("Objeto movido y activado en la posición: " + nuevaPosicion);
    }

    public void DesactivarItem()
    {
        objetoRecogido = true;

        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        if (spriteRenderers != null)
        {
            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                renderer.enabled = false;
            }
        }

        foreach (Collider2D colliderItem in collidersItems)
            colliderItem.enabled = false;
    }

    private void ActivarItem()
    {
        objetoRecogido = false;

        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        if (spriteRenderers != null)
        {
            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                renderer.enabled = true;
            }
        }

        foreach (Collider2D colliderItem in collidersItems)
            colliderItem.enabled = true;
    }

    // Método para apagar todas las luces al quitar el fusible
    private void ApagaLuces()
    {
        LucesTodas luces = GameObject.Find("GlobalLight").GetComponent<LucesTodas>();
        luces.CambiaEstadoLuces();
    }
}
