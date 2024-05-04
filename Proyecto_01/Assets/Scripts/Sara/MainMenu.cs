using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelAjustes;
    [SerializeField] private GameObject panelCreditos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ComenzarJuego()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        int siguienteEscena = escenaActual + 1;
        if (siguienteEscena == 2)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(siguienteEscena);
    }

    public void IrAjustes()
    {
        gameObject.SetActive(false);
        panelAjustes.SetActive(true);
    }

    public void IrCreditos()
    {
        gameObject.SetActive(false);
        panelCreditos.SetActive(true);
    }

    public void VolverAtras()
    {
        gameObject.SetActive(true);
        panelAjustes.SetActive(false);
        panelCreditos.SetActive(false);
    }

    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Has salido del juego.");
    }
}
