using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFollow : MonoBehaviour
{
    [SerializeField] private RectTransform healthBarRectTransform;
    public Transform TargetTransform;
    public Image HealthBarImage;
    public float yOffset;
    public float xOffset;

    private void LateUpdate()
    {
        // Update the position of the health bar to match the enemy's position
        healthBarRectTransform.position = Camera.main.WorldToScreenPoint(TargetTransform.position + Vector3.right * xOffset + Vector3.up * yOffset);

    }
}
