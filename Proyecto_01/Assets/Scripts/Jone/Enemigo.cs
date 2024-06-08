using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    SFXManager sfxManager;

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
    float dañoAtaque = 20f;   

    int corrEnemigo = 20;

    Animator anim;

    public static int contadorEnemigosMuertos = 0;

    void Start()
    {
        saludActual = saludMax;
        anim = GetComponent<Animator>();
        rangoDeteccion = rangoDeteccionBase;
        sfxManager = FindObjectOfType<SFXManager>();
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

        // Girar Sprite dependiendo de la dirección en la que camina el enemigo
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
        sfxManager.PlaySFX(sfxManager.audiosEnemigos[0]);
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, velocidad * Time.deltaTime);       
    }

    private void EstadoAtaque()
    {
        if (Time.time >= tiempoSiguienteAtaque)
        {
            anim.SetTrigger("ataca");
            sfxManager.PlaySFX(sfxManager.audiosEnemigos[1]);
            player.GetComponent<Player>().recibeDamage(dañoAtaque);
            tiempoSiguienteAtaque = Time.time + tiempoEspera;
        }      
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
       // Gizmos.DrawWireSphere(player.transform.position, rangoDeteccion);

    }
    public void recibeDamage(float damage)
    {
        saludActual -= (int)damage;

        anim.SetTrigger("recibeDaño");
        // Sonido recibir daño
        sfxManager.PlaySFX(sfxManager.audiosEnemigos[3]);

        if (saludActual <= 0)
        {
            Muere();
        }
    }

    void Muere()
    {
        anim.SetBool("seMuere", true);
        // Sonido muerte
        sfxManager.PlaySFX(sfxManager.audiosEnemigos[2]);
        // Destroy(this.gameObject, 1f);    // Si decidimos que queremos directamente eliminar al enemigo
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        contadorEnemigosMuertos++;

        // Corrupcion jugador
        Player.contadorCorr +=1;
        Player.corrupcion += corrEnemigo;
        Debug.Log("Corrupción: " + Player.corrupcion + "%");
    }

    public int GetNumEnemMuertos()
    {
        return contadorEnemigosMuertos;
    }

    public void SetNumEnemMuertos(int enemMuertos)
    {
        contadorEnemigosMuertos = enemMuertos;
    }
}
