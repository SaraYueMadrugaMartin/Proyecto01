using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuertePlayer : MonoBehaviour
{
    public void Reiniciar()
    {
        GameManager.instance.ReiniciarEscena();
    }

    public void SalirJuego()
    {
        SceneManager.LoadScene(0);
    }
}
