using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image img;
    private Color originalColor;
    private void Start()
    {
        img = GetComponent<Image>();
        originalColor = img.color;

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.MobileFireAmmo?.Invoke();
        img.color = Color.gray;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.Instance.MobileFireAmmo?.Invoke();
        img.color = originalColor;
    }
    public void Hide() { gameObject.SetActive(false); }
    public void Show() { gameObject.SetActive(true); }
}

