using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour
{
    [SerializeField] Transform puntoDisparo;
    public static bool apuntando = false;

    // Update is called once per frame
    void Update()
    {     
        // Debería comprobar que tengo pistola
        if (Input.GetButton("Fire2"))
        {
            apuntando = true;
            if (Input.GetButtonDown("Fire1"))
            {
                Dispara();
            }
        } else 
            apuntando = false;
    }

    void Dispara()
    {
        GameObject objetoBala = PoolingBalas.instancia.GetBala();       

        objetoBala.transform.position = puntoDisparo.position;
        objetoBala.transform.rotation = puntoDisparo.rotation;
    }
}
