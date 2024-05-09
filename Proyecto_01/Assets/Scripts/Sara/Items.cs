using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private string nombreItem;
    [SerializeField] private Sprite sprite;

    private bool cogerObjeto = false;

    private Inventario inventario;

    void Start()
    {
        inventario = GameObject.Find("Canvas").GetComponent<Inventario>();
    }

    private void Update()
    {
        if (cogerObjeto && Input.GetKeyDown("e"))
        {
            inventario.AñadirObjeto(nombreItem, sprite);
            if (nombreItem == "Llave")
            {
                LlavesController llave = GetComponent<LlavesController>();
                int llaveID = llave.ObtenerID();
                Debug.Log("El ID de la llave es: " + llaveID);
                inventario.RecibeIDLlave(llaveID);
            } else if (nombreItem == "Municion")
                PlayerStats.municion += 12;
            gameObject.SetActive(false);
            //Destroy(gameObject);
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
