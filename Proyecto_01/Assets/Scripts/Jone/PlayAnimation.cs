using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reproductor estándar para objetos con una única animación (componente animation)
public class PlayAnimation : MonoBehaviour
{
    private Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
        if (anim != null)
        {
            anim.Play();
        }
    }
}
