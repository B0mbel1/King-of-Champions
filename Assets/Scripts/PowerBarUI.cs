using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[System.Serializable]
public class PowerData
{
    public string name;
    public Sprite icon;
    public Key key = Key.Q;
    public float cooldown = 5f;
}

public class PowerBarUI : MonoBehaviour
{
    [SerializeField] private PowerSlotUI slotPrefab;
    [SerializeField] private List<PowerData> powers = new List<PowerData>();
    void Start()
    {
        Rebuild();
    }

    [ContextMenu("Rebuild")]
    public void Rebuild()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (PowerData power in powers)
        {
            PowerSlotUI slot = Instantiate(slotPrefab, transform);
            slot.Setup(power);
        }
    }

    [ContextMenu("Clear")]
    public void ClearSlots()
    {
        for (int i = transform.childCount - 1; i >= 0; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (Application.isPlaying)
                Destroy(child);
            else
                DestroyImmediate(child);
        }
    }
    public void AddPower(PowerData power)
    {
        powers.Add(power);
        Rebuild();
    }
}
