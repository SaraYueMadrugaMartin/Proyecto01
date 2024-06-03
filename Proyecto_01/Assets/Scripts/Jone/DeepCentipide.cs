using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepCentipide : MonoBehaviour
{
    [SerializeField] Transform puntoSalida;
    [SerializeField] Transform puntoLlegada;
    [SerializeField] Transform puntoComer;
    [SerializeField] float velocidad = 5f;
    [SerializeField] float umbralDistancia = 0.1f; // Umbral para determinar si ha llegado al punto de llegada
    bool quieto = false; // Para que deje de desplazarse una vez mate a Alex
    bool unaVez = true; // Para que solo haga una vez la animación de comer

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        this.transform.position = puntoSalida.position; // Empieza en el punto de salida
    }

    private void Update()
    {
        if (!quieto)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, puntoLlegada.position, velocidad * Time.deltaTime); // Se desplaza hacia el punto de llegada
       
            if (Vector2.Distance(this.transform.position, puntoLlegada.position) < umbralDistancia) // Comprueba si la distancia es menor que el umbral para saber si ha llegado
            {
                this.transform.position = puntoSalida.position; // Vuelve al inicio
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Cuando Alex entra en la zona ed impacto definida por el collider, la mata
    {
        if (collision.tag == "Player" && unaVez)
        {
            collision.transform.position = puntoComer.position; // Posición de Alex para que quede bien la animación
            collision.GetComponent<Player>().SePuedeMover(false); // Que Alex no se pueda mover
            anim.SetTrigger("eat");
            quieto = true;
            collision.GetComponent<Player>().recibeDamage(200); // Mata a Alex de un golpe
            unaVez = false;
        }
    }
}
