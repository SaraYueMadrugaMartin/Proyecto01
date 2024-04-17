using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaccion : MonoBehaviour
{
    [SerializeField] private GameObject puerta;
    [SerializeField] private GameObject llaveClave;
    [SerializeField] private Image imagenInventario;

    private Inventario inventario;

    List<string> elementosInteractuar = new List<string>{"Bate", "Llave", "Puerta", "Tinta", "Documento"};
    private bool cogerObjeto;
    private GameObject objetoContacto;
    private bool puertaBloqueada = true;

    private void Start()
    {
        //ComprobarEstadoPuerta();

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
            if (objetoContacto != null)
            {
                if (objetoContacto == llaveClave)
                {
                    objetoContacto.SetActive(false);
                    puertaBloqueada = false;
                    Debug.Log("Has recogido la llave clave. ¡La puerta está desbloqueada!");
                }
                else if (objetoContacto.tag == "Bate")
                {
                    objetoContacto.SetActive(false);
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
                else if (objetoContacto.tag == "Puerta")
                {
                    if (!puertaBloqueada)
                    {
                        Debug.Log("¡Has abierto la puerta!");
                        AbrirPuerta();
                    }
                    else
                    {
                        Debug.Log("La puerta está cerrada. Necesitas la llave clave para abrirla.");
                    }
                }
            }
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

    private void ComprobarEstadoPuerta()
    {
        if (puertaBloqueada)
        {
            Debug.Log("La puerta está cerrada.");
        }
        else
        {
            Debug.Log("La puerta está abierta.");
        }
    }

    private void AbrirPuerta()
    {
        puerta.SetActive(false);
    }

    private void MostrarImagenObjeto(GameObject objeto)
    {
        Sprite spriteObjeto = objeto.GetComponent<SpriteRenderer>().sprite;

        imagenInventario.sprite = spriteObjeto;
    }
}
