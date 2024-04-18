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

    public void recibeDa�o(int da�o)
    {
        saludActual -= da�o;

        // Animaci�n de da�o

        if (saludActual < 0)
        {
            Muere();
        }
    }

    void Muere()
    {
        //Debug.Log("Enemigo muere");

        // Animaci�n de muerte

        // Desactivar enemigo
        Destroy(this.gameObject, 2f);
    }
}
