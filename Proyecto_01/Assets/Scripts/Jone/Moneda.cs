using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerStats.monedasCorr += 1;
            Debug.Log("Tengo " + PlayerStats.monedasCorr + " monedas.");
            Destroy(this.gameObject);
        }
    }
}
