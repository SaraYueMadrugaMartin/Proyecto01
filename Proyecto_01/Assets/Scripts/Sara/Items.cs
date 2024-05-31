using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] public string nombreItem;
    [SerializeField] private Sprite sprite;
    private Inventario inventario;
    private bool cogerObjeto = false;

    public IDsItems idsItems;

    private PanelesInteracciones panelesInteracciones, panelAvisoInventarioCompleto;

    public Vector2 posicionInicial;
    public bool objetoRecogido = false;
    private SpriteRenderer[] spriteRenderers;

    private List<bool> objetoHaSidoRecogido;

    void Start()
    {
        inventario = GameObject.Find("Canvas").GetComponent<Inventario>();
        panelesInteracciones = FindObjectOfType<PanelesInteracciones>();
        panelAvisoInventarioCompleto = FindObjectOfType<PanelesInteracciones>();
        posicionInicial = transform.position;
        Transform[] hijosTransform = GetComponentsInChildren<Transform>(includeInactive: true);
        List<GameObject> hijosGameObjects = new List<GameObject>();
        objetoHaSidoRecogido = new List<bool>();

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (Transform hijoTransform in hijosTransform)
        {
            if (hijoTransform.gameObject != this.gameObject)
            {
                hijosGameObjects.Add(hijoTransform.gameObject);
            }
        }
    }

    private void Update()
    {
        if (cogerObjeto && Input.GetKeyDown("e"))
        {
            if (!inventario.InventarioCompleto())
            {
                inventario.AñadirObjeto(nombreItem, sprite);
                panelesInteracciones.AparecerPanelInteraccion(nombreItem);
                objetoRecogido = true;
                ObjetoRecogido(true);

                if (nombreItem == "Llave")
                {
                    LlavesController llave = GetComponent<LlavesController>();
                    int llaveID = llave.ObtenerID();
                    Debug.Log("El ID de la llave es: " + llaveID);
                    inventario.RecibeIDLlave(llaveID);
                }
                else if (nombreItem == "Municion")
                    Player.municion += 12;

                DesactivarItem();
            }
            else
            {
                panelAvisoInventarioCompleto.AparecerPanelAvisoInventarioCompleto();
                Debug.Log("¡El inventario está lleno! No se puede recoger el objeto.");
            }
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

    public void ObjetoRecogido(bool value)
    {
        objetoHaSidoRecogido.Add(value);
    }

    public bool GetObjetoRecogido()
    {
        return objetoRecogido;
    }

    public Sprite GetSpriteItems()
    {
        return sprite;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    public SpriteRenderer[] GetSpriteRenderers()
    {
        return spriteRenderers;
    }

    public void MoverYActivar(Vector3 nuevaPosicion)
    {
        transform.position = nuevaPosicion;
        ActivarItem();
        Debug.Log("Objeto movido y activado en la posición: " + nuevaPosicion);
    }

    public void DesactivarItem()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        if (spriteRenderers != null)
        {
            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                renderer.enabled = false;
            }
        }

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        if(colliders != null)
        {
            foreach(Collider2D collider in colliders)
            {
                collider.enabled = false;
            }
        }
    }

    private void ActivarItem()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        if (spriteRenderers != null)
        {
            foreach (SpriteRenderer renderer in spriteRenderers)
            {
                renderer.enabled = true;
            }
        }

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        if (colliders != null)
        {
            foreach (Collider2D collider in colliders)
            {
                collider.enabled = true;
            }
        }
    }

    public void SetIDsItem(int newID)
    {
        if (idsItems != null)
        {
            idsItems.SetIDsItem(newID);
        }
    }

    // Método para obtener el ID del item
    public int GetIDsItem()
    {
        if (idsItems != null)
        {
            return idsItems.GetIDsItem();
        }
        else
        {
            Debug.LogError("idsItems es null en el objeto: " + nombreItem);
            return -1; // Valor por defecto o código de error
        }
    }
}
