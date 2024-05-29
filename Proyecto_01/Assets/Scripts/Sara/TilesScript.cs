using UnityEngine;

public class TilesScript : MonoBehaviour
{
    public Vector2 targetPosition;
    [SerializeField] public Vector2 posicionCorrecta;
    [SerializeField] private PuzzleDeslizable puzzleDeslizable;
    private bool enPosicionCorrecta = false;
    //public Vector2 posCorrecta;

    void Start()
    {
        targetPosition = GetComponent<RectTransform>().anchoredPosition;
    }

    void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 previousPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, 0.08f);

        ComprobarPiezasEncajadas();
    }

    public void ComprobarPiezasEncajadas()
    {
        if (!enPosicionCorrecta)
        {
            Debug.Log(gameObject.name + " Target: " + targetPosition + " Correct: " + posicionCorrecta);
            if (Vector2.Distance(targetPosition, posicionCorrecta) < 0.05f)
            {
                enPosicionCorrecta = true;
                puzzleDeslizable.piezasEncajadas++;
                Debug.Log(gameObject.name + " está en la posición correcta.");
            }
            else
            {
                Debug.Log(gameObject.name + " está en la posición mala.");

            }
        }
    }
}
