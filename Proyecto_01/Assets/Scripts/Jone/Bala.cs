using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] float velocidad = 20f;
    [SerializeField] int da�o = 50;
    Rigidbody2D rb;
    [SerializeField] GameObject efectoImpacto;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * velocidad;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemigo enemigo = collision.GetComponent<Enemigo>();
        if (enemigo != null)
        {
            enemigo.recibeDa�o(da�o);
        }

        Instantiate(efectoImpacto, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
