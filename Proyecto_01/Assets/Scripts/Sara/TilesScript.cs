using UnityEngine;

public class TilesScript : MonoBehaviour
{
    public Vector2 targetPosition;
    public Vector2 posCorrecta;

    void Start()
    {
        targetPosition = GetComponent<RectTransform>().anchoredPosition;
    }

    void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector2 previousPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, 0.08f);
    }
}
