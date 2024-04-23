using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Visibilidad : MonoBehaviour
{
    bool estaDentro = false;
    SpriteRenderer spr;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (estaDentro)
        {
            spr.enabled = false;
        }      
        if (!estaDentro)
        {
            spr.enabled = true;
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
