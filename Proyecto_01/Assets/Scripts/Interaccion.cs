using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaccion : MonoBehaviour
{
    Inventario inventario;
    // Start is called before the first frame update
    void Start()
    {
        inventario = GetComponent<Inventario>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Interaccion")
        {
            Debug.Log("Pulsa espacio para recoger objeto");
            if (Input.GetKey(KeyCode.Space))
            {
                inventario.cuadrado = true;
                Destroy(collision.gameObject, 1f);
            }
        }   
        if (collision.tag == "Puerta")
        {
            Debug.Log("Puerta cerrada");
            if (inventario.cuadrado)
            {
                Debug.Log("Tengo llave");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Destroy(collision.gameObject, 1f);
                    Debug.Log("Puerta abierta");
                }
            } else
            {
                Debug.Log("No tengo llave");
            }
        }
    }
}
