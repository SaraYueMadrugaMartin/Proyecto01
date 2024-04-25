<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
=======
using System;
using System.Collections;
using System.Collections.Generic;
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
using UnityEngine;

public class Enemigo : MonoBehaviour
{
<<<<<<< HEAD
    public int saludMax = 100;

    int saludActual;

    
    // Start is called before the first frame update
    void Start()
    {
        saludActual = saludMax;
    }

=======
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
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
    public void recibeDaño(int daño)
    {
        saludActual -= daño;

<<<<<<< HEAD
        // Animación de daño

        if (saludActual < 0)
=======
        anim.SetTrigger("recibeDaño");
        // Sonido recibir daño

        if (saludActual <= 0)
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
        {
            Muere();
        }
    }

    void Muere()
    {
<<<<<<< HEAD
        //Debug.Log("Enemigo muere");

        // Animación de muerte

        // Desactivar enemigo
        Destroy(this.gameObject, 2f);
=======
        anim.SetBool("seMuere", true);
        // Sonido muerte

        // Destroy(this.gameObject, 1f);    // Si decidimos que queremos directamente eliminar al enemigo
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;

        // Corrupcion jugador
        PlayerStats.contadorCorr +=1;
        PlayerStats.corrupcion += corrEnemigo;
        Debug.Log("Corrupción: " + PlayerStats.corrupcion + "%");
>>>>>>> 0bbaf9bf9d88cabc38382d704a1424094f38f980
    }
}
