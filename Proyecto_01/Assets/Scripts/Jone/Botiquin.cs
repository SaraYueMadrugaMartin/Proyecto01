using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botiquin : MonoBehaviour
{
    private bool estaTocando = false;


    private void Update()
    {
        if (estaTocando && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Recoge botiqu�n");

            // Todo esto deber�a suceder dentro del inventario
            // Salta texto de interacci�n "Tienes X botiquines. �Desea usar 1 botiquin para curarse?" con botones s� o no
            // En el caso de que le de a que s�

            // Efecto recuperar salud
            PlayerStats.saludActual += 50;
            if (PlayerStats.saludActual > 100 )
            {
                PlayerStats.saludActual = 100;
            }
            Debug.Log("Salud :" + PlayerStats.saludActual);
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
}
