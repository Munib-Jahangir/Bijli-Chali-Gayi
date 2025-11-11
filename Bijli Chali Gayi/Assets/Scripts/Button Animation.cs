using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Scale Settings")]
    public float hoverScale = 1.1f;        // Jab mouse hover kare to button ka scale
    public float clickScale = 0.9f;        // Jab click kare to thoda chhota ho jaye
    public float animationSpeed = 10f;     // Animation ki smoothness speed

    [Header("Color Settings")]
    public Color normalColor = Color.white;
    public Color hoverColor = new Color(0.8f, 0.9f, 1f);
    public Color clickColor = new Color(0.7f, 0.7f, 1f);

    private Vector3 originalScale;
    private Image buttonImage;
    private Color targetColor;
    private Vector3 targetScale;

    void Start()
    {
        originalScale = transform.localScale;
        buttonImage = GetComponent<Image>();
        targetColor = normalColor;
        targetScale = originalScale;
    }

    void Update()
    {
        // Smoothly lerp scale and color
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * animationSpeed);

        if (buttonImage != null)
            buttonImage.color = Color.Lerp(buttonImage.color, targetColor, Time.deltaTime * animationSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
        targetColor = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
        targetColor = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        targetScale = originalScale * clickScale;
        targetColor = clickColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
        targetColor = hoverColor;
    }
}
