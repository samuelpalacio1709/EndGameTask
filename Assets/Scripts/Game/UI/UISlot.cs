using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private CanvasGroup group;
    public void ShowIcon(Sprite sprite)
    {
        group.alpha = 1;
        iconImage.sprite = sprite;
    }
}
