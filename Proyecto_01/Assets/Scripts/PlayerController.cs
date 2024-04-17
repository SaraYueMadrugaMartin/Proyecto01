using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] float velocidadGiro = 10f;
    [SerializeField] float velocidadBase = 20f;
    [SerializeField] float velocidadCorrer = 30f;

    float velocidad;
    Rigidbody2D rb;
    Animator anim;
    [SerializeField] float fuerzaSalto = 30f;

    CapsuleCollider2D pies;

    public bool tocaSuelo = false;

    // Disparos
    bool miroDerecha = true;
    [SerializeField] GameObject flecha;
    [SerializeField] GameObject arco;
    [SerializeField] GameObject arcoFlecha;
    float tiempoEntreDisparos = 1;
    float siguienteDisparo = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pies = GetComponent<CapsuleCollider2D>();
        anim= GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        Acelerar();
        Mover();
        GirarSprite();
        //RotarJugador();

    }

    void Update()
    {
        Saltar();

        if (Input.GetButtonDown("Fire1") && Time.time >= siguienteDisparo)
        {
            StartCoroutine(Disparar());
        }
    }

    IEnumerator Disparar()
    {
        float anguloDisparo = miroDerecha ? -45f : 130f; // Uso de ternas
        Quaternion rotacion = Quaternion.Euler(0, 0, anguloDisparo); // Porque la siguiente función requiere de un quaternion, no un float
        Instantiate(flecha, arco.transform.position, rotacion);

        arcoFlecha.SetActive(false);
        arco.SetActive(true);

        siguienteDisparo = Time.time + tiempoEntreDisparos;

        yield return new WaitForSeconds(tiempoEntreDisparos);

        arcoFlecha.SetActive(true);
        arco.SetActive(false);
    }

    void GirarSprite()
    {
        bool seMueve = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if (seMueve)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);

            if (Mathf.Sign(rb.velocity.x) == 1)
                miroDerecha = true;
            else
                miroDerecha = false;
        }
    }

    private void Acelerar()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            velocidad = velocidadCorrer;
        else
            velocidad = velocidadBase;
    }

    private void Mover()
    {
        float movimiento = Input.GetAxis("Horizontal");

        if (movimiento != 0f)
            anim.SetBool("estaMoviendo", true);
        else
            anim.SetBool("estaMoviendo", false);

        Vector3 vectorMovimiento = new Vector3(movimiento*velocidad, rb.velocity.y,0f);
        //rb.AddForce(vectorMovimiento* velocidadBase);
        rb.velocity = vectorMovimiento;

    }

   /* private void RotarJugador()
    {
        float movimiento = Input.GetAxis("Vertical");
        rb.AddTorque(movimiento*velocidadGiro);
    }
   */

    void Saltar()
    {
        if (pies.IsTouchingLayers(LayerMask.GetMask("Plataforma")))
            tocaSuelo = true;
        else
            tocaSuelo = false;

        if (Input.GetButtonDown("Jump") && tocaSuelo)
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            anim.SetBool("estaSaltando", true);
        }
        if (Input.GetButtonUp("Jump"))
            anim.SetBool("estaSaltando", false);
    }

    public void Morir()
    {
        anim.SetTrigger("Muriendo");
        rb.velocity = new Vector2(10f, 50f);

    }

}
