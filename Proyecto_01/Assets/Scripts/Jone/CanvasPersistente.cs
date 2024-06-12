using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPersistente : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
