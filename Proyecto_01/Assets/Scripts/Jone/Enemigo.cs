using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Salud enemigo
    public int saludMax = 100;
    int saludActual;

    int corrEnemigo = 20;

    Animator anim;

    void Start()
    {
        saludActual = saludMax;
        anim = GetComponent<Animator>();
    }

    public void recibeDaño(int daño)
    {
        saludActual -= daño;

        anim.SetTrigger("recibeDaño");
        // Sonido recibir daño

        if (saludActual <= 0)
        {
            Muere();
        }
    }

    void Muere()
    {
        anim.SetBool("seMuere", true);
        // Sonido muerte

        // Destroy(this.gameObject, 1f);    // Si decidimos que queremos directamente eliminar al enemigo
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        // Corrupcion jugador
        PlayerStats.contadorCorr +=1;
        PlayerStats.corrupcion += corrEnemigo;
    }
}
