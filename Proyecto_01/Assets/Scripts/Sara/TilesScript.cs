using UnityEngine;

public class TilesScript : MonoBehaviour
{
    public Vector2 targetPosition;

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
