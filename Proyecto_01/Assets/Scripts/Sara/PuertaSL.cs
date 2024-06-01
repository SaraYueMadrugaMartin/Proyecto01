using UnityEngine;

public class PuertaSL : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 posNuevaArriba;
    [SerializeField] private Vector2 posNuevaAbajo;
    [SerializeField] private FadeAnimation fadeAnimation;

    private bool jugadorTocandoArriba;
    private bool jugadorTocandoAbajo;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (jugadorTocandoArriba)
            {
                Invoke("CambioPosicionAbajo", 0.3f);
                fadeAnimation.FadeOut();
            }
            else if (jugadorTocandoAbajo)
            {
                Invoke("CambioPosicionArriba", 0.3f);
                fadeAnimation.FadeOut();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector2 playerPosition = other.transform.position;


            if (playerPosition.y > (posNuevaArriba.y))
            {
                jugadorTocandoArriba = true;
                jugadorTocandoAbajo = false;
            }
            else
            {
                jugadorTocandoArriba = false;
                jugadorTocandoAbajo = true;
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
        }
    }

    private void CambioPosicionArriba()
    {
        player.transform.position = posNuevaArriba;
    }

    private void CambioPosicionAbajo()
    {
        player.transform.position = posNuevaAbajo;
    }
}
