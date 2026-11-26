using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PowerSlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image circleImage;
    [SerializeField] private TMP_Text cooldownText;
    [SerializeField] private TMP_Text keyText;

    [SerializeField] private Color readyColor = Color.white;
    [SerializeField] private Color cooldownColor = new Color(0.35f, 0.35f, 0.35f, 1f);

    public event System.Action<PowerData> OnUsed;

    private PowerData data;
    private float remaining;

    public bool IsReady => remaining <= 0f;


    public void Setup(PowerData powerData)
    {
        data = powerData;
        remaining = 0f;

        iconImage.sprite = data.icon;
        iconImage.enabled = data.icon != null;

        if (keyText != null) keyText.text = data.key.ToString();

        SetVisual(true);
    }

    void Update()
    {
        if (data == null) return;

        if (IsReady)
        {
            if (Keyboard.current != null && Keyboard.current[data.key].wasPressedThisFrame)
            {
                Use();
            }
            return;
        }

        remaining -= Time.deltaTime;

        if (remaining <= 0f)
        {
            remaining = 0f;
            SetVisual(true);
        }
        else
        {
            cooldownText.text = remaining >= 1f
                ? Mathf.CeilToInt(remaining).ToString()
                : remaining.ToString("0.0");
        }
    }

    private void Use()
    {
        remaining = data.cooldown;
        SetVisual(false);
        cooldownText.text = Mathf.CeilToInt(remaining).ToString();

        OnUsed?.Invoke(data);
    }

    private void SetVisual(bool ready)
    {
        Color c = ready ? readyColor : cooldownColor;
        iconImage.color = c;
        if (circleImage != null) circleImage.color = c;
        cooldownText.gameObject.SetActive(!ready);
    }
}
