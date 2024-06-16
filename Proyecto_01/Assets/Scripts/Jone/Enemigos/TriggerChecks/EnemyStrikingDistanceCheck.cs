using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrikingDistanceCheck : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
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
            // Jugador entra en la zona de ataque del enemigo
            _enemy.SetStrikingDistanceBool(true); // Con esto el enemigo cambiará a estado de ataque
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            //Jugador sale de la zona de ataque del enemigo
            _enemy.SetStrikingDistanceBool(false); // Con esto el enemigo cambiará a estado de seguimiento
        }
    }
}
