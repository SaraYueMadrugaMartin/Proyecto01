using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botiquin : MonoBehaviour
{
    private bool estaTocando = false;
    [SerializeField] private Inventario inventario;


    private void Update()
    {
        if (estaTocando && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Recoge botiquín");

            // Todo esto debería suceder dentro del inventario
            // Salta texto de interacción "Tienes X botiquines. ¿Desea usar 1 botiquin para curarse?" con botones sí o no
            // En el caso de que le de a que sí

            // Efecto recuperar salud
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            estaTocando = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            estaTocando = false;
    }

    public void Curarse()
    {
        Player.saludActual += 50;
        if (Player.saludActual > 100)
        {
            Player.saludActual = 100;
        }
        inventario.VaciarHueco("Botiquin");
        Debug.Log("Salud :" + Player.saludActual);
    }
}
