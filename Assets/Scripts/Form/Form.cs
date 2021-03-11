using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Form : MonoBehaviour
{
    [SerializeField] private TMP_Text _viewText;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Image _logo;

    public void SetText(string text)
    {
        _viewText.text = text;
    }

    public void SetLogo(Sprite sprite)
    {
        _logo.sprite = sprite;
    }

    public void SetLayouts(int spriteOrder, int canvasOrder)
    {
        _canvas.sortingOrder = canvasOrder;
        _spriteRenderer.sortingOrder = spriteOrder;
    }

    public void OnClose()
    {
        Destroy(gameObject);
    }
}
