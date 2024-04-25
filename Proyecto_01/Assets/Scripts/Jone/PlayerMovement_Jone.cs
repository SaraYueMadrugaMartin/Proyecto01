using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_Jone : MonoBehaviour
{
    [SerializeField] float movimiento = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float velocidadX = Input.GetAxis("Horizontal") * movimiento * Time.deltaTime;
        float velocidadY = Input.GetAxis("Vertical") * movimiento * Time.deltaTime;

        transform.Translate(velocidadX, 0, 0);
        transform.Translate(0, velocidadY, 0);
    }
}
