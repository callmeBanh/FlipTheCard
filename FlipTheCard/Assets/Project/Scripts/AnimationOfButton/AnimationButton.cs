using UnityEngine;
using UnityEngine.EventSystems;
using PrimeTween;

public class AnimationButton : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    public float shrinkScale = 0.9f;
    public float hoverScale = 1f;
    public float duration = 0.3f;

    private RectTransform rectTransform;
    private Vector3 originalScale;

    private Sequence hoverSequence;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverSequence.Stop();

        hoverSequence = Sequence.Create(cycles: -1)
            // Nhỏ lại
            .Chain(Tween.Scale(
                rectTransform,
                originalScale * shrinkScale,
                duration,
                Ease.InOutQuad
            ))
            // To
            .Chain(Tween.Scale(
                rectTransform,
                originalScale * hoverScale,
                duration,
                Ease.InOutSine
            ))
            // Nhỏ lại (tạo nhịp lặp)
            .Chain(Tween.Scale(
                rectTransform,
                originalScale * shrinkScale,
                duration,
                Ease.InOutSine
            ));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverSequence.Stop();

        Tween.Scale(
            rectTransform,
            originalScale,
            duration,
            Ease.OutQuad
        );
    }

    void OnDisable()
    {
        hoverSequence.Stop();
    }
}
