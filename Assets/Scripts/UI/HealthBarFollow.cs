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
    Canvas healthBarCanvas;

    private void Start()
    {
        healthBarCanvas = transform.parent.GetComponentInParent<Canvas>();
    }
    private void LateUpdate()
    {
        // Update the position of the health bar to match the enemy's position
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(TargetTransform.position + Vector3.right * xOffset + Vector3.up * yOffset);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(healthBarCanvas.transform as RectTransform, screenPosition, healthBarCanvas.worldCamera, out Vector2 canvasPosition);


        // Set the canvas position of the health bar
        healthBarRectTransform.localPosition = canvasPosition;
    }
}
