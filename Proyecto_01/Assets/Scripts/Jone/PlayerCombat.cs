using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator anim;

    [SerializeField] private GameObject panelMuerte;

    public Transform puntoAtaque;

    [SerializeField] float rangoAtaque = 0.5f;

    public LayerMask enemigos;

    public int da�oAtaque = 50;
    public float ratioAtaque = 2f;
    float tiempoSiguienteAtaque = 0f;

    public static bool atacando = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time >= tiempoSiguienteAtaque)
        {
            if (Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
            {
                atacando = true;
                Ataque();
                tiempoSiguienteAtaque = Time.time + 1f / ratioAtaque;
                StartCoroutine(CambiarValorDespuesDeEsperar());
            }           
        }
    }

    void Ataque()
    {
        anim.SetTrigger("Ataque");
        // Sonido ataque

        // Detectar enemigos en rango de ataque 
        Collider2D[] golpeaEnemigos = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, enemigos);

        // Hacerles da�o
        foreach (Collider2D enemigo in  golpeaEnemigos) 
        {
            enemigo.GetComponent<Enemigo>().recibeDamage(da�oAtaque);
        }
    }

    private IEnumerator CambiarValorDespuesDeEsperar()
    {
        yield return new WaitForSeconds(0.8f);
        atacando = false;
    }

    public void recibeDa�o(float da�o)
    {
        Player.saludActual -= da�o;
        Debug.Log("Salud: " + Player.saludActual);

        anim.SetTrigger("recibeDa�o");
        // Sonido recibir da�o

        if (Player.saludActual <= 0)
        {
            Muere();            
        }
    }

    private void Muere()
    {
        anim.SetBool("muere", true);
        Invoke("MostrarPanelMuerte", 1.5f);
        // Sonido muerte

        // Pantalla muerte
        // Reinicio escena con el �ltimo punto de guardado
    }

    void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null)
            return;
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
    }

    private void MostrarPanelMuerte()
    {
        panelMuerte.SetActive(true);
    }
}
