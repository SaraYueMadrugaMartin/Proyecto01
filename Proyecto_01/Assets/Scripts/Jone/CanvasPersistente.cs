using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPersistente : MonoBehaviour
{
    // Elementos del canvas que es necesario que sean persistentes para que los usen los managers con persistencia
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Hacer que el canvas sea persistente al cambiar de escenas
    }
}
