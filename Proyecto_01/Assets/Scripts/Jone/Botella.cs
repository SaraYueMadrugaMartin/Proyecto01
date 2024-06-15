using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botella : MonoBehaviour
{
    [SerializeField] float duracion = 2f;
    [SerializeField] float damage = 20f;
    [SerializeField] float velocidadRotacion = 500f; // Velocidad de rotación en grados por segundo

    float tiempoVida;

    void Start()
    {
        tiempoVida = duracion;
    }

    private void Update()
    {
        tiempoVida -= Time.deltaTime;
        if (tiempoVida < 0)
        {
            tiempoVida = duracion;
            gameObject.SetActive(false);
        }
        // Rotar el objeto
        transform.Rotate(0, 0, velocidadRotacion * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        // Comprueba que sea enemigo y le hace daño
        if (otro.CompareTag("Player"))
        {
            Player player = otro.GetComponent<Player>();
            player.recibeDamage(damage);
            gameObject.SetActive(false); // Desactivar el objeto cuando entra en contacto con el enemigo. Destruir no porque sino solo se dispara 1 bala.
        }
        // Incluir un efecto de colisión
        // Sonido impacto        
    }
}
