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
    private bool defiende = false;

    // Para volver a posición de inicio
    private float tiempoIdle = 0f;
    private float tiempoEsperaIdle = 8f;
    Vector3 posicionInicial;
    private float umbralDistancia = 0.1f;
    private Coroutine moverCorrutina;

    // Para atacar al jugador
    [SerializeField] float rangoAtaque = 2f;
    float tiempoEspera = 2f;
    float tiempoSiguienteAtaque = 0f;
    float dañoAtaque = 20f;

    Animator anim;

    void Start()
    {
        saludActual = saludMax;
        posicionInicial = transform.position;
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
            if (moverCorrutina != null)
            {
                StopCoroutine(moverCorrutina);
                anim.SetBool("seMueve", false);
            }
            EstadoSeguimiento();
        }
        else if (distancia < rangoAtaque)
        {
            if (moverCorrutina != null)
            {
                StopCoroutine(moverCorrutina);
                anim.SetBool("seMueve", false);
            }
            EstadoAtaque();
        }
        else
        {
            EstadoIdle();
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

    private void EstadoIdle()
    {
        anim.SetBool("seMueve", false);
        tiempoIdle += Time.deltaTime;
        // Si pasa 8 segundos en estado de idle sin detectar al jugador vuelve a la posición inicial
        if (tiempoIdle > tiempoEsperaIdle)
        {
            if (moverCorrutina != null)
            {
                StopCoroutine(moverCorrutina);
            }
            moverCorrutina = StartCoroutine(MoverAPosicionInicial());
            tiempoIdle = 0f; // Resetea el tiempoIdle después de iniciar la corrutina.
        }
    }

    private IEnumerator MoverAPosicionInicial()
    {
        while (Vector2.Distance(this.transform.position, posicionInicial) > umbralDistancia)
        {
            distancia = Vector2.Distance(transform.position, player.transform.position);
            if (distancia < rangoDeteccion)
            {
                anim.SetBool("seMueve", false);
                yield break; // Sale de la corrutina si el jugador vuelve a estar en el rango de detección
            }
            anim.SetBool("seMueve", true);
            transform.position = Vector2.MoveTowards(transform.position, posicionInicial, velocidad * Time.deltaTime);
            yield return null;
        }
        anim.SetBool("seMueve", false);
    }


    private void EstadoSeguimiento()
    {
        if (!defiende) 
        {
            anim.SetBool("seMueve", true);
            // Se mueve hacia el jugador
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, velocidad * Time.deltaTime);
        }
    }

    private void EstadoAtaque()
    {
        // El enemigo tiene un tiempo fijo entre ataques
        if (Time.time >= tiempoSiguienteAtaque)
        {
            anim.SetTrigger("ataca");
            player.GetComponent<Player>().recibeDamage(dañoAtaque);
            tiempoSiguienteAtaque = Time.time + tiempoEspera;
            // Después de atacar hay una posibilidad de que entre en estado de defensa
            RandomDefiende();
        }
    }

    private void RandomDefiende()
    {
        bool saleDefiende = UnityEngine.Random.value < 0.4f; // 40% de probabilidad de ser true
        if (saleDefiende)
        {
            EstadoDefensa();
        }
    }

    private void EstadoDefensa()
    {
        anim.SetBool("seDefiende", true);
        defiende = true;
        StartCoroutine(MantenerDefensa());
    }

    // Tiempo de defensa aleatorio entre 2 y 7 segundos
    private IEnumerator MantenerDefensa()
    {
        float tiempoDefensa = UnityEngine.Random.Range(3f, 8f);
        yield return new WaitForSeconds(tiempoDefensa);
        SaleEstadoDefensa();
    }

    private void SaleEstadoDefensa()
    {
        anim.SetBool("seDefiende", false);
        defiende = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }

    public void recibeDamage(float damage)
    {
        if (!defiende) // Si se está defendiendo no recibe daño
        {
            saludActual -= (int)damage;
            XelaHealthAnim.CambiaValue();
            anim.SetTrigger("recibeDaño");
            // Sonido recibir daño
            // Comprueba la salud cada vez que recibe un golpe para ver si muere
            if (saludActual <= 0)
                Muere();
        }
        else 
        {
            // Sonido defensa
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

