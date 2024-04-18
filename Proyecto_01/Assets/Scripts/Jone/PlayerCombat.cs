using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;

    public Transform puntoAtaque;

    [SerializeField] float rangoAtaque = 0.5f;

    public LayerMask enemigos;

    public int dañoAtaque = 50;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ataque();
        }
    }

    void Ataque()
    {
        // Reproducir animación ataque
        anim.SetTrigger("Ataque");

        // Detectar enemigos en rango de ataque 
        Collider2D[] golpeaEnemigos = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, enemigos);

        // Hacerles daño
        foreach (Collider2D enemigo in  golpeaEnemigos) 
        {
            //Debug.Log("Golpeo " +  enemigo.name);
            enemigo.GetComponent<Enemigo>().recibeDaño(dañoAtaque);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null)
            return;
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
    }
}
