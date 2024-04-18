using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioNivel : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Invoke("cargarEscena", 5f); // Tiempo de espera para cargar por ejemplo una animación entre medias
            cargarEscena();
        }
    }

    private void cargarEscena()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        int siguienteEscena = escenaActual + 1;
        if (siguienteEscena == 2)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(siguienteEscena);
    }
}
