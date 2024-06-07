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
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, posPiezaInicial, 0.08f);
        ComprobarPiezasEncajadas();
    }

    public void ComprobarPiezasEncajadas()
    {
        bool estabaEnPosCorrecta = enPosCorrecta;

        if (Vector2.Distance(posPiezaInicial, posCorrecta) < 0.01f)
        {
            enPosCorrecta = true;
        }
        else
        {
            enPosCorrecta = false;
        }

        if (enPosCorrecta && !estabaEnPosCorrecta)
        {
            Debug.Log("La pieza: " + name + " está en la posición correcta.");
            puzzleDeslizable.piezasEncajadas++;
        }
        else if (!enPosCorrecta && estabaEnPosCorrecta)
        {
            Debug.Log("La pieza: " + name + " está en la posición incorrecta.");
            puzzleDeslizable.piezasEncajadas--;
        }

        if (puzzleDeslizable.piezasEncajadas == puzzleDeslizable.tiles.Length)
        {
            puzzleDeslizable.TodasPiezasCorrectas();
        }
    }
}