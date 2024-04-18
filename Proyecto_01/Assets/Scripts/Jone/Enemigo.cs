using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int saludMax = 100;

    int saludActual;

    
    // Start is called before the first frame update
    void Start()
    {
        saludActual = saludMax;
    }

    public void recibeDaño(int daño)
    {
        saludActual -= daño;

        // Animación de daño

        if (saludActual < 0)
        {
            Muere();
        }
    }

    void Muere()
    {
        //Debug.Log("Enemigo muere");

        // Animación de muerte

        // Desactivar enemigo
        Destroy(this.gameObject, 2f);
    }
}
