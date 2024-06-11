using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SiguienteEscena : MonoBehaviour
{
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private GameObject player;

    private bool jugadorTocando = false;

    private void Update()
    {
        if(jugadorTocando && Input.GetKeyDown(KeyCode.E))
        {
            fadeAnimation.FadeOutNivel();
            StartCoroutine(SiguienteNivel());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            jugadorTocando = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            jugadorTocando = false;
    }

    IEnumerator SiguienteNivel()
    {
        yield return new WaitForSeconds(1f);
        player.transform.position = new Vector2(5f, 78.8f);
    }
}
