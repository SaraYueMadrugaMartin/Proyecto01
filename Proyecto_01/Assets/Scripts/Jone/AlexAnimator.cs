using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enum para representar las prioridades de las animaciones
public enum PrioridadAnimacion
{
    Baja,
    Media,
    Alta
}

// Clase est�tica para gestionar las animaciones del personaje
public static class AlexAnimator
{
    private static int animPrevia = 0;
    private static bool reproduciendo = false; // Si hay una animaci�n reproduciendose anteriormente

    // Cola de animaciones pendientes con su prioridad
    private static Queue<(int, PrioridadAnimacion)> animQueue = new Queue<(int, PrioridadAnimacion)>();

    private static PrioridadAnimacion currentPriority = PrioridadAnimacion.Baja;

    // M�todo para reproducir una animaci�n, con su identificador y prioridad
    public static void PlayAnimacion(int num, Animator anim, PrioridadAnimacion priority = PrioridadAnimacion.Baja)
    {
        // Determina si debe esperar a que la animaci�n previa termine
        bool esperaFin;
        switch (animPrevia)
        {
            case 1: esperaFin = false; break;
            case 2: esperaFin = false; break;
            case 3: esperaFin = true; break;
            case 4: esperaFin = false; break;
            case 5: esperaFin = true; break;
            case 6: esperaFin = true; break;
            case 7: esperaFin = true; break;
            case 8: esperaFin = true; break;
            case 9: esperaFin = true; break;
            default: esperaFin = true; break;
        }

        // Si la prioridad es alta, interrumpe cualquier animaci�n y reproduce la nueva
        if (priority == PrioridadAnimacion.Alta)
        {
            StopAllCoroutines(anim);
            ReproduceAnimacion(anim, num, priority);
        }
        // Si debe esperar a que la animaci�n previa termine y la nueva animaci�n tiene igual o menor prioridad, encola la nueva animaci�n
        else if (esperaFin && reproduciendo && priority <= currentPriority)
        {
            animQueue.Enqueue((num, priority));
        }  
        else
        {
            ReproduceAnimacion(anim, num, priority); // Reproduce la nueva animaci�n inmediatamente.
        }
    }

    // M�todo para reproducir una animaci�n, estableciendo el estado de reproducci�n y la prioridad actual
    private static void ReproduceAnimacion(Animator anim, int num, PrioridadAnimacion priority)
    {
        reproduciendo = true;  // Animaci�n en curso
        currentPriority = priority;  // Actualiza la prioridad actual
        anim.Play(ControladorAnimaciones.diccionarioAnimaciones[num]);  // Reproduce la animaci�n
        animPrevia = num;  // Actualiza la animaci�n previa
        anim.GetComponent<MonoBehaviour>().StartCoroutine(AnimacionFinalizada(anim));  // Inicia la corrutina para esperar a que la animaci�n termine
    }

    // M�todo para detener todas las corrutinas y limpiar la cola de animaciones.
    private static void StopAllCoroutines(Animator anim)
    {
        anim.GetComponent<MonoBehaviour>().StopAllCoroutines(); 
        animQueue.Clear();  // Limpia la cola de animaciones
        reproduciendo = false; 
    }

    // Corrutina para esperar a que la animaci�n termine.
    private static IEnumerator AnimacionFinalizada(Animator anim)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);  // Obtiene la informaci�n del estado de la animaci�n
        yield return new WaitForSeconds(stateInfo.length);  // Espera a que la animaci�n termine

        reproduciendo = false; 
        currentPriority = PrioridadAnimacion.Baja; 

        // Si hay animaciones en la cola, reproduce la siguiente animaci�n en la cola
        if (animQueue.Count > 0)
        {
            var nextAnim = animQueue.Dequeue();
            ReproduceAnimacion(anim, nextAnim.Item1, nextAnim.Item2);
        }
    }
}
