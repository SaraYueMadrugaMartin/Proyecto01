using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] float velocidad = 20f;
    [SerializeField] float duracion = 2f;
    [SerializeField] int daño = 50;
    [SerializeField] LayerMask capaEvitar;

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
        if(Pistola.direccionDerecha)
            transform.position += transform.right * velocidad * Time.deltaTime;
        else
            transform.position -= transform.right * velocidad * Time.deltaTime;
    }   

    private void OnTriggerEnter2D(Collider2D otro)
    {
        // Verificar si el collider pertenece a la capa que queremos evitar
        if (capaEvitar == (capaEvitar | (1 << otro.gameObject.layer)))
        {
            // Ignorar temporalmente las colisiones entre esta capa y la capa del otro collider
            Physics2D.IgnoreLayerCollision(gameObject.layer, otro.gameObject.layer, true);
        }

        Debug.Log("Ha colisionado con algo");

        Enemigo enemigo = otro.GetComponent<Enemigo>();
        if (enemigo != null)
        {
            enemigo.recibeDamage(daño);
            gameObject.SetActive(false); // Desactivar el objeto cuando entra en contacto con el enemigo. Destruir no porque sino solo se dispara 1 bala.
        }

        // Incluir un efecto de colisión

        //Destroy(gameObject);
    }
}
