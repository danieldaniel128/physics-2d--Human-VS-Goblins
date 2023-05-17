using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] MyBoxCollider2D enemyCollider;
    [SerializeField] MyRigidBody2D enemyRigidBody;
    [SerializeField] float _enemySpeed;
    public Health EnemyHealth { get; set; }

    private void Start()
    {
        enemyRigidBody.AddForce(_enemySpeed * Vector2.left);
        enemyCollider.OnCollision += OnMyCollisionEnter2D;
    }
    void OnMyCollisionEnter2D(MyBoxCollider2D collider) 
    {
       
        if (collider.tag.Equals("Castle"))
        {
            Debug.Log("collided with: "+ collider);
        }
    }

}
