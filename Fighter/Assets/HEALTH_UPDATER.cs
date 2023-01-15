using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class HEALTH_UPDATER : MonoBehaviour
{
    [SerializeField]
    GameObject TargetHealthBar;

    [SerializeField]
    float TemporaryHealthPercent;

    private float BarSize = 750;

    void Update()
    {
        UpdateHealthUI(TemporaryHealthPercent);
    }

    void UpdateHealthUI(float HealthPercent)
    {
        float NewBarSize = Mathf.Abs((HealthPercent / 100f) * BarSize);
        RectTransform COLOR_RT = TargetHealthBar.GetComponent<RectTransform>();
        COLOR_RT.sizeDelta = new Vector2(-NewBarSize, 55);
    }
}
