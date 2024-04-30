using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzle : MonoBehaviour
{
    [SerializeField] private GameObject[] numeros;
    private int indiceActual = 0;

    [SerializeField] private int numeroCorrecto;
    [SerializeField] private bool codigoCorrecto;

    private int totalNumerosCorrectos;

    void Start()
    {
        totalNumerosCorrectos = 0;

        for (int i = 1; i < numeros.Length; i++)
        {
            numeros[i].SetActive(false);
        }

        numeros[0].SetActive(true);
    }

    void Update()
    {

    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            PulsarBotonDerecho();
            PulsarBotonIzquierdo();
        }
    }

    public void PulsarBotonDerecho()
    {
        numeros[indiceActual].SetActive(false);

        indiceActual = (indiceActual + 1) % numeros.Length;

        numeros[indiceActual].SetActive(true);
        VerificarNumero();
    }

    public void PulsarBotonIzquierdo()
    {
        numeros[indiceActual].SetActive(false);

        if (indiceActual == 0)
        {
            indiceActual = numeros.Length - 1;
        }
        else
        {
            indiceActual = (indiceActual - 1) % numeros.Length;
        }

        numeros[indiceActual].SetActive(true);
        VerificarNumero();
    }

    public void VerificarNumero()
    {
        if (numeros[indiceActual] == numeros[numeroCorrecto])
        {
            totalNumerosCorrectos++;
            codigoCorrecto = true;
            Debug.Log("Numero Correcto" + totalNumerosCorrectos);
        }
        else
        {
            codigoCorrecto = false;
            Debug.Log("Este número no es correcto" + totalNumerosCorrectos);
        }
    }

    public void VerificarCodigoCompleto()
    {

    }
}
