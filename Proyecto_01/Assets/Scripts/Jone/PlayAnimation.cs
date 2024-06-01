using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    private Animation anim;

    void Start()
    {
        // Obtener el componente Animation del GameObject
        anim = GetComponent<Animation>();

        // Reproducir la animaci�n
        if (anim != null)
        {
            Debug.Log("deber�a reproducirse");
            anim.Play("corrupcion");
        }
    }
}
