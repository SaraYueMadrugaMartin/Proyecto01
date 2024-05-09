using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    private bool estaTocando = false;

    private void Update()
    {
        if (estaTocando && Input.GetKeyDown(KeyCode.E))
        {
            Player.monedasCorr += 1;
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
