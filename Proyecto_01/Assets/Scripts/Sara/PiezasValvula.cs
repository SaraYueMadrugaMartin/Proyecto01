using UnityEngine;

public class PiezasValvula : MonoBehaviour
{
    private Vector2 posCorrecta;
    public bool encajada;
    public bool seleccionada;

    void Start()
    {
        posCorrecta = new;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, posCorrecta) < 0.5f)
        {
            if (!seleccionada)
            {
                if (!encajada)
                {
                    transform.position = posCorrecta;
                    encajada = true;
                    Camera.main.GetComponent<ValvulaArrastrar>().PiezasEncajadas++;
                }
            }
        }
    }
}
