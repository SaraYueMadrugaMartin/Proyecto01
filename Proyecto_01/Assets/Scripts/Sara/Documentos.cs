using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Documentos : MonoBehaviour
{
    [SerializeField] private GameObject panelAsociado;

    private Inventario inventario;

    public bool jugadorTocando = false;

    private void Start()
    {
        inventario = FindObjectOfType<Inventario>();
    }

    void Update()
    {
        if (jugadorTocando && Input.GetKeyDown("e"))
        {
            if (!inventario.InventarioCompleto())
            {
                Time.timeScale = 0;
                panelAsociado.SetActive(true);
            }            
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = false;
        }
    }

    public void SalirDocs()
    {
        Time.timeScale = 1;
        panelAsociado.SetActive(false);
    }

    /*[SerializeField] private GameObject mensajeDoc;

    public bool jugadorTocando = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(jugadorTocando && Input.GetKeyDown("e"))
        {
            Time.timeScale = 0;
            mensajeDoc.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocando = false;
        }
    }

    public void SalirDocumento()
    {
        Time.timeScale = 1;
        mensajeDoc.SetActive(false);
    }*/
}
