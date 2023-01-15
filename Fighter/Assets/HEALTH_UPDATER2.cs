using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class HEALTH_UPDATER : MonoBehaviour
{
    [SerializeField]
    GameObject TargetHealthBar2;

    [SerializeField]
    float TemporaryHealthPercent2;

    private float BarSize2 = 750;

    void Update()
    {
        UpdateHealthUI2(TemporaryHealthPercent2);
    }

    void UpdateHealthUI2(float HealthPercent)
    {
        float NewBarSize = Mathf.Abs(((100 - HealthPercent) / 100f) * BarSize2);
        RectTransform COLOR_RT = TargetHealthBar.GetComponent<RectTransform>();
        COLOR_RT.sizeDelta = new Vector2(-NewBarSize, 55);
    }
}
