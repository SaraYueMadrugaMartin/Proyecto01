using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasilloEnemigos : MonoBehaviour
{
    private void Update()
    {
        ActivarHijosSegunContador();
    }

    void ActivarHijosSegunContador()
    {
        int hijosActivados = 0;

        // Recorremos todos los hijos del objeto padre
        foreach (Transform hijo in transform)
        {
            // Activamos el hijo si no hemos alcanzado la cantidad máxima
            if (hijosActivados < Player.contadorCorr)
            {
                hijo.gameObject.SetActive(true);
                hijosActivados++;
            }
            else
            {
                // Desactivamos los hijos restantes si ya hemos alcanzado la cantidad máxima
                hijo.gameObject.SetActive(false);
            }
        }
    }

}
