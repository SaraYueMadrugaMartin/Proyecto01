using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp_pistola : MonoBehaviour
{
    private bool estaTocando = false;
    [SerializeField] Player player;

    private void Update()
    {
        if (estaTocando && Input.GetKeyDown(KeyCode.E))
        {
            player.EquiparArma(2);
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
