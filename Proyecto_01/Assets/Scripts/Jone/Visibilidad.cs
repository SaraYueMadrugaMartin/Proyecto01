using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Visibilidad : MonoBehaviour
{
    bool estaDentro = false;
    SpriteRenderer spr;
    SpriteShapeRenderer spr2;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        spr2 = GetComponent<SpriteShapeRenderer>();
    }

    private void Update()
    {
        if (estaDentro)
        {
            spr.enabled = false;
            spr2.enabled = false;
        }      
        if (!estaDentro)
        {
            spr.enabled = true;
            spr2.enabled = true;
        }          
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            estaDentro = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            estaDentro = false;
    }
}
