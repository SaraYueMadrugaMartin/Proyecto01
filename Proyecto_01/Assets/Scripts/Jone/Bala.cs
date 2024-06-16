using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] float velocidad = 20f;
    [SerializeField] float duracion = 2f;
    [SerializeField] int damage = 20;
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
        if(Player.direccionDerecha)
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

        // Comprueba que sea enemigo y le hace daño    
        Enemy enemy = otro.GetComponent<Enemy>();
        Xela xela = otro.GetComponent<Xela>(); // Para que funcione también con Xela
        if (enemy != null)
        {
            enemy.Damage(damage);
            gameObject.SetActive(false); // Desactivar el objeto cuando entra en contacto con el enemigo. Destruir no porque sino solo se dispara 1 bala.
        }
        else if (xela != null)
        {
            xela.recibeDamage(damage);
            gameObject.SetActive(false); // Desactivar el objeto cuando entra en contacto con el enemigo. Destruir no porque sino solo se dispara 1 bala.
        }

        Debug.Log("Ha colisionado con algo");
        // Incluir un efecto de colisión
        // Sonido impacto        
    }
}
