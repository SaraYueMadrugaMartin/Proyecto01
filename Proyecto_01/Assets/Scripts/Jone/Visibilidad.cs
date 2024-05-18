using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Visibilidad : MonoBehaviour
{
    bool estaDentro = false;
    SpriteRenderer[] spr;

    private void Start()
    {
        spr = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (estaDentro)
        {
            foreach (SpriteRenderer sprite in spr)
            {
                sprite.enabled = false;
            }
        }      
        if (!estaDentro)
        {
            foreach (SpriteRenderer sprite in spr)
            {
                sprite.enabled = true;
            }
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
