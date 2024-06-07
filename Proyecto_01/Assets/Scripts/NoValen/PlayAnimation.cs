using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reproductor est�ndar para objetos con una �nica animaci�n (componente animation)
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
