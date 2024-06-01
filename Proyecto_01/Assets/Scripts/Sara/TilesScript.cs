using UnityEngine;

public class TilesScript : MonoBehaviour
{
    public Vector2 posPiezaInicial;
    [SerializeField] public Vector2 posCorrecta;
    [SerializeField] private PuzzleDeslizable puzzleDeslizable;
    private bool enPosCorrecta;

    void Start()
    {
        posPiezaInicial = GetComponent<RectTransform>().anchoredPosition;
    }

    void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 previousPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, posPiezaInicial, 0.08f);
        ComprobarPiezasEncajadas();
    }

    public void ComprobarPiezasEncajadas()
    {
        if (!enPosCorrecta)
        {
            if(Vector2.Distance(posPiezaInicial, posCorrecta) < 0.05f)
            {
                enPosCorrecta = true;
                puzzleDeslizable.piezasEncajadas++;
                if(puzzleDeslizable.piezasEncajadas == 15)
                {
                    puzzleDeslizable.TodasPiezasCorrectas();
                }
            }
        }
    }
}
