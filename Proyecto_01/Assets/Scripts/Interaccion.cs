using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaccion : MonoBehaviour
{
    [SerializeField] private Image imagenInventario;

    private Inventario inventario;
    private Puerta puerta;

    List<string> elementosInteractuar = new List<string>{"Bate", "Llave", "Tinta", "Documento"};
    private bool cogerObjeto;
    private GameObject objetoContacto;

    private void Start()
    {
        inventario = FindObjectOfType<Inventario>();
        if (inventario == null)
        {
            Debug.LogError("¡No se encontró el componente Inventario en la escena!");
        }
    }

    void Update()
    {
        if (cogerObjeto && Input.GetKeyDown(KeyCode.E))
        {
            CogerObjetos();
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (elementosInteractuar.Contains(other.tag))
        {
            Debug.Log("¿Coger el objeto?");
            cogerObjeto = true;
            objetoContacto = other.gameObject;

            if (inventario != null && other.CompareTag("Llave") || other.CompareTag("Bate") || other.CompareTag("Tinta") || other.CompareTag("Documento"))
            {
                inventario.AgregarObjeto(other.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (elementosInteractuar.Contains(other.tag))
        {
            cogerObjeto = false;
            objetoContacto = null;
        }
    }

    private void CogerObjetos()
    {
        if (objetoContacto != null)
        {
            if (objetoContacto.tag == "Llave")
            {
                objetoContacto.SetActive(false);
                puerta.ActualizarEstadoPuerta();
                Debug.Log("Has recogido la llave clave");
            }
            else if (objetoContacto.tag == "Bate")
            {
                objetoContacto.SetActive(false);
                //inventario.objetoRecogido = true;
                Debug.Log("Has recogido un arma");
            }
            else if (objetoContacto.tag == "Tinta")
            {
                objetoContacto.SetActive(false);
                Debug.Log("Has recogido un bote de tinta");
            }
            else if (objetoContacto.tag == "Documento")
            {
                objetoContacto.SetActive(false);
                Debug.Log("Has recogido un documento");
            }
        }
    }
    private void MostrarImagenObjeto(GameObject objeto)
    {
        Sprite spriteObjeto = objeto.GetComponent<SpriteRenderer>().sprite;

        imagenInventario.sprite = spriteObjeto;
    }
}
