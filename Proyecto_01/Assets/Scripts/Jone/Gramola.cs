using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gramola : MonoBehaviour
{
    private bool estaTocando = false;

    private void Update()
    {
        if (estaTocando && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interactua");
            // Salta texto de interacción "Tienes X monedas. ¿Desea usar 1 moneda para escuchar una canción?" con botones sí o no
            // En el caso de que le de a que sí
            if (PlayerStats.monedasCorr >= 1)
            {
                PlayerStats.monedasCorr -= 1;
                // Reproduce música y espera a que termine el audio
                PlayerStats.corrupcion = 0;
                Debug.Log("Gasta moneda");
            }
            else
                Debug.Log("No hay monedas suficientes");
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
