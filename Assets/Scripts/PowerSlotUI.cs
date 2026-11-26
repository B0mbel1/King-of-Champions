using UnityEngine;
using UnityEngine.UI;

public class PowerSlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    public void Setup(PowerData data)
    {
        iconImage.sprite = data.icon;
        iconImage.enabled = data.icon != null;
    }
}
