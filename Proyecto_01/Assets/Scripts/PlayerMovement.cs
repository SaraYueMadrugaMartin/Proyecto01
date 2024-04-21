using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movimiento = 5f;
    float multiplicador = 1;
    private Vector2 posicionInicial;

    Animator anim;
    Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        posicionInicial = transform.position;
    }

    private void FixedUpdate()
    {
        Velocidad();
        Mover();
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
        if (Inventario.Instance.TieneObjetosRequeridos)
        {
            SaveManager.SavePlayerData(this);
            Debug.Log("Datos guardados");
        }
        else
        {
            Debug.Log("No se pueden guardar los datos porque no se tienen los objetos requeridos.");
        }
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
        else
        {
            multiplicador = 1f;
            anim.SetBool("estaCorriendo", false);
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
        //Debug.Log(velocidadX);
    }
}
