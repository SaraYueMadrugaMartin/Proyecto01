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

// Clase estática para gestionar las animaciones del personaje
public static class AlexAnimator
{
    private static int animPrevia = 0;
    private static bool reproduciendo = false; // Si hay una animación reproduciendose anteriormente

    // Cola de animaciones pendientes con su prioridad
    private static Queue<(int, PrioridadAnimacion)> animQueue = new Queue<(int, PrioridadAnimacion)>();

    private static PrioridadAnimacion currentPriority = PrioridadAnimacion.Baja;

    // Método para reproducir una animación, con su identificador y prioridad
    public static void PlayAnimacion(int num, Animator anim, PrioridadAnimacion priority = PrioridadAnimacion.Baja)
    {
        // Determina si debe esperar a que la animación previa termine
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

        // Si la prioridad es alta, interrumpe cualquier animación y reproduce la nueva
        if (priority == PrioridadAnimacion.Alta)
        {
            StopAllCoroutines(anim);
            ReproduceAnimacion(anim, num, priority);
        }
        // Si debe esperar a que la animación previa termine y la nueva animación tiene igual o menor prioridad, encola la nueva animación
        else if (esperaFin && reproduciendo && priority <= currentPriority)
        {
            animQueue.Enqueue((num, priority));
        }  
        else
        {
            ReproduceAnimacion(anim, num, priority); // Reproduce la nueva animación inmediatamente.
        }
    }

    // Método para reproducir una animación, estableciendo el estado de reproducción y la prioridad actual
    private static void ReproduceAnimacion(Animator anim, int num, PrioridadAnimacion priority)
    {
        reproduciendo = true;  // Animación en curso
        currentPriority = priority;  // Actualiza la prioridad actual
        anim.Play(ControladorAnimaciones.diccionarioAnimaciones[num]);  // Reproduce la animación
        animPrevia = num;  // Actualiza la animación previa
        anim.GetComponent<MonoBehaviour>().StartCoroutine(AnimacionFinalizada(anim));  // Inicia la corrutina para esperar a que la animación termine
    }

    // Método para detener todas las corrutinas y limpiar la cola de animaciones.
    private static void StopAllCoroutines(Animator anim)
    {
        anim.GetComponent<MonoBehaviour>().StopAllCoroutines(); 
        animQueue.Clear();  // Limpia la cola de animaciones
        reproduciendo = false; 
    }

    // Corrutina para esperar a que la animación termine.
    private static IEnumerator AnimacionFinalizada(Animator anim)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);  // Obtiene la información del estado de la animación
        yield return new WaitForSeconds(stateInfo.length);  // Espera a que la animación termine

        reproduciendo = false; 
        currentPriority = PrioridadAnimacion.Baja; 

        // Si hay animaciones en la cola, reproduce la siguiente animación en la cola
        if (animQueue.Count > 0)
        {
            var nextAnim = animQueue.Dequeue();
            ReproduceAnimacion(anim, nextAnim.Item1, nextAnim.Item2);
        }
    }
}
