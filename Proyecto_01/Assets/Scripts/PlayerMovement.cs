using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movimiento
    [SerializeField] float movimiento = 5f;
    float multiplicador = 1;

    Animator anim;
    Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Velocidad();
        Debug.Log(multiplicador);
        anim.SetFloat("Velocidad", multiplicador);
        Mover();
    }

    private void Velocidad()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            multiplicador = 1.6f;
            anim.SetBool("estaCorriendo", true);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            multiplicador = 0.8f;
            // Aquí iría la animación de sigilo o de andar agachado, ahora mismo es la animación de andar pero más lenta
            anim.SetBool("estaCorriendo", false);
        }
        else
        {
            multiplicador = 1f;
            anim.SetBool("estaCorriendo", false);
        }
        if (PlayerStats.corrupcion >= 25)
        {
            if (PlayerStats.corrupcion < 50)
            {
                multiplicador -= 0.1f;                
            }
            else if (PlayerStats.corrupcion < 75)
            {
                multiplicador -= 0.2f;
            }
            else if (PlayerStats.corrupcion < 90)
            {
                multiplicador -= 0.3f;
            }
            else 
            {
                multiplicador -= 0.4f;
            }
        }                      
    }

    private void Mover()
    {
        float velocidadX = Input.GetAxis("Horizontal") * movimiento * multiplicador * Time.deltaTime;
        float velocidadY = Input.GetAxis("Vertical") * movimiento * multiplicador * Time.deltaTime;

        if (velocidadX != 0f || velocidadY != 0f)
            anim.SetBool("estaMoviendo", true);
        else
            anim.SetBool("estaMoviendo", false);

        transform.Translate(velocidadX, 0, 0);
        transform.Translate(0, velocidadY, 0);

        // Girar Sprite
        bool seMueve = Mathf.Abs(velocidadX) > Mathf.Epsilon;
        if (seMueve)
        {
            transform.localScale = new Vector2(Mathf.Sign(velocidadX), 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
