using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogosSystem : MonoBehaviour
{
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private TextMeshProUGUI dialogoAlex01;

    [SerializeField] private float esperaInicio;

    [SerializeField] private string[] frasesDialogoAlex01;

    [SerializeField] private float velocidadTexto;
    private int index; // Lo creamos para conocer la posici�n en la que se encuentra en el array de las frases.

    void Start()
    {        
        panelDialogo.SetActive(false);
        StartCoroutine(EsperaInicial());
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Si el texto que se est� mostrando coincide con el �ndice de la frase actual
            if(dialogoAlex01.text == frasesDialogoAlex01[index])
            {
                SiguienteFrase(); // Llama a la siguiente frase.
            }
            else
            {
                StopAllCoroutines(); // Sino, para todas las corrutinas
                dialogoAlex01.text = frasesDialogoAlex01[index]; // Rellena la frase completa de golpe.
            }
        }
    }

    void ComenzarDialogos()
    {
        index = 0; // Iniciamos el �ndice en 0.        
        StartCoroutine(Frase());
    }

    IEnumerator EsperaInicial()
    {
        yield return new WaitForSeconds(esperaInicio);
        panelDialogo.SetActive(true);
        dialogoAlex01.text = string.Empty; // Vaciamos el texto para empezar a escribir la nueva frase.
        ComenzarDialogos();
    }

    IEnumerator Frase()
    {
        // Recorremos cada car�cter de la frase y la vamos a�adiendo al texto del di�logo.
        foreach(char letra in frasesDialogoAlex01[index].ToCharArray())
        {
            dialogoAlex01.text += letra;
            yield return new WaitForSeconds(velocidadTexto); // Definimos el tiempo que queremos que pase entre caracteres, para simular la escritura.
        }
    }

    void SiguienteFrase()
    {
        // Si el �ndice es menor que el array de frases -1
        if(index < frasesDialogoAlex01.Length - 1)
        {
            index ++; // Incrementamos uno el �ndice para pasar a la siguiente frase.
            dialogoAlex01.text = string.Empty; // Vaciamos el texto para empezar a escribir la nueva frase.
            StartCoroutine(Frase());
        }
        else
        {
            panelDialogo.SetActive(false); // Si no lo cumple, quiere decir que estamos en la �ltima frase, as� que desactivamos el GameObject.
            dialogoAlex01.text = string.Empty;
        }
    }
}
