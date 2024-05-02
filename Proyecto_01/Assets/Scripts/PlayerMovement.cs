using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovement : MonoBehaviour
{
    // Movimiento
    [SerializeField] float movimiento = 5f;
    float multiplicador = 1;
    private Vector2 posicionInicial;
    private bool miraDerecha = true;
    private Puntero puntero;
    private GameObject pistola;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        puntero = GetComponent<Puntero>();
        pistola = transform.Find("Puntero").gameObject;

        posicionInicial = transform.position;
    }

    private void FixedUpdate()
    {
        if (!Pistola.apuntando && !PlayerCombat.atacando)
        {
            anim.SetBool("estaApuntando", false);
            puntero.enabled = false;
            pistola.SetActive(false);
            Velocidad();
            anim.SetFloat("Velocidad", multiplicador);
            Mover();
        }
        else if (Pistola.apuntando)
        {
            pistola.SetActive(true);
            puntero.enabled = true;
            anim.SetBool("estaApuntando", true);
        }       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            LoadData();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            DeleteSavedData();
        }
    }

    private void LoadData()
    {
        PlayerData playerData = SaveManager.LoadPlayerData();
        if(playerData != null)
        {
            transform.position = new Vector2(playerData.posicion[0], playerData.posicion[1]);
            Debug.Log("Datos cargados");
        }
        else
        {
            transform.position = posicionInicial;
            Debug.Log("No se encontraron datos guardados. Cargando posición inicial del jugador.");
        }
        
    }

    private void SaveData()
    {
        PlayerData data = new PlayerData(this);
        SaveManager.SavePlayerData(this);
        Debug.Log("Posición del jugador guardada en: " + transform.position);
    }

    private void DeleteSavedData()
    {
        SaveManager.DeleteSavedData();
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
        Debug.Log("Le da la vuelta al personaje");
        miraDerecha = !miraDerecha;
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }
}