using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void FadeOut()
    {
        gameObject.SetActive(true);
        animator.Play("PanelFade");
        Invoke("AnimationFin", 1.0f);
    }

    public void AnimationFin()
    {
        gameObject.SetActive(false);
    }
}
