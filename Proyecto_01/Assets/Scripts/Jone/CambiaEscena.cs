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
            CargarEscena();
        }
    }
    private void CargarEscena()
    {
        int contador = 0;
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(escenaActual + 1);
        contador++;

        if(contador == 1)
        {
            AudioManager.instance.Play("CampoMaiz");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if(contador == 2)
        {
            AudioManager.instance.Play("Planta1");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
