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

    void Start()
    {
        inventario = GameObject.Find("Canvas").GetComponent<Inventario>();
        panelesInteracciones = FindObjectOfType<PanelesInteracciones>();
        panelAvisoInventarioCompleto = FindObjectOfType<PanelesInteracciones>();
        posicionInicial = transform.position;
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

                gameObject.SetActive(false);
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
}
