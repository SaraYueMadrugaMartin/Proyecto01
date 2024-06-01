using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiaEscena : MonoBehaviour
{
    private bool estaTocando = false;

    private void Update()
    {
        if (estaTocando && Input.GetKeyDown(KeyCode.E))
        {
            cargarEscena();
        }
    }
    private void cargarEscena()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(escenaActual + 1);
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
