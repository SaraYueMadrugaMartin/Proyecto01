using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour
{
    [SerializeField] Transform puntoDisparo;
    public GameObject balaPrefab;

    // Update is called once per frame
    void Update()
    {
        // Debería comprobar que tengo pistola
        if (Input.GetButton("Fire2"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Dispara();
            }
        }       
    }

    void Dispara()
    {
        Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
    }
}
