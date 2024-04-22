using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Corrupción
    static public int contadorCorr = 0;
    static public float corrupcion = 0;
    static public int monedasCorr = 0;

    // Salud
    static public float saludMax = 100;
    static public float saludActual;

    private void Start()
    {
        saludActual = saludMax;
    }
}
