using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaSL : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 posNuevaArriba;
    [SerializeField] private Vector2 posNuevaAbajo;
    [SerializeField] private FadeAnimation fadeAnimation;

    private bool jugadorTocandoAbajo;
    private bool jugadorTocandoArriba;

    void Start()
    {
        
    }

    void Update()
    {
        if(jugadorTocandoAbajo && Input.GetKeyDown("e"))
        {
            Invoke("CambioPosicionArriba", 0.3f);
            fadeAnimation.FadeOut();
        }
        else if(jugadorTocandoArriba && Input.GetKeyDown("e"))
        {
            Invoke("CambioPosicionAbajo", 0.3f);
            fadeAnimation.FadeOut();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 obtenerPosPlayer = other.transform.position - transform.position;

            if(obtenerPosPlayer.y < 0 || obtenerPosPlayer.x < 0)
                    jugadorTocandoAbajo = true;
            else if (obtenerPosPlayer.y > 0 || obtenerPosPlayer.x > 0)
                    jugadorTocandoArriba = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocandoAbajo = false;
            jugadorTocandoArriba = false;
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
