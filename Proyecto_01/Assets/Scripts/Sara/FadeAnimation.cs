using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void FadeOut()
    {
        gameObject.SetActive(true);
        animator.Play("FadeAnimation");
        StartCoroutine(ActivarFadeInOut());
        //Invoke("AnimationFin", 1.0f);
    }

    public void FadeOutNivel()
    {
        gameObject.SetActive(true);
        animator.Play("FadeAnimationNivel");
        StartCoroutine(ActivarFadeInOutNivel());
        //Invoke("AnimationFin", 1.0f);
    }

    /*public void AnimationFin()
    {
        gameObject.SetActive(false);
    }*/

    IEnumerator ActivarFadeInOut()
    {
        yield return new WaitForSecondsRealtime(1f);
        gameObject.SetActive(false);
    }

    IEnumerator ActivarFadeInOutNivel()
    {
        yield return new WaitForSecondsRealtime(3f);
        gameObject.SetActive(false);
    }
}
