using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] public string nombreItem;
    [SerializeField] private Sprite sprite;
    private Inventario inventario;
    private bool cogerObjeto = false;

    private PanelesInteracciones panelesInteracciones, panelAvisoInventarioCompleto;

    public Vector2 posicionInicial;
    public bool objetoRecogido = false;
    public Items ultimoObjetoRecogido;

    private Collider2D itemCollider;
    private GameObject[] propiedadesHijosItems;

    void Start()
    {
        inventario = GameObject.Find("Canvas").GetComponent<Inventario>();
        panelesInteracciones = FindObjectOfType<PanelesInteracciones>();
        panelAvisoInventarioCompleto = FindObjectOfType<PanelesInteracciones>();
        posicionInicial = transform.position;
        itemCollider = GetComponent<Collider2D>();
        Transform[] hijosTransform = GetComponentsInChildren<Transform>(includeInactive: true);
        List<GameObject> hijosGameObjects = new List<GameObject>();

        foreach (Transform hijoTransform in hijosTransform)
        {
            if (hijoTransform.gameObject != this.gameObject) // Excluir el propio objeto
            {
                hijosGameObjects.Add(hijoTransform.gameObject);
            }
        }

        propiedadesHijosItems = hijosGameObjects.ToArray();
        //spriteItem = transform.Find("Sprite").gameObject;
        //RegistrarObjeto();
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
                //gameObject.SetActive(false);
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

    public void MoverYActivar(Vector3 nuevaPosicion)
    {
        transform.position = nuevaPosicion;
        ActivarItem();
        Debug.Log("Objeto movido y activado en la posición: " + nuevaPosicion);
    }

    private void DesactivarItem()
    {
        itemCollider.enabled = false;
        if (propiedadesHijosItems != null)
        {
            foreach (GameObject hijo in propiedadesHijosItems)
            {
                hijo.SetActive(false);
            }
        }
    }

    private void ActivarItem()
    {
        itemCollider.enabled = true;
        if (propiedadesHijosItems != null)
        {
            foreach (GameObject hijo in propiedadesHijosItems)
            {
                hijo.SetActive(true);
            }
        }
        gameObject.SetActive(true);
    }
}
