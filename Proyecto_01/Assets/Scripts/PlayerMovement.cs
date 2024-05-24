using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovement : MonoBehaviour
{
    //SaveManager saveManager;

    GameManager gameManager;

    // Movimiento
    [SerializeField] float movimiento = 5f;
    float multiplicador = 1;
    private bool miraDerecha = true;
    private Puntero puntero;
    private GameObject pistola;
    public static bool sigilo = false;

    Animator anim;

    void Start()
    {
        gameManager = GameManager.instance;
        //saveManager = new SaveManager();

        anim = GetComponent<Animator>();
        puntero = GetComponent<Puntero>();
        pistola = transform.Find("Puntero").gameObject;
    }

    private void FixedUpdate()
    {
        if (!Pistola.apuntando && !PlayerCombat.atacando && !Pistola.recargando)
        {
            anim.SetBool("estaApuntando", false);
            anim.SetBool("estaRecargando", false);
            puntero.enabled = false;
            pistola.SetActive(false);
            Velocidad();
            anim.SetFloat("Velocidad", multiplicador);
            Mover();
        }
        else if (Pistola.apuntando && Pistola.recargando)
        {
            pistola.SetActive(false);
            puntero.enabled = false;
            anim.SetBool("estaApuntando", false);
            anim.SetBool("estaRecargando", true);
        }
        else if (Pistola.apuntando && !Pistola.recargando)
        {
            pistola.SetActive(true);
            puntero.enabled = true;
            anim.SetBool("estaApuntando", true);
            anim.SetBool("estaRecargando", false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            gameManager.GuardarDatosEscena();
            Debug.Log("Se han guardado los datos.");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            gameManager.ReiniciarEscena();
            Debug.Log("Datos cargados.");
        }
    }

    private void Velocidad()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sigilo = false;
            multiplicador = 1.6f;
            anim.SetBool("estaCorriendo", true);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            sigilo = true;
            multiplicador = 0.8f;
            // Aquí iría la animación de sigilo o de andar agachado, ahora mismo es la animación de andar pero más lenta
            anim.SetBool("estaCorriendo", false);
        }
        else
        {
            sigilo = false;
            multiplicador = 1f;
            anim.SetBool("estaCorriendo", false);
        }
        if (Player.corrupcion >= 25)
        {
            if (Player.corrupcion < 50)
            {
                multiplicador -= 0.1f;                
            }
            else if (Player.corrupcion < 75)
            {
                multiplicador -= 0.2f;
            }
            else if (Player.corrupcion < 90)
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
        if (velocidadX > 0 && !miraDerecha)
        {
            Flip();
            puntero.VolteaPuntero();
        }
        if (velocidadX < 0 && miraDerecha)
        {
            Flip();
            puntero.VolteaPuntero();
        }
    }
    private void Flip()
    {
        miraDerecha = !miraDerecha;
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    public void SetPosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    public void SetRotation(Quaternion newRotation)
    {
        transform.rotation = newRotation;
    }
}