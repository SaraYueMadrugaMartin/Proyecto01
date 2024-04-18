using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    PlayerStats playerStats;

    public int saludMax = 100;

    int saludActual;

    
    // Start is called before the first frame update
    void Start()
    {
        saludActual = saludMax;
        playerStats = GetComponent<PlayerStats>(); 
    }

    public void recibeDaño(int daño)
    {
        saludActual -= daño;

        // Animación de recibir daño

        if (saludActual <= 0)
        {
            Muere();
        }
    }

    void Muere()
    {
        Debug.Log("Enemigo muere");

        // Animación de muerte

        // Desactivar enemigo
        Destroy(this.gameObject, 1f);

        // Corrupcion jugador
        playerStats.contadorCorr += 1;
        Debug.Log("playerStats.contadorCorr");
    }
}
