using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour
{
    [SerializeField] Transform puntoDisparo;
    public static bool apuntando = false;
    public static bool recargando = false;
    private Animator animPistola, animBrazo;
    private int cargador = 6;

    private void Start()
    {
        animPistola = transform.Find("Puntero").GetComponent<Animator>();
        animBrazo = transform.Find("Puntero").Find("Brazo").GetComponent<Animator>();
    }


    // Habr�a que comprobar si tiene pistola
    void Update()
    {     
        // Deber�a comprobar que tengo pistola
        if (Input.GetButton("Fire2"))
        {
            // Implementaci�n visual de n�mero de balas
            apuntando = true;
            if (Input.GetButtonDown("Fire1"))
            {
                if (cargador > 0)
                {
                    Dispara();
                }
                else
                {
                    // Hay que mostrar un mensaje en pantalla
                    Debug.Log("Sin munici�n, pulsa R mientras apuntas para recargar");
                }
            }
            if (Input.GetKeyDown("r"))
            {
                Recarga();
            }
        } else 
            apuntando = false;
    }

    private void Dispara()
    {
        animPistola.SetTrigger("dispara");
        animBrazo.SetTrigger("dispara");

        GameObject objetoBala = PoolingBalas.instancia.GetBala();

        objetoBala.transform.position = puntoDisparo.position;
        objetoBala.transform.rotation = puntoDisparo.rotation;

        --cargador;
    }

    private void Recarga()
    {
        if (PlayerStats.municion > 0)
        {
            recargando = true;
            StartCoroutine(CambiarValorDespuesDeEsperar());
            Debug.Log("Recargado");
            cargador = 6;
            PlayerStats.municion -= 6;
        } else
        {
            Debug.Log("No hay suficiente munici�n"); // Implementar texto de aviso
        }     
    }

    private IEnumerator CambiarValorDespuesDeEsperar()
    {
        yield return new WaitForSeconds(0.6f);
        recargando = false;
    }
}
