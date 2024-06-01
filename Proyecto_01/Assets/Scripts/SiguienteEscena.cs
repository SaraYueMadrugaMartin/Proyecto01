using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SiguienteEscena : MonoBehaviour
{
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fadeAnimation.FadeOutNivel();
            StartCoroutine(SiguienteNivel());
        }
    }

    IEnumerator SiguienteNivel()
    {
        yield return new WaitForSeconds(1f);
        player.transform.position = new Vector2(3.66f, 78.8f);
    }
}
