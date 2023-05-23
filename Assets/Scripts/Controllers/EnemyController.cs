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
        enemyCollider.OnCollisionEnter += OnMyCollisionEnter2D;
        enemyCollider.OnCollisionExit += OnMyCollisionExit2D;
    }
    void OnMyCollisionExit2D(MyBoxCollider2D collider) 
    {
       
        if (collider.tag.Equals("Castle"))
        {
             GameManager.Instance.PlayerCastleHealth.GotHurt(EnemyHealth.Damage);//do it so it will get health and do the logic of values inside instead of getting damage
        }
        if (collider.tag.Equals("PlayersWeapon"))
        {
            EnemyHealth.GotHurt(GameManager.Instance.PlayerCastleHealth.Damage);
        }
        //enemyCollider.OnCollisionEnter
    }
    void OnMyCollisionEnter2D(MyBoxCollider2D collider)
    {
        collider.SetOnCollisionWith(CanCollideWithCollider);
    }

    bool CanCollideWithCollider(MyBoxCollider2D collider)//true is cant, false is not
    {
        if (collider.tag.Contains("Enemy"))
            return false;
        return true;
    }

}
