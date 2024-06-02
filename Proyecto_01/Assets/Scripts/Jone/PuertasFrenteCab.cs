using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PuertasFrenteCab : MonoBehaviour
{
    SpriteRenderer[] puertas;

    void Start()
    {
        puertas = GetComponentsInChildren<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cuando un objeto entra en la zona de las puertas, estas se ponen más transparentes
        CambiarTransparenciaColor(0.5f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Cuando un objeto sale de la zona de las puertas, estas vuelven a su transparencia original
        CambiarTransparenciaColor(1f);
    }

    void CambiarTransparenciaColor(float transparencia)
    {
        foreach (SpriteRenderer sprite in puertas)
        {
            // Obtener el color actual del objeto
            Color colorActual = sprite.color;

            // Establecer la nueva transparencia
            colorActual.a = transparencia;

            // Aplicar el nuevo color al objeto
            sprite.color = colorActual;
        }       
    }
}
