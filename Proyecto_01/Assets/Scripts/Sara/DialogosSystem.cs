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
    private int index; // Lo creamos para conocer la posici�n en la que se encuentra en el array de las frases.

    private void Awake()
    {
        Time.timeScale = 0f; // Nada m�s empezar la escena, el tiempo del juego se detenga.
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
        // Recorremos cada car�cter de la frase y la vamos a�adiendo al texto del di�logo.
        foreach(char letra in frasesDialogoAlex01[index].ToCharArray())
        {
            dialogoAlex01.text += letra;
            yield return new WaitForSecondsRealtime(velocidadTexto); // Definimos el tiempo que queremos que pase entre caracteres, para simular la escritura.
        }
    }

    void SiguienteFrase()
    {
        // Si el �ndice es menor que el array de frases -1
        if (index < frasesDialogoAlex01.Length - 1)
        {
            index++; // Aumentamos el �ndice +1.
            dialogoAlex01.text = string.Empty; // Vac�a el texto para empezar a escribir la nueva frase.

            // Reproducimos el audio despu�s de la frase n�mero 2. (He metido una frase vac�a en medio, se corresponde con el audio).
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
            panelDialogo.SetActive(false); // Si no lo cumple, quiere decir que estamos en la �ltima frase, as� que desactivamos el GameObject.
            dialogoAlex01.text = string.Empty;
            Time.timeScale = 1.0f; // Al terminar la �ltima frase, el juego deja de estar pausado.
        }
    }

    IEnumerator EsperarYReproducirAudio()
    {
        audioSource.Play();
        yield return new WaitForSecondsRealtime(audioSource.clip.length);
        SiguienteFrase();
    }
}
