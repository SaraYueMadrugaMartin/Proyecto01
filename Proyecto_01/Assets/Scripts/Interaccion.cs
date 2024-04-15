using System.Collections.Generic;
using UnityEngine;

public class Interaccion : MonoBehaviour
{
    [SerializeField] private GameObject puerta;
    List<string> elementosInteractuar = new List<string>{"Interaccion", "Llave", "Puerta"};
    private bool cogerObjeto;
    private GameObject objetoContacto;
    private bool puertaBloqueada = true;

    private void Start()
    {
        //ComprobarEstadoPuerta();
    }

    void Update()
    {
        if (cogerObjeto && Input.GetKeyDown(KeyCode.E))
        {
            if (objetoContacto != null && objetoContacto.tag == "Llave")
            {
                Destroy(objetoContacto);
                puertaBloqueada = false;
                Debug.Log("Has recogido una llave");
            }
            else if (objetoContacto != null && objetoContacto.tag == "Interaccion")
            {
                Destroy(objetoContacto);
                Debug.Log("Has recogido un arma");
            }
            else if (objetoContacto != null && objetoContacto.tag == "Puerta")
            {
                if (!puertaBloqueada)
                {
                    Debug.Log("¡Has abierto la puerta!");
                    AbrirPuerta();
                }
                else
                {
                    Debug.Log("La puerta está cerrada. Necesitas una llave.");
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
}
