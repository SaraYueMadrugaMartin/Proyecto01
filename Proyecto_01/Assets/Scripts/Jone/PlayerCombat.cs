<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
=======
using System;
using System.Collections;
using System.Collections.Generic;
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
<<<<<<< HEAD
    public Animator anim;
=======
    Animator anim;
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980

    public Transform puntoAtaque;

    [SerializeField] float rangoAtaque = 0.5f;

    public LayerMask enemigos;

    public int da�oAtaque = 50;
<<<<<<< HEAD
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ataque();
        }
=======
    public float ratioAtaque = 2f;
    float tiempoSiguienteAtaque = 0f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= tiempoSiguienteAtaque)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ataque();
                tiempoSiguienteAtaque = Time.time + 1f / ratioAtaque;
            }
        }       
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
    }

    void Ataque()
    {
<<<<<<< HEAD
        // Reproducir animaci�n ataque
        anim.SetTrigger("Ataque");
=======
        anim.SetTrigger("Ataque");
        // Sonido ataque
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980

        // Detectar enemigos en rango de ataque 
        Collider2D[] golpeaEnemigos = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, enemigos);

        // Hacerles da�o
        foreach (Collider2D enemigo in  golpeaEnemigos) 
        {
<<<<<<< HEAD
            //Debug.Log("Golpeo " +  enemigo.name);
=======
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
            enemigo.GetComponent<Enemigo>().recibeDa�o(da�oAtaque);
        }
    }

<<<<<<< HEAD
=======
    public void recibeDa�o(float da�o)
    {
        PlayerStats.saludActual -= da�o;
        Debug.Log("Salud: " + PlayerStats.saludActual);

        anim.SetTrigger("recibeDa�o");
        // Sonido recibir da�o

        if (PlayerStats.saludActual <= 0)
        {
            Muere();
        }
    }

    private void Muere()
    {
        anim.SetBool("muere", true);
        // Sonido muerte

        // Pantalla muerte
        // Reinicio escena con el �ltimo punto de guardado
    }

>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
    void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null)
            return;
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
    }
}
