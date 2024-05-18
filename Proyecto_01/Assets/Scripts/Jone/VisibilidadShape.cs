using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class VisibilidadShape : MonoBehaviour
{
    bool estaDentro = false;
    SpriteShapeRenderer spr;

    private void Start()
    {
        spr = GetComponent<SpriteShapeRenderer>();
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
        Debug.Log("entra");
        if (collision.CompareTag("Player"))
            estaDentro = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("sale");
        if (collision.CompareTag("Player"))
            estaDentro = false;
    }
}
