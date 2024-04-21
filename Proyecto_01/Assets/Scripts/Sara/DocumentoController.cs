using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentoController : MonoBehaviour
{
    [SerializeField] private GameObject mensajeDoc;

    public bool jugadorTocando = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(jugadorTocando && Input.GetKeyDown("e"))
        {
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
        mensajeDoc.SetActive(false);
    }
}
