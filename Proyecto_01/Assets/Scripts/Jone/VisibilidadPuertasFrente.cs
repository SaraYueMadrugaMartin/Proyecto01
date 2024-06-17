using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VisibilidadPuertasFrente : MonoBehaviour
{
    Tilemap puertas;

    void Start()
    {
        puertas = GetComponent<Tilemap>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cuando player entra en la zona de las puertas, estas se ponen más transparentes
        if(collision.CompareTag("Player"))
            CambiarTransparenciaColor(0.5f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Cuando player sale de la zona de las puertas, estas vuelven a su transparencia original
        if (collision.CompareTag("Player"))
            CambiarTransparenciaColor(1f);
    }

    void CambiarTransparenciaColor(float transparencia)
    {
        // Obtener el color actual del objeto
        Color colorActual = puertas.color;

        // Establecer la nueva transparencia
        colorActual.a = transparencia;

        // Aplicar el nuevo color al objeto
        puertas.color = colorActual;
    }
}
