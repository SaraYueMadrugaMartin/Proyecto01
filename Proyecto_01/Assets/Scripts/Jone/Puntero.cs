using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Puntero : MonoBehaviour
{
    private Transform transformPuntero;
    bool miraDerecha = true;

    private void Awake()
    {
        transformPuntero = transform.Find("Puntero");
    }

    private void Update()
    {
        Vector3 posRaton = GetMouseWorldPosition();

        Vector3 direccionPuntero = (posRaton - transform.position).normalized;
        float angulo = Mathf.Atan2(direccionPuntero.y, direccionPuntero.x) * Mathf.Rad2Deg;
        //float altura = Mathf.Sin(angulo);
        
        // Puedo intentar limitar el ángulo o que la rotación depanda solo de Y
        //altura = Mathf.LerpAngle(-70f, 70f, altura);
        //Debug.Log(altura);
        transformPuntero.eulerAngles = new Vector3(0, 0, angulo);

        float velocidadX = Input.GetAxis("Horizontal");
        if (velocidadX > 0 && miraDerecha)
            Flip();
        if (velocidadX < 0 && !miraDerecha)
            Flip();
        /*Vector3 localScale = Vector3.one;
        if (angulo > 90 || angulo < -90)
        {
            localScale.y = -1f;
        } else
        {
            localScale.y = +1f;
        }

        transformPuntero.localScale = localScale;*/
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        miraDerecha = !miraDerecha;
    }



    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
