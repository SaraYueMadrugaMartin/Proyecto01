using UnityEngine;

public class PuertaSL : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 posNuevaArriba;
    [SerializeField] private Vector2 posNuevaAbajo;
    [SerializeField] private FadeAnimation fadeAnimation;

    [SerializeField] private bool jugadorTocandoArriba;
    [SerializeField] private bool jugadorTocandoAbajo;
    [SerializeField] private bool jugadorTocandoDerecha;
    [SerializeField] private bool jugadorTocandoIzquierda;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (jugadorTocandoArriba)
            {
                CambioPosicionAbajo();
            }
            else if (jugadorTocandoAbajo)
            {
                CambioPosicionArriba();
            }
            else if (jugadorTocandoDerecha)
            {
                CambioPosicionIzquierda();
            }
            else if (jugadorTocandoIzquierda)
            {
                CambioPosicionDerecha();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 playerPosition = other.transform.position;
            Vector2 doorPosition = transform.position;

            float distanciaY = Mathf.Abs(playerPosition.y - doorPosition.y);
            float distanciaX = Mathf.Abs(playerPosition.x - doorPosition.x);

            if (distanciaY > distanciaX)
            {
                jugadorTocandoArriba = playerPosition.y > doorPosition.y;
                jugadorTocandoAbajo = playerPosition.y < doorPosition.y;
                jugadorTocandoIzquierda = false;
                jugadorTocandoDerecha = false;
            }
            else
            {
                jugadorTocandoIzquierda = playerPosition.x < doorPosition.x;
                jugadorTocandoDerecha = playerPosition.x > doorPosition.x;
                jugadorTocandoArriba = false;
                jugadorTocandoAbajo = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerEnter2D(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorTocandoArriba = false;
            jugadorTocandoAbajo = false;
            jugadorTocandoDerecha = false;
            jugadorTocandoIzquierda = false;
        }
    }

    private void CambioPosicionArriba()
    {
        player.transform.position = posNuevaArriba;
        fadeAnimation.FadeOut();
    }

    private void CambioPosicionAbajo()
    {
        player.transform.position = posNuevaAbajo;
        fadeAnimation.FadeOut();
    }

    private void CambioPosicionDerecha()
    {
        player.transform.position = posNuevaArriba;
        fadeAnimation.FadeOut();
    }

    private void CambioPosicionIzquierda()
    {
        player.transform.position = posNuevaAbajo;
        fadeAnimation.FadeOut();
    }
}
