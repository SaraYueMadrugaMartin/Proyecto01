using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Salud enemigo
    public int saludMax = 100;
    int saludActual;

    // Para seguir al jugador
    [SerializeField] GameObject player;
    [SerializeField] float rangoDeteccion = 5f;
    [SerializeField] float velocidad = 1f;
    float distancia;

    // Para atacar al jugador
    [SerializeField] float rangoAtaque = 2f;
    float tiempoEspera = 2f;
    float tiempoSiguienteAtaque = 0f;
    float dañoAtaque = 20f;

    int corrEnemigo = 20;

    Animator anim;

    void Start()
    {
        saludActual = saludMax;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        distancia = Vector2.Distance(transform.position, player.transform.position);
        //Vector2 direccion = player.transform.position - transform.position;

        if (distancia < rangoDeteccion && distancia > rangoAtaque)
        {
            EstadoSeguimiento();           
        }
        else if (distancia < rangoAtaque)
        {
            EstadoAtaque();
        }
        else
        {
            anim.SetBool("seMueve", false);
        }
        
    }

    private void EstadoSeguimiento()
    {
        anim.SetBool("seMueve", true);
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, velocidad * Time.deltaTime);       
    }

    private void EstadoAtaque()
    {
        if (Time.time >= tiempoSiguienteAtaque)
        {
            anim.SetTrigger("ataca");
            player.GetComponent<PlayerCombat>().recibeDaño(dañoAtaque);
            tiempoSiguienteAtaque = Time.time + tiempoEspera;
        }      
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
       // Gizmos.DrawWireSphere(player.transform.position, rangoDeteccion);

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
        Debug.Log("Corrupción: " + PlayerStats.corrupcion + "%");
    }
}
