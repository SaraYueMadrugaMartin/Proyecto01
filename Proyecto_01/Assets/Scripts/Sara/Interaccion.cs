<<<<<<< HEAD
ï»¿using System.Collections;
=======
using System.Collections;
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaccion : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] private Image imagenInventario;

    private Inventario inventario;
    private PuertaController puerta;

    List<string> elementosInteractuar = new List<string>{"Bate", "Llave", "Tinta", "Documento"};
=======
    /*[SerializeField] private Image imagenInventario;

    private Inventario inventario;
    private Puerta puerta;

    List<string> elementosInteractuar = new List<string> { "Bate", "Llave", "Tinta", "Documento" };
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
    private bool cogerObjeto;
    private GameObject objetoContacto;

    private void Start()
    {
        inventario = FindObjectOfType<Inventario>();
        if (inventario == null)
        {
<<<<<<< HEAD
            Debug.LogError("Â¡No se encontrÃ³ el componente Inventario en la escena!");
        }

        puerta = FindObjectOfType<PuertaController>();
        if (puerta == null)
        {
            Debug.LogError("Â¡No se encontrÃ³ el componente Inventario en la escena!");
=======
            Debug.LogError("¡No se encontró el componente Inventario en la escena!");
        }

        puerta = FindObjectOfType<Puerta>();
        if (puerta == null)
        {
            Debug.LogError("¡No se encontró el componente Inventario en la escena!");
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
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
<<<<<<< HEAD
            Debug.Log("Â¿Coger el objeto?");
=======
            Debug.Log("¿Coger el objeto?");
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
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
<<<<<<< HEAD
    }
}
=======
    }*/
}
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
