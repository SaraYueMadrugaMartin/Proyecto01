using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Video;

public class Xela : MonoBehaviour
{
    SonidosXela sonidosXela;

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
    float damageAtaque = 20f;

    Animator anim;

    // Para reproducir cinemáticas finales
    [SerializeField] private GameObject cintaFinBueno;
    [SerializeField] private VideoPlayer cinematicaFinBueno;
    private float duracionFinBueno;
    [SerializeField] private GameObject cintaFinMalo;
    [SerializeField] private VideoPlayer cinematicaFinMalo;
    private float duracionFinMalo;

    void Start()
    {
        sonidosXela = FindObjectOfType<SonidosXela>();
        saludActual = saludMax;
        posicionInicial = transform.position;
        anim = GetComponent<Animator>();
        rangoDeteccion = rangoDeteccionBase;

        cinematicaFinBueno = cintaFinBueno.GetComponent<VideoPlayer>();
        duracionFinBueno = (float)cinematicaFinBueno.clip.length;
        cintaFinBueno.SetActive(false);

        cinematicaFinMalo = cintaFinMalo.GetComponent<VideoPlayer>();
        duracionFinMalo = (float)cinematicaFinMalo.clip.length;
        cintaFinMalo.SetActive(false);
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
            PlayXelaIdle(sonidosXela.idleXela[0]);
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
            sonidosXela.PlayAudiosXela(sonidosXela.audiosXela[0], 0.5f);
            player.GetComponent<Player>().recibeDamage(damageAtaque);
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
            sonidosXela.PlayAudiosXela(sonidosXela.audiosXela[1], 0.5f);
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
        sonidosXela.PlayAudiosXela(sonidosXela.audiosXela[2], 0.5f);

        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
        this.enabled = false;

        // Finales
        if(Player.contadorCorr < Player.enemigosFinMalo)
        {
            Debug.Log("Reproduce final bueno");
            StartCoroutine(ReproducirFinBueno());
        }
        else
        {
            Debug.Log("Reproduce final malo");
            StartCoroutine(ReproducirFinMalo());
        }
    }

    public void PlayXelaIdle(AudioClip clip)
    {
        if (clip != null)
        {
            sonidosXela.Xela.clip = clip;
            sonidosXela.Xela.loop = true;
            sonidosXela.Xela.Play();
        }
    }

    public void StopXelaIdle()
    {
        sonidosXela.Xela.Stop();
    }

    #region CinematicasFinales
    IEnumerator ReproducirFinBueno()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cintaFinBueno.SetActive(true);

        cinematicaFinBueno.Prepare();
        while (!cinematicaFinBueno.isPrepared)
        {
            yield return null;
        }

        cinematicaFinBueno.Play();
        StartCoroutine(PararFinBueno());
    }

    IEnumerator PararFinBueno()
    {
        yield return new WaitForSecondsRealtime(duracionFinBueno);
        cinematicaFinBueno.Pause();
        cintaFinBueno.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator ReproducirFinMalo()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cintaFinMalo.SetActive(true);

        cinematicaFinMalo.Prepare();
        while (!cinematicaFinMalo.isPrepared)
        {
            yield return null;
        }

        cinematicaFinMalo.Play();
        StartCoroutine(PararFinMalo());
    }

    IEnumerator PararFinMalo()
    {
        yield return new WaitForSecondsRealtime(duracionFinMalo);
        cinematicaFinMalo.Pause();
        cintaFinMalo.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    #endregion
}

