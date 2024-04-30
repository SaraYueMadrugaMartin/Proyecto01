using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzle : MonoBehaviour
{
    [SerializeField] private GameObject[] numeros;
    private int indiceActual = 0;

    [SerializeField] private int numeroCorrecto;
    [SerializeField] private bool codigoCorrecto;

    private static int totalNumerosCorrectos; // Lo pongo en static para que el contador sume entre los scripts, sino lo hace de manera individual

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
            VerificarCodigoCompleto();
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
            if (!codigoCorrecto)
            {
                totalNumerosCorrectos++;
                Debug.Log("Número Correcto: " + totalNumerosCorrectos);
            }
            codigoCorrecto = true;
        }
        else
        {
            if (codigoCorrecto)
            {
                totalNumerosCorrectos--;
                Debug.Log("Este número no es correcto. Total de números correctos: " + totalNumerosCorrectos);
            }
            codigoCorrecto = false;
        }
    }


    public void VerificarCodigoCompleto()
    {
        if(totalNumerosCorrectos == 4)
        {
            SceneManager.LoadScene("PruebasSara");
        }
        else
        {
            Debug.Log("Este no es el código correcto");
        }
    }
}
