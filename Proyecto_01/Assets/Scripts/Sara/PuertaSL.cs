using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaSL : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 posNuevaArriba;
    [SerializeField] private Vector2 posNuevaAbajo;
    [SerializeField] private FadeAnimation fadeAnimation;

    [SerializeField] private bool jugadorTocandoAbajo;
    [SerializeField] private bool jugadorTocandoArriba;
    [SerializeField] private bool jugadorTocandoIzquierda;
    [SerializeField] private bool jugadorTocandoDerecha;

    void Start()
    {
        
    }

    void Update()
    {        
        if(jugadorTocandoAbajo && Input.GetKeyDown("e"))
        {
            Invoke("CambioPosicionArriba", 0.3f);
            fadeAnimation.FadeOut();
            jugadorTocandoAbajo = false;
            Debug.Log("La posición del jugador es: " + player.transform.position);
        }
        else if(jugadorTocandoArriba && Input.GetKeyDown("e"))
        {
            Invoke("CambioPosicionAbajo", 0.3f);
            fadeAnimation.FadeOut();
            jugadorTocandoArriba = false;
            Debug.Log("La posición del jugador es: " + player.transform.position);
        }
        else if (jugadorTocandoDerecha && Input.GetKeyDown("e"))
        {
            Invoke("CambioPosicionAbajo", 0.3f);
            fadeAnimation.FadeOut();
            jugadorTocandoDerecha = false;
            Debug.Log("La posición del jugador es: " + player.transform.position);
        }
        else if (jugadorTocandoIzquierda && Input.GetKeyDown("e"))
        {
            Invoke("CambioPosicionArriba", 0.3f);
            fadeAnimation.FadeOut();
            jugadorTocandoIzquierda = false;
            Debug.Log("La posición del jugador es: " + player.transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 obtenerPosPlayer = other.transform.position - transform.position;

            if (obtenerPosPlayer.y < 0)
                jugadorTocandoAbajo = true;
            else if (obtenerPosPlayer.y > 0)
                jugadorTocandoArriba = true;
            else if (obtenerPosPlayer.x < 0)
                jugadorTocandoIzquierda = true;
            else if(obtenerPosPlayer.x > 0)
                jugadorTocandoDerecha = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocandoAbajo = false;
            jugadorTocandoArriba = false;
            jugadorTocandoDerecha = false;
            jugadorTocandoIzquierda = false;    
        }
    }

    public void CambioPosicionArriba()
    {
        player.transform.position = posNuevaArriba;
    }

    public void CambioPosicionAbajo()
    {
        player.transform.position = posNuevaAbajo;
    }
}
