using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolverPlanta01 : MonoBehaviour
{
    [SerializeField] private FadeAnimation fadeAnimation;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            fadeAnimation.FadeOutNivel();
            StartCoroutine(VolverPlanta());
        }
    }

    IEnumerator VolverPlanta()
    {
        yield return new WaitForSeconds(1f);
        player.transform.position = new Vector2(-4.99f, 12.13f);
    }
}
