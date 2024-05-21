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

    [SerializeField] private AudioSource audioSource;
    private int index; // Lo creamos para conocer la posición en la que se encuentra en el array de las frases.

    private void Awake()
    {
        Time.timeScale = 0f; // Nada más empezar la escena, el tiempo del juego se detenga.
    }

    void Start()
    {        
        panelDialogo.SetActive(false);
        StartCoroutine(EsperaInicial());
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Si el texto que se está mostrando coincide con el índice de la frase actual
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
        index = 0; // Iniciamos el índice en 0.        
        StartCoroutine(Frase()); // Empieza a escribir las frases.
    }

    IEnumerator EsperaInicial()
    {
        yield return new WaitForSecondsRealtime(esperaInicio); // Utilizamos WaitForSecondsRealTime para que el Time.timeScale no afecte a las corrutinas y se puedan ejecutar.
        panelDialogo.SetActive(true);
        dialogoAlex01.text = string.Empty; // Vaciamos el texto para empezar a escribir la nueva frase.
        ComenzarDialogos();
    }

    IEnumerator Frase()
    {
        // Recorremos cada carácter de la frase y la vamos añadiendo al texto del diálogo.
        foreach(char letra in frasesDialogoAlex01[index].ToCharArray())
        {
            dialogoAlex01.text += letra;
            yield return new WaitForSecondsRealtime(velocidadTexto); // Definimos el tiempo que queremos que pase entre caracteres, para simular la escritura.
        }
    }

    void SiguienteFrase()
    {
        // Si el índice es menor que el array de frases -1
        if (index < frasesDialogoAlex01.Length - 1)
        {
            index++; // Aumentamos el índice +1.
            dialogoAlex01.text = string.Empty; // Vacía el texto para empezar a escribir la nueva frase.

            // Reproducimos el audio después de la frase número 2. (He metido una frase vacía en medio, se corresponde con el audio).
            if (index == 2)
            {
                panelDialogo.SetActive(false);
                StartCoroutine(EsperarYReproducirAudio());
            }
            else
            {
                panelDialogo.SetActive(true);
                StartCoroutine(Frase());
            }
        }
        else
        {
            panelDialogo.SetActive(false); // Si no lo cumple, quiere decir que estamos en la última frase, así que desactivamos el GameObject.
            dialogoAlex01.text = string.Empty;
            Time.timeScale = 1.0f; // Al terminar la última frase, el juego deja de estar pausado.
        }
    }

    IEnumerator EsperarYReproducirAudio()
    {
        audioSource.Play();
        yield return new WaitForSecondsRealtime(audioSource.clip.length);
        SiguienteFrase();
    }
}
