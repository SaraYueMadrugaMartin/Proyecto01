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
    [SerializeField] float rangoDeteccionBase = 5f;
    [SerializeField] float rangoDeteccionSigilo = 3.5f;
    private float rangoDeteccion;
    [SerializeField] float velocidad = 1f;
    float distancia;
    private bool miraDerecha = true;

    // Para atacar al jugador
    [SerializeField] float rangoAtaque = 2f;
    float tiempoEspera = 2f;
    float tiempoSiguienteAtaque = 0f;
    float da�oAtaque = 20f;   

    int corrEnemigo = 20;

    Animator anim;

    void Start()
    {
        saludActual = saludMax;
        anim = GetComponent<Animator>();
        rangoDeteccion = rangoDeteccionBase;
    }
    private void Update()
    {
        distancia = Vector2.Distance(transform.position, player.transform.position);

        if (Player.estaSigilo)
            rangoDeteccion = rangoDeteccionSigilo;
        else
            rangoDeteccion = rangoDeteccionBase;

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

        // Girar Sprite dependiendo de la direcci�n en la que camina el enemigo
        if (player.transform.position.x > transform.position.x && !miraDerecha)
        {
            Flip();
        }
        else if (player.transform.position.x < transform.position.x && miraDerecha)
        {
            Flip();
        }
    }

    private void Flip()
    {
        miraDerecha = !miraDerecha;
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
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
            player.GetComponent<PlayerCombat>().recibeDa�o(da�oAtaque);
            tiempoSiguienteAtaque = Time.time + tiempoEspera;
        }      
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
       // Gizmos.DrawWireSphere(player.transform.position, rangoDeteccion);

    }
    public void recibeDa�o(int da�o)
    {
        saludActual -= da�o;

        anim.SetTrigger("recibeDa�o");
        // Sonido recibir da�o

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
        Player.contadorCorr +=1;
        Player.corrupcion += corrEnemigo;
        Debug.Log("Corrupci�n: " + Player.corrupcion + "%");
    }
}
