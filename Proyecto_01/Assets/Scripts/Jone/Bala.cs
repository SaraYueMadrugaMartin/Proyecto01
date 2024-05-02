using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] float velocidad = 20f;
    [SerializeField] float duracion = 2f;
    [SerializeField] int daño = 50;

    float tiempoVida;

    [SerializeField] GameObject efectoImpacto;

    void Start()
    {
        tiempoVida = duracion;
    }

    private void Update()
    {
        tiempoVida -= Time.deltaTime;
        if(tiempoVida < 0)
        {
            tiempoVida = duracion;
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        transform.position += transform.right * velocidad * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemigo enemigo = collision.GetComponent<Enemigo>();
        if (enemigo != null)
        {
            enemigo.recibeDaño(daño);
        }

        //Instantiate(efectoImpacto, transform.position, transform.rotation);

        //Destroy(gameObject);
    }
}
