using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuertePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolverPantallaInicio()
    {
        SceneManager.LoadScene(0);
    }

    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Has salido del juego");
    }
}
