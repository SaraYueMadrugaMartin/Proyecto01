using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xela : MonoBehaviour
{
    // Salud enemigo
    public static int saludMax = 500;
    public static int saludActual;
    [SerializeField] GameObject panelVidaXela;

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
            player.GetComponent<Player>().recibeDamage(da�oAtaque);
            tiempoSiguienteAtaque = Time.time + tiempoEspera;
        }
    }

    private void EstadoDefensa()
    {

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        // Gizmos.DrawWireSphere(player.transform.position, rangoDeteccion);

    }
    public void recibeDamage(float damage)
    {
        saludActual -= (int)damage;
        XelaHealthAnim.CambiaValue();
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
        panelVidaXela.SetActive(false);
        EntradaFinal.salaFinal = false;
        // Sonido muerte

        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
        this.enabled = false;

        // Finales
        if(Player.contadorCorr < 12)
        {
            // Reproduce final bueno
        }
        else
        {
            // Reproduce final malo
        }
    }
}

