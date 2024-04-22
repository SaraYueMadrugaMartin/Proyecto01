using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator anim;

    public Transform puntoAtaque;

    [SerializeField] float rangoAtaque = 0.5f;

    public LayerMask enemigos;

    public int dañoAtaque = 50;
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
    }

    void Ataque()
    {
        anim.SetTrigger("Ataque");
        // Sonido ataque

        // Detectar enemigos en rango de ataque 
        Collider2D[] golpeaEnemigos = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, enemigos);

        // Hacerles daño
        foreach (Collider2D enemigo in  golpeaEnemigos) 
        {
            enemigo.GetComponent<Enemigo>().recibeDaño(dañoAtaque);
        }
    }

    public void recibeDaño(float daño)
    {
        PlayerStats.saludActual -= daño;
        Debug.Log(PlayerStats.saludActual);

        anim.SetTrigger("recibeDaño");
        // Sonido recibir daño

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
        // Reinicio escena con el último punto de guardado
    }

    void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null)
            return;
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
    }
}
