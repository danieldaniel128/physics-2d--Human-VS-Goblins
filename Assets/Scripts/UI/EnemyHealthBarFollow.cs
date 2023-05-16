using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarFollow : MonoBehaviour
{
    public Transform enemyTransform;
    private RectTransform healthBarRectTransform;
    [SerializeField] float yOffset;
    [SerializeField] float xOffset;
    private void Start()
    {
        healthBarRectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // Update the position of the health bar to match the enemy's position
        healthBarRectTransform.position = Camera.main.WorldToScreenPoint(enemyTransform.position + Vector3.right * xOffset + Vector3.up * yOffset);

    }
}
