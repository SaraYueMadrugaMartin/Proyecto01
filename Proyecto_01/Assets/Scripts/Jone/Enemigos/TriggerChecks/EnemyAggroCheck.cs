using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroCheck : MonoBehaviour
{
    public GameObject PlayerTarget {  get; set; }
    private Enemy _enemy;

    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            // Jugador entra en la zona agresiva del enemigo
            _enemy.SetAggroStatus(true); // Con esto el enemigo cambiará a estado de seguimiento
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            //Jugador sale de la zona agresiva del enemigo
            _enemy.SetAggroStatus(false); // Con esto el enemigo cambiará a estado de idle
        }
    }
}
