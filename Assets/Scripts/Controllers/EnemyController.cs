using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] MyBoxCollider2D enemyCollider;
    [SerializeField] MyRigidBody2D enemyRigidBody;
    [SerializeField] float _enemySpeed;
    private void Start()
    {
        enemyRigidBody.AddForce(_enemySpeed * Vector2.left);
    }
}
